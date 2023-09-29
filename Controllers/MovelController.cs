using InduMovel.Models;
using InduMovel.Repositories.Interfaces;
using InduMovel.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace InduMovel.Controllers
{
    public class MovelController : Controller
    {
        private readonly IMovelRepository _movelRespository;
        public MovelController(IMovelRepository movelRespository)
        {
            _movelRespository = movelRespository;
        }
        public IActionResult List(string categoria)
        {
            IEnumerable<Movel> moveis;
            var categoriaAtual = string.Empty;
            if (string.IsNullOrEmpty(categoria))
            {
                moveis = _movelRespository.Moveis.OrderBy(m => m.MovelId);
                categoriaAtual = "Todos os itens";
            }
            else
            {
                moveis = _movelRespository.Moveis.Where(m =>
                m.Categoria.Nome.Equals(categoria)).OrderBy(m => m.MovelId);

                categoriaAtual = categoria;
            }
            var movelListViewMovel = new MovelListViewModel
            {
                Moveis = moveis,
                CategoriaAtual = categoriaAtual
            };
            return View(movelListViewMovel);
        }
        public IActionResult Detail(int movelId)
        {

            var movel = _movelRespository.Moveis.FirstOrDefault(m => m.MovelId == movelId);

            return View(movel);
        }

        public IActionResult Search(string searchString)
        {

            IEnumerable<Movel> moveis;
            string categoriaAtual = string.Empty;
            if (string.IsNullOrEmpty(searchString))
            {
                moveis = _movelRespository.Moveis.OrderBy(m => m.Nome);
                categoriaAtual = "Todos os Itens";
            }
            else
            {
                moveis = _movelRespository.Moveis.Where(m =>
                m.Nome.ToLower() == searchString.ToLower()).OrderBy(m => m.Nome);

                if (moveis.Any())
                {
                    categoriaAtual = "Itens";
                }
                else
                {
                    categoriaAtual = "Nada encontrado";
                }
            }
            return View("~/Views/Movel/List.cshtml", new
            MovelListViewModel
            {
                CategoriaAtual = categoriaAtual,
                Moveis = moveis
            });

        }




    }
}