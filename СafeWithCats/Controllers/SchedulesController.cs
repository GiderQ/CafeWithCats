using Microsoft.AspNetCore.Mvc;
using CafeWithCats.Models;

namespace CafeWithCats.Controllers;

[ApiController]
[Route("[controller]")]
public class SchedulesController : ControllerBase
{
    private readonly CafeContext _context;

    public SchedulesController(CafeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_context.Schedules.ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var schedule = _context.Schedules.Find(id);
        return schedule == null ? NotFound() : Ok(schedule);
    }

    [HttpPost]
    public IActionResult Create(Schedule schedule)
    {
        _context.Schedules.Add(schedule);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = schedule.Id }, schedule);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Schedule schedule)
    {
        _context.Schedules.Update(schedule);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var schedule = _context.Schedules.Find(id);
        if (schedule == null) return NotFound();
        _context.Schedules.Remove(schedule);
        _context.SaveChanges();
        return NoContent();
    }
}