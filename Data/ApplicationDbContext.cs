using Microsoft.EntityFrameworkCore;
using thingservice.Model;

namespace thingservice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Thing> Things { get; set; }
        public DbSet<ThingGroup> ThingGroups { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
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


            modelBuilder.Entity<ThingGroup>()
                .Property(x => x.groupCode).HasMaxLength(50);
            modelBuilder.Entity<ThingGroup>()
            .Property(x => x.groupName).HasMaxLength(50);
            modelBuilder.Entity<ThingGroup>()
           .Property(x => x.groupDescription).HasMaxLength(100);


            modelBuilder.Entity<Parameter>()
                .Property(x => x.ParameterCode).HasMaxLength(50);
            modelBuilder.Entity<Parameter>()
            .Property(x => x.parameterName).HasMaxLength(50);
            modelBuilder.Entity<Parameter>()
           .Property(x => x.parameterDescription).HasMaxLength(100);

        }
    }
}