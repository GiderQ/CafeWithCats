using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;

namespace CafeWithCats.Controllers;

[ApiController]
[Route("[controller]")]
public class StaffController : ControllerBase
{
    private readonly CafeContext _context;

    public StaffController(CafeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_context.Staff.ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var staff = _context.Staff.Find(id);
        return staff == null ? NotFound() : Ok(staff);
    }

    [HttpPost]
    public IActionResult Create(Staff staff)
    {
        _context.Staff.Add(staff);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = staff.Id }, staff);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Staff staff)
    {
        _context.Staff.Update(staff);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var staff = _context.Staff.Find(id);
        if (staff == null) return NotFound();
        _context.Staff.Remove(staff);
        _context.SaveChanges();
        return NoContent();
    }
}