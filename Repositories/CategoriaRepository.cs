using InduMovel.Context;
using InduMovel.Models;
using InduMovel.Repositories.Interfaces;
namespace InduMovel.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context)
        {


            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}