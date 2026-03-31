using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWithCats.Models;

[Table("menus")]
public class Menu : Entity
{
    [Column("language")]
    public bool Language { get; set; }

    [Column("dish_id")]
    public int DishId { get; set; }
    public Dish Dish { get; set; }

    [Column("drink_id")]
    public int DrinkId { get; set; }
    public Drink Drink { get; set; }

    [Column("cafe_id")]
    public int CafeId { get; set; }
    public Cafe Cafe { get; set; }   
}