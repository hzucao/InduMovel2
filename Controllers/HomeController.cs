using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InduMovel.Models;
using InduMovel.Repositories.Interfaces;
using InduMovel.ViewModel;

namespace InduMovel.Controllers;

public class HomeController : Controller
{
    private readonly IMovelRepository _movelRepository;

    public HomeController(IMovelRepository movelRepository)
    {
        _movelRepository = movelRepository;
    }
    public IActionResult Index()
    {
        var homeViewModel = new HomeViewModel
        {
            MoveisEmProducao = _movelRepository.MoveisEmProducao
        };
        return View(homeViewModel);
    }
   
}
