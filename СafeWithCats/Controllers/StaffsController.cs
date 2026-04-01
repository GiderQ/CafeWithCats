using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeWithCats.Models;

namespace CafeWithCats.Controllers;

public class StaffsController : Controller
{
    private readonly CafeContext _context;

    public StaffsController(CafeContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var staffs = _context.Staffs.ToList();
        return View(staffs);
    }

    public IActionResult Details(int id)
    {
        var staff = _context.Staffs.Find(id);
        if (staff == null) return NotFound();
        return View(staff);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Staff staff)
    {
        if (ModelState.IsValid)
        {
            _context.Staffs.Add(staff);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(staff);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var staff = _context.Staffs.Find(id);
        if (staff == null) return NotFound();
        return View(staff);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Staff staff)
    {
        if (id != staff.Id) return BadRequest();

        if (ModelState.IsValid)
        {
            _context.Staffs.Update(staff);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(staff);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var staff = _context.Staffs.Find(id);
        if (staff == null) return NotFound();
        return View(staff);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var staff = _context.Staffs.Find(id);
        if (staff == null) return NotFound();

        _context.Staffs.Remove(staff);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}