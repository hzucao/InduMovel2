using InduMovel.Context;
using InduMovel.Migrations;
using InduMovel.Models;
using InduMovel.Repositories.Interfaces;
namespace InduMovel.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;
        private readonly Carrinho _carrinho;
        public PedidoRepository(AppDbContext context, Carrinho carrinho)
        {
            _context = context;
            _carrinho = carrinho;
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
            var carrinhoItens = _carrinho.CarrinhoItens;
            foreach (var carI in carrinhoItens)
            {
                var pm = new PedidoMovel()
                {
                    Quantidade = carI.Quantidade,
                    MovelId = carI.Movel.MovelId,
                    PedidoId = pedido.PedidoId,
                    Preco = Convert.ToDecimal(carI.Movel.Valor)
                };
                _context.PedidoMoveis.Add(pm);
            }
            _context.SaveChanges();
        }
    }
}