using Microsoft.EntityFrameworkCore;
using ThingsAPI.Model;

namespace ThingsAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Thing> Things { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Thing>()
                .Property(x => x.physicalConnection).HasMaxLength(100);
            modelBuilder.Entity<Thing>()
                .Property(x => x.thingCode).HasMaxLength(50);
            modelBuilder.Entity<Thing>()
            .Property(x => x.thingName).HasMaxLength(50);
            modelBuilder.Entity<Thing>()
           .Property(x => x.position).HasDefaultValue(0);
            modelBuilder.Entity<Thing>()
           .Property(x => x.Description).HasMaxLength(100);
        }
    }
}