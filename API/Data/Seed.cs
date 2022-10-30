using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text; 
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUser(DataContext  context){
            if(await context.User.AnyAsync()) return;

            var usersDate=System.IO.File.ReadAllText("Data/UserSeedData.json");
            //var users=JsonSerializer.Deserialize<List<AppUser>>(usersDate);
            var users= Newtonsoft.Json.JsonConvert.DeserializeObject<List<AppUser>>(usersDate);

            foreach(var user in users){
                using var hmac=new HMACSHA512();
                user.UserName=user.UserName.ToLower();
                user.PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes("P@ssw0rd"));
                user.PasswordSalt=hmac.Key;

                context.User.Add(user);
            }
            await context.SaveChangesAsync();

        }
    }
}