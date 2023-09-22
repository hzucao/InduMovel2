using InduMovel.Models;
using Microsoft.Identity.Client;
namespace InduMovel.ViewModel
{
    public class MovelListViewModel
    {
        public IEnumerable<Movel> Moveis { get; set; }
        public string CategoriaAtual { get; set; }
    }
}