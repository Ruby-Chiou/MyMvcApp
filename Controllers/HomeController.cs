using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace MyMvcApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    public HomeController(ApplicationDbContext context)
    {
            _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.ToListAsync();
        return View(products);
    }
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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
