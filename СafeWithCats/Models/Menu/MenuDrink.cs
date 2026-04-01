using System.ComponentModel.DataAnnotations.Schema;
namespace CafeWithCats.Models;

[Table("menu_drinks")]
public class MenuDrink
{
    [Column("menu_id")]
    public int MenuId { get; set; }
    public Menu Menu { get; set; }

    [Column("drink_id")]
    public int DrinkId { get; set; }
    public Drink Drink { get; set; }
}