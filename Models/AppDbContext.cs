using Microsoft.EntityFrameworkCore;

namespace CVProject.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<CV> CVs { get; set; }
    }
}
