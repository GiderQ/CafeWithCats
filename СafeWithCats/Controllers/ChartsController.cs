using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeWithCats.Models;

namespace CafeWithCats.Controllers;

public class ChartsController : Controller
{
    private readonly CafeContext _context;

    public ChartsController(CafeContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var drinks = await _context.Drinks.ToListAsync();

        ViewBag.DrinkNames = drinks.Select(d => d.Name).ToList();
        ViewBag.DrinkPrices = drinks.Select(d => d.Price).ToList();

        return View();
    }
}