using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;

namespace CafeWithCats.Controllers;

[ApiController]
[Route("[controller]")]
public class CatsController : ControllerBase
{
    private readonly CafeContext _context;

    public CatsController(CafeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_context.Cats.ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var cat = _context.Cats.Find(id);
        return cat == null ? NotFound() : Ok(cat);
    }

    [HttpPost]
    public IActionResult Create(Cat cat)
    {
        _context.Cats.Add(cat);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = cat.Id }, cat);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Cat cat)
    {
        _context.Cats.Update(cat);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var cat = _context.Cats.Find(id);
        if (cat == null) return NotFound();
        _context.Cats.Remove(cat);
        _context.SaveChanges();
        return NoContent();
    }
}