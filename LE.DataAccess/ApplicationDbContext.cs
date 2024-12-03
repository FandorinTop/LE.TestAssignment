using LE.DomainEntities;
using Microsoft.EntityFrameworkCore;
using DomainTask = LE.DomainEntities.Task;

namespace LE.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                   .HasIndex(b => b.Username)
                   .IsUnique();
            modelBuilder.Entity<User>()
                  .HasIndex(b => b.Email)
                  .IsUnique();
        }

        public DbSet<DomainTask> Tasks { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
