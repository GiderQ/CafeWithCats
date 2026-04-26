using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin,Manager")]
public class DishesController : Controller
{
    private readonly CafeContext _context;

    public DishesController(CafeContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var dishes = await _context.Dishes.ToListAsync();
        return View(dishes);
    }

    public async Task<IActionResult> Details(int id)
    {
        var dish = await _context.Dishes.FindAsync(id);
        if (dish == null) return NotFound();
        return View(dish);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Dish dish)
    {
        if (ModelState.IsValid)
        {
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(dish);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var dish = await _context.Dishes.FindAsync(id);
        if (dish == null) return NotFound();
        return View(dish);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Dish dish)
    {
        if (id != dish.Id) return BadRequest();

        if (ModelState.IsValid)
        {
            _context.Dishes.Update(dish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(dish);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var dish = await _context.Dishes.FindAsync(id);
        if (dish == null) return NotFound();
        return View(dish);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var dish = await _context.Dishes.FindAsync(id);
        if (dish == null) return NotFound();

        _context.Dishes.Remove(dish);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}