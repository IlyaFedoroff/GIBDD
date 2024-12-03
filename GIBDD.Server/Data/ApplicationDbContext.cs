using GIBDD.Server.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GIBDD.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



        public DbSet<Car> Cars { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<Officer> Officers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Связи
            modelBuilder.Entity<Inspection>()
                .HasOne(i => i.Car)
                .WithMany(c => c.Inspections)
                .HasForeignKey(i => i.CarId);

            modelBuilder.Entity<Inspection>()
                .HasOne(i => i.Owner)
                .WithMany(o => o.Inspections)
                .HasForeignKey(i => i.OwnerId);

            modelBuilder.Entity<Inspection>()
                .HasOne(i => i.Officer)
                .WithMany(o => o.Inspections)
                .HasForeignKey(i => i.OfficerId);
        }
    }
}
