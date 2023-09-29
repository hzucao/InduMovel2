using InduMovel.Models;
using InduMovel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace InduMovel.Components
{
    public class CarrinhoResumo : ViewComponent
    {
        private readonly Carrinho _carrinho;
        public CarrinhoResumo(Carrinho carrinho)
        {
            _carrinho = carrinho;
        }
        public IViewComponentResult Invoke()
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
    }
}