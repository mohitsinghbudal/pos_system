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
    }
}