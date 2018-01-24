using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PillarInterview.Data.Models;

namespace PillarInterview.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentManager> DepartmentManagers { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
         
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<DepartmentManager>()
                .HasKey(e => e.DepartmentId);

            builder.Entity<UserInfo>()
                .HasKey(e => e.UserId);

            builder.Entity<User>()
                .HasOne(e => e.UserInfo)
                .WithOne(e => e.User)
                .HasForeignKey<UserInfo>(e => e.UserId);

            builder.Entity<Department>()
                .HasOne(e => e.DepartmentManager)
                .WithOne(e => e.Department)
                .HasForeignKey<DepartmentManager>(e => e.DepartmentId);

            builder.Entity<UserInfo>()
                .HasOne(e => e.Department)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
