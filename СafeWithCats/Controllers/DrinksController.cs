using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;
using Microsoft.EntityFrameworkCore;

public class DrinksController : Controller
{
    private readonly CafeContext _context;

    public DrinksController(CafeContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var drinks = await _context.Drinks.ToListAsync();
        return View(drinks);
    }

    public async Task<IActionResult> Details(int id)
    {
        var drink = await _context.Drinks.FindAsync(id);
        if (drink == null) return NotFound();
        return View(drink);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Drink drink)
    {
        if (ModelState.IsValid)
        {
            _context.Drinks.Add(drink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(drink);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var drink = await _context.Drinks.FindAsync(id);
        if (drink == null) return NotFound();
        return View(drink);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Drink drink)
    {
        if (id != drink.Id) return BadRequest();

        if (ModelState.IsValid)
        {
            _context.Drinks.Update(drink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(drink);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var drink = await _context.Drinks.FindAsync(id);
        if (drink == null) return NotFound();
        return View(drink);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var drink = await _context.Drinks.FindAsync(id);
        if (drink == null) return NotFound();

        _context.Drinks.Remove(drink);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}