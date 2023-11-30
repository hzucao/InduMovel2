using InduMovel.Context;
using InduMovel.Models;
using Microsoft.EntityFrameworkCore;

namespace InduMovel.Areas.Admin.Services
{
    public class RelatorioVendasServices
    {
        private readonly AppDbContext _context;
        public RelatorioVendasServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Pedido>> BuscaPorData(DateTime? di,
        DateTime? df)
        {

            var queryPedidos = from obj in _context.Pedidos select obj;
            if (di.HasValue)
            {
                queryPedidos = queryPedidos.Where(p => p.PedidoEnviado >=

                di.Value);
            }
            if (df.HasValue)
            {
                queryPedidos = queryPedidos.Where(p => p.PedidoEnviado <=

                df.Value);
            }
            return await queryPedidos.Include(i =>
            i.PedidoMoveis).ThenInclude(m => m.Movel).OrderBy(d =>
            d.PedidoEnviado).ToListAsync();
        }
    }
}