namespace impar_back_end.Context
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using impar_back_end.Models.Car.Entity;
    using impar_back_end.Models.Photo.Entity;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Car>()
                .Property(c => c.PhotoId);

            modelBuilder.Entity<Photo>()
                .HasKey(p => p.Id);
        }

    }
}
