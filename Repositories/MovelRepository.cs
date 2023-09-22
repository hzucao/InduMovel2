using InduMovel.Context;
using InduMovel.Models;
using InduMovel.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace InduMovel.Repositories
{
    public class MovelRepository : IMovelRepository
    {
        private readonly AppDbContext _context;
        public MovelRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Movel> Moveis => _context.Moveis.Include(c =>
        c.Categoria);
        public IEnumerable<Movel> MoveisEmProducao =>
        _context.Moveis.Where(m => m.EmProducao).Include(c => c.Categoria);
        public Movel GetMovelById(int movelId)
        {
            return _context.Moveis.FirstOrDefault(m => m.MovelId ==

            movelId);
        }
    }
}