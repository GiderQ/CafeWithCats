using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;

namespace CafeWithCats.Controllers;

[ApiController]
[Route("[controller]")]
public class DishesController : ControllerBase
{
    private readonly CafeContext _context;

    public DishesController(CafeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_context.Dishes.ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var dish = _context.Dishes.Find(id);
        return dish == null ? NotFound() : Ok(dish);
    }

    [HttpPost]
    public IActionResult Create(Dish dish)
    {
        _context.Dishes.Add(dish);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = dish.Id }, dish);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Dish dish)
    {
        _context.Dishes.Update(dish);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var dish = _context.Dishes.Find(id);
        if (dish == null) return NotFound();
        _context.Dishes.Remove(dish);
        _context.SaveChanges();
        return NoContent();
    }
}