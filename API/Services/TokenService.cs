using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService:ITokenService
    {
        private readonly SymmetricSecurityKey _Key;
        public TokenService(IConfiguration config){
                  _Key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user){
             var claims=new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
             };
             var creds=new SigningCredentials(_Key,SecurityAlgorithms.HmacSha256Signature);

             var TokenDescriptor=new SecurityTokenDescriptor{
                    Subject=new ClaimsIdentity(claims),
                    Expires=DateTime.Now.AddDays(100),
                    SigningCredentials=creds
             };
             var TokenHandler=new JwtSecurityTokenHandler();

             var Token=TokenHandler.CreateToken(TokenDescriptor);

             return TokenHandler.WriteToken(Token);
        }
    }
}