using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ApplicationUser : IdentityUser
    {
        // updating identity
        
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Address { get; set; }

        public Gender Gender { get; set; }
    }
    
    
    public enum Gender
    {
        Male,
        Female,
        Others
    }
}
