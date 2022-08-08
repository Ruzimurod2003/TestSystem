using Microsoft.EntityFrameworkCore;
using Models;
using Functions.PasswordHash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Functions.Configuration;

namespace SqlServer_Database.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User>? Users { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<Problem>? Problems { get; set; }
        public DbSet<Department>? Departments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    RoleId = Guid.Parse("184fbb2c-1644-4fcd-a068-f9a6bcd75baa"),
                    RoleName = "Owner"
                },
                new Role
                {
                    RoleId = Guid.Parse("978ea56a-1767-4f58-9854-783ede9c769f"),
                    RoleName = "Administrator"
                },
                new Role
                {
                    RoleId = Guid.Parse("12c9dc21-cabb-4995-aa51-60a959a22f03"),
                    RoleName = "TestCreator"
                },
                new Role
                {
                    RoleId = Guid.Parse("86ef4afe-bc06-4926-bc1a-f800c8c219dc"),
                    RoleName = "User"
                }
                );
            modelBuilder.Entity<User>().HasData
                (
                 new User
                 {
                     Id = Guid.Parse("49113af2-004d-42e4-b797-3d5316c447fb"),
                     FirstName = "Ruzimurod",
                     LastName = "Abdunazarov",
                     Birth = DateTime.Parse("23.11.2003"),
                     Email = "Ruzimurodabdunazarov2003@mail.ru",
                     RoleId = Guid.Parse("184fbb2c-1644-4fcd-a068-f9a6bcd75baa"),
                     HashPassword = SecurePasswordHasher.Hash("P@ssw0rd"),
                     CreatedDate = DateTime.Now
                 },
                 new User
                 {
                     Id = Guid.Parse("b41eb9ad-b277-4ee2-8455-36e581c72f1e"),
                     FirstName = "Ruzinazar",
                     LastName = "Chuliyev",
                     Birth = DateTime.Parse("20.07.1972"),
                     Email = "Ruzinazarchuliyev1972@mail.ru",
                     RoleId = Guid.Parse("978ea56a-1767-4f58-9854-783ede9c769f"),
                     HashPassword = SecurePasswordHasher.Hash("Sw0rdf1sh"),
                     CreatedDate = DateTime.Now
                 }
                 );

        }
    }
}
