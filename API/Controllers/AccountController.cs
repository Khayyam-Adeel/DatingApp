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
                if (await UserExist(registerDTO.Username)) return BadRequest("Username is taken");
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
            catch (Exception)
            {
                throw;
            }
           
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO){
              var user =await _context.User.SingleOrDefaultAsync(x=>x.UserName==loginDTO.Username);
              if(user==null) return Unauthorized("Invalid username");

              using var hmac=new HMACSHA512(user.PasswordSalt);
              var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

              for(int i=0 ;i<computedHash.Length;i++){
                if(computedHash[i]!=user.PasswordHash[i]) return Unauthorized("Invalid password");
              }
              return new UserDTO{
                    Username=user.UserName,
                    Token=_tokenService.CreateToken(user)
                };
        }
        private async Task<bool> UserExist(string username){
            return await _context.User.AnyAsync(x=>x.UserName==username.ToLower());
        }
    }
}