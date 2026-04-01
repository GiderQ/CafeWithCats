using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;
using Microsoft.EntityFrameworkCore;

public class SchedulesController : Controller
{
    private readonly CafeContext _context;

    public SchedulesController(CafeContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var schedules = await _context.Schedules.Include(s => s.Cats).ToListAsync();
        return View(schedules);
    }

    public async Task<IActionResult> Details(int id)
    {
        var schedule = await _context.Schedules
            .Include(s => s.Cats)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (schedule == null) return NotFound();
        return View(schedule);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Cats = _context.Cats.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Schedule schedule, int[] selectedCats)
    {
        if (selectedCats != null)
        {
            schedule.Cats = _context.Cats.Where(c => selectedCats.Contains(c.Id)).ToList();
        }

        if (ModelState.IsValid)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Cats = _context.Cats.ToList();
        return View(schedule);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var schedule = await _context.Schedules
            .Include(s => s.Cats)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (schedule == null) return NotFound();

        ViewBag.Cats = _context.Cats.ToList();
        return View(schedule);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Schedule schedule, int[] selectedCats)
    {
        if (id != schedule.Id) return BadRequest();

        if (selectedCats != null)
        {
            schedule.Cats = _context.Cats.Where(c => selectedCats.Contains(c.Id)).ToList();
        }

        if (ModelState.IsValid)
        {
            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Cats = _context.Cats.ToList();
        return View(schedule);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var schedule = await _context.Schedules
            .Include(s => s.Cats)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (schedule == null) return NotFound();
        return View(schedule);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var schedule = await _context.Schedules
            .Include(s => s.Cats)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (schedule == null) return NotFound();

        _context.Schedules.Remove(schedule);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}