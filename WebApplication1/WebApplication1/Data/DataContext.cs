using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        // <ClassName>
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {

        }
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<WebApplication1.ViewModels.RoleStore> RoleStore { get; set; }

        public DbSet<WebApplication1.ViewModels.AppUserViewModel> AppUserViewModel { get; set; }

        

        
    }
}
