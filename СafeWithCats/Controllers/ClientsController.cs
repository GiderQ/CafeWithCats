using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;

namespace CafeWithCats.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientsController : ControllerBase
{
    private readonly CafeContext _context;

    public ClientsController(CafeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_context.Clients.ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var client = _context.Clients.Find(id);
        return client == null ? NotFound() : Ok(client);
    }

    [HttpPost]
    public IActionResult Create(Client client)
    {
        _context.Clients.Add(client);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = client.Id }, client);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Client client)
    {
        _context.Clients.Update(client);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var client = _context.Clients.Find(id);
        if (client == null) return NotFound();
        _context.Clients.Remove(client);
        _context.SaveChanges();
        return NoContent();
    }
}