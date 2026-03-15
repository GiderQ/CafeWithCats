using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class CafesController : ControllerBase
{
    private readonly CafeContext _context;

    public CafesController(CafeContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult GetAll() => Ok(_context.Cafes.ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var cafe = _context.Cafes.Find(id);
        return cafe == null ? NotFound() : Ok(cafe);
    }

    [HttpPost]
    public IActionResult Create(Cafe cafe)
    {
        _context.Cafes.Add(cafe);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = cafe.Id }, cafe);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Cafe cafe)
    {
        _context.Cafes.Update(cafe);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var cafe = _context.Cafes.Find(id);
        if (cafe == null) return NotFound();
        _context.Cafes.Remove(cafe);
        _context.SaveChanges();
        return NoContent();
    }
}