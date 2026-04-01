using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CafeWithCats.Models;
using Microsoft.EntityFrameworkCore;

public class CafesController : Controller
{
    private readonly CafeContext _context;

    public CafesController(CafeContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var cafes = await _context.Cafes.ToListAsync();
        return View(cafes);
    }

    public async Task<IActionResult> Details(int id)
    {
        var cafe = await _context.Cafes.FindAsync(id);
        if (cafe == null) return NotFound();
        return View(cafe);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Cats = new SelectList(_context.Cats.ToList(), "Id", "Name");
        ViewBag.Staffs = new SelectList(_context.Staffs.ToList(), "Id", "FirstName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Cafe cafe)
    {
        if (ModelState.IsValid)
        {
            _context.Cafes.Add(cafe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Cats = new SelectList(_context.Cats.ToList(), "Id", "Name");
        ViewBag.Staffs = new SelectList(_context.Staffs.ToList(), "Id", "FirstName");

        return View(cafe);
    }


    public async Task<IActionResult> Edit(int id)
    {
        var cafe = await _context.Cafes.FindAsync(id);
        if (cafe == null) return NotFound();
        return View(cafe);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Cafe cafe)
    {
        if (id != cafe.Id) return BadRequest();
        if (!ModelState.IsValid) return View(cafe);

        _context.Cafes.Update(cafe);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var cafe = await _context.Cafes.FindAsync(id);
        if (cafe == null) return NotFound();
        return View(cafe);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var cafe = await _context.Cafes.FindAsync(id);
        if (cafe == null) return NotFound();
        _context.Cafes.Remove(cafe);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}