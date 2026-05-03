using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeWithCats.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;


namespace CafeWithCats.Controllers;

[Authorize(Roles = "Admin")]
public class ChartsController : Controller
{
    private readonly CafeContext _context;

    public ChartsController(CafeContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var drinks = await _context.Drinks.ToListAsync();

        ViewBag.DrinkNames = drinks.Select(d => d.Name).ToList();
        ViewBag.DrinkPrices = drinks.Select(d => d.Price).ToList();
        ViewBag.AlcoholicCount = drinks.Count(d => d.IsAlcoholic);
        ViewBag.NonAlcoholicCount = drinks.Count(d => !d.IsAlcoholic);
        return View();
    }

    public async Task<IActionResult> Export()
    {
        var drinks = await _context.Drinks.ToListAsync();

        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("Drinks");

        ws.Cell(1, 1).Value = "ID";
        ws.Cell(1, 2).Value = "Name";
        ws.Cell(1, 3).Value = "Price (₴)";
        ws.Cell(1, 4).Value = "Volume (ml)";
        ws.Cell(1, 5).Value = "Alcoholic";
        ws.Cell(1, 6).Value = "Ingredients";

        var headerRow = ws.Range(1, 1, 1, 6);
        headerRow.Style.Font.Bold = true;
        headerRow.Style.Fill.BackgroundColor = XLColor.FromHtml("#6b3f1f");
        headerRow.Style.Font.FontColor = XLColor.White;

        for (int i = 0; i < drinks.Count; i++)
        {
            var d = drinks[i];
            ws.Cell(i + 2, 1).Value = d.Id;
            ws.Cell(i + 2, 2).Value = d.Name;
            ws.Cell(i + 2, 3).Value = d.Price;
            ws.Cell(i + 2, 4).Value = d.Volume;
            ws.Cell(i + 2, 5).Value = d.IsAlcoholic ? "Yes" : "No";
            ws.Cell(i + 2, 6).Value = d.Ingredients;
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Seek(0, SeekOrigin.Begin);

        return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "drinks_report.xlsx");
    }

    public IActionResult Import()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Import(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            ViewBag.Error = "Please select a valid Excel file.";
            return View();
        }

        if (!file.FileName.EndsWith(".xlsx"))
        {
            ViewBag.Error = "Only .xlsx files are supported.";
            return View();
        }

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        stream.Seek(0, SeekOrigin.Begin);

        using var workbook = new XLWorkbook(stream);
        var ws = workbook.Worksheet(1);
        var rows = ws.RangeUsed().RowsUsed().Skip(1);

        int imported = 0;
        int skipped = 0;

        foreach (var row in rows)
        {
            var name = row.Cell(2).GetString().Trim();
            if (string.IsNullOrEmpty(name)) continue;

            if (await _context.Drinks.AnyAsync(d => d.Name == name))
            {
                skipped++;
                continue;
            }

            var drink = new Drink
            {
                Name = name,
                Price = row.Cell(3).GetValue<decimal>(),
                Volume = row.Cell(4).GetValue<int>(),
                IsAlcoholic = row.Cell(5).GetString().Trim().ToLower() == "yes",
                Ingredients = row.Cell(6).GetString().Trim()
            };

            _context.Drinks.Add(drink);
            imported++;
        }

        await _context.SaveChangesAsync();

        ViewBag.Success = $"Import complete: {imported} added, {skipped} skipped (duplicates).";
        return View();
    }
}