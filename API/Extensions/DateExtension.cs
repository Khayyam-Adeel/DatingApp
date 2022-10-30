using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class DateExtension
    {
        public static int calculateAge(this DateTime DOB){
                var today=DateTime.Today;
                var age=today.Year-DOB.Year;
                if(DOB.Date>today.AddYears(-age)) age--;  
                return age;
        }
    }
}