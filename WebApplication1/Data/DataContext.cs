using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace BlogApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<BlogDetails> BlogDetails {get;set; }
    }
}
