using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;

namespace CafeWithCats.Controllers;

[ApiController]
[Route("[controller]")]
public class MenusController : ControllerBase
{
    private readonly CafeContext _context;

    public MenusController(CafeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_context.Menus.ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var menu = _context.Menus.Find(id);
        return menu == null ? NotFound() : Ok(menu);
    }

    [HttpPost]
    public IActionResult Create(Menu menu)
    {
        _context.Menus.Add(menu);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = menu.Id }, menu);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Menu menu)
    {
        _context.Menus.Update(menu);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var menu = _context.Menus.Find(id);
        if (menu == null) return NotFound();
        _context.Menus.Remove(menu);
        _context.SaveChanges();
        return NoContent();
    }
}