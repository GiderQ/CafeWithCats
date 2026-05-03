using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;
using Microsoft.EntityFrameworkCore;

public class ReservationsController : Controller
{
    private readonly CafeContext _context;

    public ReservationsController(CafeContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var reservations = await _context.Reservations
                                         .Include(r => r.Cafe)
                                         .ToListAsync();
        return View(reservations);
    }

    public async Task<IActionResult> Details(int id)
    {
        var reservation = await _context.Reservations
                                        .Include(r => r.Cafe)
                                        .FirstOrDefaultAsync(r => r.Id == id);
        if (reservation == null) return NotFound();
        return View(reservation);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Cafes = _context.Cafes.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Reservation reservation)
    {
        if (!await _context.Cafes.AnyAsync(c => c.Id == reservation.CafeId))
            ModelState.AddModelError("CafeId", "Selected cafe does not exist.");

        if (ModelState.IsValid)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Cafes = _context.Cafes.ToList();
        return View(reservation);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null) return NotFound();

        ViewBag.Cafes = _context.Cafes.ToList();
        return View(reservation);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Reservation reservation)
    {
        if (id != reservation.Id) return BadRequest();

        if (!await _context.Cafes.AnyAsync(c => c.Id == reservation.CafeId))
            ModelState.AddModelError("CafeId", "Selected cafe does not exist.");

        if (ModelState.IsValid)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Cafes = _context.Cafes.ToList();
        return View(reservation);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var reservation = await _context.Reservations
                                        .Include(r => r.Cafe)
                                        .FirstOrDefaultAsync(r => r.Id == id);
        if (reservation == null) return NotFound();
        return View(reservation);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null) return NotFound();

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}