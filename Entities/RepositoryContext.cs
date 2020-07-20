using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
    : base(options)
        {
        }

        public DbSet<Pomodoro> Pomodoros { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=MyPomodoroDB;", b => b.MigrationsAssembly("Entities"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<User>()
                .HasMany(g => g.Pomodoros)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
