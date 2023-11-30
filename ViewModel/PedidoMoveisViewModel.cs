using InduMovel.Models;

namespace InduMovel.ViewModel
{
    public class PedidoMoveisViewModel
    {
        public Pedido Pedidos { get; set; }
        public IEnumerable<PedidoMovel> PedidoMoveis { get; set; }
    }
}