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

        //public DbSet<Pomodoro> Pomodoros { get; set; }
    }
}
