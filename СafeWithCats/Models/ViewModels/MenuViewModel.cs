namespace CafeWithCats.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class MenuViewModel
{
    public Menu Menu { get; set; } = new();

    public List<int> SelectedDishIds { get; set; } = new();
    public List<int> SelectedDrinkIds { get; set; } = new();

    [ValidateNever]
    public IEnumerable<Dish> AvailableDishes { get; set; } = new List<Dish>();

    [ValidateNever]
    public IEnumerable<Drink> AvailableDrinks { get; set; } = new List<Drink>();

    [ValidateNever]
    public IEnumerable<Cafe> AvailableCafes { get; set; } = new List<Cafe>();
}