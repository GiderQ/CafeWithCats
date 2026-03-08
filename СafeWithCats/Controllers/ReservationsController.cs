using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;

namespace CafeWithCats.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly CafeContext _context;

    public ReservationsController(CafeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_context.Reservations.ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var reservation = _context.Reservations.Find(id);
        return reservation == null ? NotFound() : Ok(reservation);
    }

    [HttpPost]
    public IActionResult Create(Reservation reservation)
    {
        _context.Reservations.Add(reservation);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Reservation reservation)
    {
        _context.Reservations.Update(reservation);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var reservation = _context.Reservations.Find(id);
        if (reservation == null) return NotFound();
        _context.Reservations.Remove(reservation);
        _context.SaveChanges();
        return NoContent();
    }
}