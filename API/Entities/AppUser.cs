using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {
        public AppUser()
        {
            Photos = new List<Photo>();
            Created = DateTime.UtcNow;
            LastActive = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public DateTime DateOfBirth{get;set;}

        public string KnownAs{get;set;}

        public DateTime Created{get;set;}

        public DateTime LastActive{get;set;}
        public string Gender{get;set;}

        public string Introduction{get;set;}

        public string Lookingfor{get;set;}

        public string interests{get;set;}

        public string City{get;set;}

        public string Country{get;set;}

        public ICollection<Photo> Photos{get;set;}
        
        // public int GetAge(){
        //     return DateOfBirth.calculateAge();

        // }


    }
}