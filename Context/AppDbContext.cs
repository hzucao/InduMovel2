using InduMovel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace InduMovel.Context
{
    public class AppDbContext : IdentityDbContext<UserAccount>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options)
        {
        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Movel> Moveis { get; set; }
        public DbSet<CarrinhoItem> CarrinhoItens{get;set;}
    }
}