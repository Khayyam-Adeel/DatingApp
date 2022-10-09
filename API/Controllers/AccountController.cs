using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController:BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService){
            _context=context;
            _tokenService=tokenService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO){
           
            try
            {
                if (await UserExsist(registerDTO.Username)) return BadRequest("UserName is Taken");
                using var hmac = new HMACSHA512();
                var user = new AppUser
                {
                    UserName = registerDTO.Username.ToLower(),
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.password)),
                    PasswordSalt = hmac.Key

                };
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return new UserDTO{
                    Username=user.UserName,
                    Token=_tokenService.CreateToken(user)
                };
            }
            catch (Exception ex)
            {
                string ex1 = ex.Message;
                throw ex;
            }
           
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> LoginDTO(LoginDTO loginDTO){
              var user =await _context.User.SingleOrDefaultAsync(x=>x.UserName==loginDTO.Username);
              if(user==null) return Unauthorized("invalid UserName");

              using var hmac=new HMACSHA512(user.PasswordSalt);
              var ComputedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

              for(int i=0 ;i<ComputedHash.Length;i++){
                if(ComputedHash[i]!=user.PasswordHash[i]) return Unauthorized("Password is invalid");
              }
              return new UserDTO{
                    Username=user.UserName,
                    Token=_tokenService.CreateToken(user)
                };
        }
        private async Task<bool> UserExsist(string Username){
            return await _context.User.AnyAsync(x=>x.UserName==Username.ToLower());
        }
    }
}