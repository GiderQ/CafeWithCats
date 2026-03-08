using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;

namespace CafeWithCats.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
    private readonly CafeContext _context;

    public PersonsController(CafeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_context.Persons.ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var person = _context.Persons.Find(id);
        return person == null ? NotFound() : Ok(person);
    }

    [HttpPost]
    public IActionResult Create(Person person)
    {
        _context.Persons.Add(person);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Person person)
    {
        _context.Persons.Update(person);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var person = _context.Persons.Find(id);
        if (person == null) return NotFound();
        _context.Persons.Remove(person);
        _context.SaveChanges();
        return NoContent();
    }
}