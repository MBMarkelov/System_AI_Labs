using Microsoft.EntityFrameworkCore;
using SAI.Infrastructure.Models;

namespace SAI.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ContactEntity> Contacts => Set<ContactEntity>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactEntity>().ToTable("contacts");
            base.OnModelCreating(modelBuilder);
        }
    }
}
