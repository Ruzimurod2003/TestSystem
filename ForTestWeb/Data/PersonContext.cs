using Microsoft.EntityFrameworkCore;

namespace ForTestWeb.Data
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options)
        {
            //Database.EnsureCreated();  // создаем базу данных при первом обращении
        }
        public DbSet<Person>? People { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person { Id = Guid.NewGuid().ToString(), Name = "Tom", Age = 37 },
                new Person { Id = Guid.NewGuid().ToString(), Name = "Bob", Age = 41 },
                new Person { Id = Guid.NewGuid().ToString(), Name = "Sam", Age = 24 }
                );
        }

    }
}
