using LumiaPraktika.Models;
using Microsoft.EntityFrameworkCore;

namespace LumiaPraktika.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}
