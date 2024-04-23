using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class memberUpdateDTO
    {
        public string Introduction { get; set; }

        public string Lookingfor { get; set; }

        public string interests { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}