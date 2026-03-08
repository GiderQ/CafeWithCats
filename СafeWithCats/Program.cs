using CafeWithCats.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<CafeContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/people", (CafeContext db) => db.Persons.ToList());
app.MapGet("/clients", (CafeContext db) => db.Clients.ToList());
app.MapGet("/staff", (CafeContext db) => db.Staff.ToList());
app.MapGet("/cats", (CafeContext db) => db.Cats.ToList());
app.MapGet("/cafes", (CafeContext db) => db.Cafes.ToList());
app.MapGet("/dishes", (CafeContext db) => db.Dishes.ToList());
app.MapGet("/drinks", (CafeContext db) => db.Drinks.ToList());
app.MapGet("/menus", (CafeContext db) => db.Menus.ToList());
app.MapGet("/persons", (CafeContext db) => db.Persons.ToList());
app.MapGet("/reservations", (CafeContext db) => db.Reservations.ToList());
app.MapGet("/shedules", (CafeContext db) => db.Shedules.ToList());

app.Run();