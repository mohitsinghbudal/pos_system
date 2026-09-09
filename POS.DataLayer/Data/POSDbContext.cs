using Microsoft.EntityFrameworkCore;
using POS.DataLayer.Models;

namespace POS.DataLayer.Data
{
    public class POSDbContext : DbContext
    {
        public POSDbContext(DbContextOptions<POSDbContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                .HasOne(r => r.DeletedByUser)
                .WithMany()
                .HasForeignKey(r => r.DeletedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.RoleUpdatedByUser)
                .WithMany()
                .HasForeignKey(u => u.RoleUpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.DeletedByUser)
                .WithMany()
                .HasForeignKey(u => u.DeletedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // RefreshToken → User
            modelBuilder.Entity<RefreshToken>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    } }
