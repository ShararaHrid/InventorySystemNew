using System.Diagnostics;
using InventorySystem.Models;
using InventorySystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace InventorySystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    //private readonly UnitOfWork _unitOfWork;
    public HomeController(ILogger<HomeController> logger)//,UnitOfWork unitOfWork)
    {
        _logger = logger;
        //_unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        //var products = _unitOfWork.Products.GetAll();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
