using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PillarInterview.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PillarInterview.Data
{
    public static class DbInitializer
    {
        
        public async static Task Initialize(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();
           
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }
            
            await roleManager.CreateAsync(new IdentityRole { Name = Roles.AdminRole });
            await roleManager.CreateAsync(new IdentityRole { Name = Roles.UserRole });

            var user = new User { UserName = "Admin", Email = "aaa@aaa.com" };
            await userManager.CreateAsync(user, "P@ssw0rd");
            var user1 = new User { UserName = "User1", Email = "user1@aaa.com" };
            await userManager.CreateAsync(user1, "P@ssw0rd");

            await userManager.AddToRoleAsync(user, Roles.AdminRole);
            await userManager.AddToRoleAsync(user1, Roles.UserRole);

            var customerTypes = new [] {
                new CustomerType{ Title="Municipality"},
                new CustomerType{ Title="Business"}
            };

            foreach (CustomerType type in customerTypes)
            {
                context.CustomerTypes.Add(type);
            }
            
            context.SaveChanges();
       
            //var departments = new Department[] {
            //    new Department{ Name="Main Department" }
            //};
            //foreach (Department department in departments)
            //{
            //    context.Departments.Add(department);
            //}
            //context.SaveChanges();
        }
    }
    

}
