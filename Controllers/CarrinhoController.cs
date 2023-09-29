using InduMovel.Models;
using InduMovel.Repositories.Interfaces;
using InduMovel.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace InduMovel.Controllers
{
    [Authorize]
    public class CarrinhoController : Controller
    {
        private readonly Carrinho _carrinho;
        private readonly IMovelRepository _movelRepository;
        public CarrinhoController(Carrinho carrinho, IMovelRepository movelRepository)
        {
            _carrinho = carrinho;
            _movelRepository = movelRepository;
        }
        public IActionResult Index()
        {
            var itens = _carrinho.GetCarrinhoCompraItems();
            _carrinho.CarrinhoItens = itens;
            var carrinhoVM = new CarrinhoViewModel
            {
                Carrinho = _carrinho,
                CarrinhoTotal = _carrinho.GetCarrinhoCompraTotal()
            };
            return View(carrinhoVM);
        }
        public IActionResult AdicionarItemNoCarrinhoCompra(int movelId)
        {
            var movelSelecionado = _movelRepository.Moveis.FirstOrDefault(l => l.MovelId == movelId);
            if (movelSelecionado != null)
            {
                _carrinho.AdicionarItemCarrinho(movelSelecionado);
            }
            return RedirectToAction("Index");
        }
        public IActionResult RemoverCarrinho(int movelId)
        {
            var movelSelecionado = _movelRepository.Moveis.FirstOrDefault(l => l.MovelId == movelId);
            if (movelSelecionado != null)
            {
                _carrinho.RemoverItemDoCarrinhoCompra(movelSelecionado);
            }
            return RedirectToAction("Index");
        }
    }
}