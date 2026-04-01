using System.ComponentModel.DataAnnotations.Schema;
namespace CafeWithCats.Models;

[Table("menu_dishes")]
public class MenuDish
{
    [Column("menu_id")]
    public int MenuId { get; set; }
    public Menu Menu { get; set; }

    [Column("dish_id")]
    public int DishId { get; set; }
    public Dish Dish { get; set; }
}