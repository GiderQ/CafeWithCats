using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;
using Microsoft.EntityFrameworkCore;

public class CatsController : Controller
{
    private readonly CafeContext _context;

    public CatsController(CafeContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var cats = await _context.Cats.Include(c => c.Schedule).ToListAsync();
        return View(cats);
    }

    public async Task<IActionResult> Details(int id)
    {
        var cat = await _context.Cats
            .Include(c => c.Schedule)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cat == null) return NotFound();
        return View(cat);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Schedules = _context.Schedules.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Cat cat)
    {
        if (!await _context.Schedules.AnyAsync(s => s.Id == cat.ScheduleId))
        {
            ModelState.AddModelError("ScheduleId", "Selected schedule does not exist.");
        }

        if (ModelState.IsValid)
        {
            _context.Cats.Add(cat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Schedules = _context.Schedules.ToList();
        return View(cat);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var cat = await _context.Cats.FindAsync(id);
        if (cat == null) return NotFound();

        ViewBag.Schedules = _context.Schedules.ToList();
        return View(cat);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Cat cat)
    {
        if (id != cat.Id) return BadRequest();

        if (!ModelState.IsValid) return View(cat);

        _context.Cats.Update(cat);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var cat = await _context.Cats
            .Include(c => c.Schedule)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cat == null) return NotFound();
        return View(cat);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var cat = await _context.Cats.FindAsync(id);
        if (cat == null) return NotFound();

        _context.Cats.Remove(cat);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}