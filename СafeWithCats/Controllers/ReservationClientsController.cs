using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;
using Microsoft.EntityFrameworkCore;

public class ReservationClientsController : Controller
{
    private readonly CafeContext _context;

    public ReservationClientsController(CafeContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var reservationClients = await _context.ReservationClients
                                               .Include(rc => rc.Reservation)
                                               .Include(rc => rc.Client)
                                               .ToListAsync();
        return View(reservationClients);
    }

    public async Task<IActionResult> Details(int reservationId, int clientId)
    {
        var reservationClient = await _context.ReservationClients
                                             .Include(rc => rc.Reservation)
                                             .Include(rc => rc.Client)
                                             .FirstOrDefaultAsync(rc => rc.ReservationId == reservationId && rc.ClientId == clientId);

        if (reservationClient == null) return NotFound();
        return View(reservationClient);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Reservations = _context.Reservations.ToList();
        ViewBag.Clients = _context.Clients.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ReservationClient reservationClient)
    {
        if (!await _context.Reservations.AnyAsync(r => r.Id == reservationClient.ReservationId))
        {
            ModelState.AddModelError("ReservationId", "Selected reservation does not exist.");
        }
        if (!await _context.Clients.AnyAsync(c => c.Id == reservationClient.ClientId))
        {
            ModelState.AddModelError("ClientId", "Selected client does not exist.");
        }

        if (ModelState.IsValid)
        {
            _context.ReservationClients.Add(reservationClient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Reservations = _context.Reservations.ToList();
        ViewBag.Clients = _context.Clients.ToList();
        return View(reservationClient);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int reservationId, int clientId)
    {
        var reservationClient = await _context.ReservationClients .Include(rc => rc.Reservation)
                                             .Include(rc => rc.Client)
                                             .FirstOrDefaultAsync(rc => rc.ReservationId == reservationId && rc.ClientId == clientId);

        if (reservationClient == null) return NotFound();
        return View(reservationClient);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int reservationId, int clientId)
    {
        var reservationClient = await _context.ReservationClients
                                             .FirstOrDefaultAsync(rc => rc.ReservationId == reservationId && rc.ClientId == clientId);

        if (reservationClient == null) return NotFound();

        _context.ReservationClients.Remove(reservationClient);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}