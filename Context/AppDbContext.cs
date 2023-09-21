using InduMovel.Models;
using Microsoft.EntityFrameworkCore;
namespace InduMovel.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options)
        {
        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Movel> Moveis { get; set; }
    }
}