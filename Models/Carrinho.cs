using InduMovel.Context;
using Microsoft.EntityFrameworkCore;
namespace InduMovel.Models
{
    public class Carrinho
    {
        private readonly AppDbContext _context;
        public Carrinho(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoId { get; set; }
        public List<CarrinhoItem> CarrinhoItens { get; set; }
        public static Carrinho GetCarrinhoCompra(IServiceProvider
        services)
        {

            ISession session =

            services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<AppDbContext>();
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            session.SetString("CarrinhoId", carrinhoId);
            return new Carrinho(context)
            {
                CarrinhoId = carrinhoId
            };

        }
        public void AdicionarItemCarrinho(Movel movel)
        {
            var carinhoItem = _context.CarrinhoItens.SingleOrDefault(s =>

        s.Movel.MovelId == movel.MovelId && s.CarrinhoId == CarrinhoId);

            if (carinhoItem == null)
            {
                carinhoItem = new CarrinhoItem
                {
                    CarrinhoId = CarrinhoId,
                    Movel = movel,
                    Quantidade = 1
                };
                _context.CarrinhoItens.Add(carinhoItem);
            }
            else
            {
                carinhoItem.Quantidade++;
            }
            _context.SaveChanges();
        }
        public int RemoverItemDoCarrinhoCompra(Movel movel)
        {
            var carrinhoQuantidade = 0;
            var carinhoItem = _context.CarrinhoItens.SingleOrDefault(s =>

            s.Movel.MovelId == movel.MovelId && s.CarrinhoId == CarrinhoId);

            if (carinhoItem != null)
            {
                if (carinhoItem.Quantidade > 1)
                {
                    carinhoItem.Quantidade--;
                    carrinhoQuantidade = carinhoItem.Quantidade;
                }
                else
                {
                    _context.CarrinhoItens.Remove(carinhoItem);
                }

            }
            _context.SaveChanges();
            return carrinhoQuantidade;
        }
        public List<CarrinhoItem> GetCarrinhoCompraItems()
        {
            return CarrinhoItens ?? _context.CarrinhoItens.Where(c =>

            c.CarrinhoId == CarrinhoId).Include(s => s.Movel).ToList();
        }
        public void LimparCarrinho()
        {
            var carrinhoCompra = _context.CarrinhoItens.Where(c =>

        c.CarrinhoId == CarrinhoId);

            _context.CarrinhoItens.RemoveRange(carrinhoCompra);
            _context.SaveChanges();
        }
        public double GetCarrinhoCompraTotal()
        {
            List<double> total = _context.CarrinhoItens.Where(_c =>

            _c.CarrinhoId == CarrinhoId).Select(c =>
            c.Quantidade * c.Movel.Valor).ToList();
            double totalr = 0;
            foreach (double t in total)
            {
                totalr = totalr + t;
            }
            return totalr;
        }
    }
}