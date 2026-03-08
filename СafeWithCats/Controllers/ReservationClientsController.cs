using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;

namespace CafeWithCats.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationClientsController : ControllerBase
{
    private readonly CafeContext _context;

    public ReservationClientsController(CafeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_context.ReservationClients.ToList());

    [HttpGet("{reservationId}/{clientId}")]
    public IActionResult Get(int reservationId, int clientId)
    {
        var rc = _context.ReservationClients.Find(reservationId, clientId);
        return rc == null ? NotFound() : Ok(rc);
    }

    [HttpPost]
    public IActionResult Create(ReservationClient rc)
    {
        _context.ReservationClients.Add(rc);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { reservationId = rc.ReservationId, clientId = rc.ClientId }, rc);
    }

    [HttpDelete("{reservationId}/{clientId}")]
    public IActionResult Delete(int reservationId, int clientId)
    {
        var rc = _context.ReservationClients.Find(reservationId, clientId);
        if (rc == null) return NotFound();
        _context.ReservationClients.Remove(rc);
        _context.SaveChanges();
        return NoContent();
    }
}