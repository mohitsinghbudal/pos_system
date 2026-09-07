using Microsoft.EntityFrameworkCore;
using POS.DataLayer.Models;

namespace POS.DataLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.HasIndex(u => u.Email).IsUnique();

                entity.Property(u => u.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(u => u.Role)
                      .HasMaxLength(50)
                      .HasDefaultValue("customer");
            });
        }
    }
}