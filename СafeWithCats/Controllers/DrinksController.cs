using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeWithCats.Controllers;

[ApiController]
[Route("[controller]")]
public class DrinksController : ControllerBase
{
    private readonly CafeContext _context;

    public DrinksController(CafeContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var drinks = await _context.Drinks.ToListAsync();
        return Ok(drinks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var drink = await _context.Drinks.FindAsync(id);
        if (drink == null)
            return NotFound();
        return Ok(drink);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Drink drink)
    {
        _context.Drinks.Add(drink);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = drink.Id }, drink);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Drink updatedDrink)
    {
        if (id != updatedDrink.Id)
            return BadRequest("ID mismatch");

        var drink = await _context.Drinks.FindAsync(id);
        if (drink == null)
            return NotFound();

        drink.Ingredients = updatedDrink.Ingredients;
        drink.IsAlcoholic = updatedDrink.IsAlcoholic;
        drink.Price = updatedDrink.Price;
        drink.Volume = updatedDrink.Volume;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var drink = await _context.Drinks.FindAsync(id);
        if (drink == null)
            return NotFound();

        _context.Drinks.Remove(drink);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}