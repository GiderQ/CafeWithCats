using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeWithCats.Models;
using CafeWithCats.Models.ViewModels;
namespace CafeWithCats.Controllers;

public class MenusController : Controller
{
    private readonly CafeContext _context;

    public MenusController(CafeContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var menus = await _context.Menus.ToListAsync();
        return View(menus);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var menu = await _context.Menus
            .Include(m => m.MenuDishes).ThenInclude(md => md.Dish)
            .Include(m => m.MenuDrinks).ThenInclude(md => md.Drink)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (menu == null) return NotFound();
        return View(menu);
    }

    public async Task<IActionResult> Create()
    {
        var vm = new MenuViewModel
        {
            AvailableDishes = await _context.Dishes.ToListAsync(),
            AvailableDrinks = await _context.Drinks.ToListAsync(),
            AvailableCafes = await _context.Cafes.ToListAsync()
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MenuViewModel vm)
    {
        ModelState.Remove("Menu.MenuDishes");
        ModelState.Remove("Menu.MenuDrinks");

        if (!ModelState.IsValid)
        {
            foreach (var key in ModelState.Keys)
            foreach (var error in ModelState[key]!.Errors)
                Console.WriteLine($"KEY: {key} | ERROR: {error.ErrorMessage}");

            vm.AvailableDishes = await _context.Dishes.ToListAsync();
            vm.AvailableDrinks = await _context.Drinks.ToListAsync();
            vm.AvailableCafes = await _context.Cafes.ToListAsync();
            return View(vm);
        }

        _context.Add(vm.Menu);
        await _context.SaveChangesAsync();

        foreach (var dishId in vm.SelectedDishIds)
            _context.MenuDishes.Add(new MenuDish { MenuId = vm.Menu.Id, DishId = dishId });

        foreach (var drinkId in vm.SelectedDrinkIds)
            _context.MenuDrinks.Add(new MenuDrink { MenuId = vm.Menu.Id, DrinkId = drinkId });

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

public async Task<IActionResult> Edit(int? id)
{
    if (id == null) return NotFound();

    var menu = await _context.Menus
        .Include(m => m.MenuDishes)
        .Include(m => m.MenuDrinks)
        .FirstOrDefaultAsync(m => m.Id == id);

    if (menu == null) return NotFound();

    var vm = new MenuViewModel
    {
        Menu = menu,
        SelectedDishIds = menu.MenuDishes.Select(md => md.DishId).ToList(),
        SelectedDrinkIds = menu.MenuDrinks.Select(md => md.DrinkId).ToList(),
        AvailableDishes = await _context.Dishes.ToListAsync(),
        AvailableDrinks = await _context.Drinks.ToListAsync(),
        AvailableCafes = await _context.Cafes.ToListAsync()
    };
    return View(vm);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, MenuViewModel vm)
{
    if (id != vm.Menu.Id) return NotFound();

    if (ModelState.IsValid)
    {
        _context.Update(vm.Menu);

        var oldDishes = _context.MenuDishes.Where(md => md.MenuId == id);
        _context.MenuDishes.RemoveRange(oldDishes);
        foreach (var dishId in vm.SelectedDishIds)
            _context.MenuDishes.Add(new MenuDish { MenuId = id, DishId = dishId });

        var oldDrinks = _context.MenuDrinks.Where(md => md.MenuId == id);
        _context.MenuDrinks.RemoveRange(oldDrinks);
        foreach (var drinkId in vm.SelectedDrinkIds)
            _context.MenuDrinks.Add(new MenuDrink { MenuId = id, DrinkId = drinkId });

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    vm.AvailableDishes = await _context.Dishes.ToListAsync();
    vm.AvailableDrinks = await _context.Drinks.ToListAsync();
    return View(vm);
}

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var menu = await _context.Menus
            .FirstOrDefaultAsync(m => m.Id == id);

        if (menu == null) return NotFound();
        return View(menu);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var menu = await _context.Menus.FindAsync(id);
        if (menu != null)
        {
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}