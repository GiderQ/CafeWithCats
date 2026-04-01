using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWithCats.Models;

[Table("menus")]
public class Menu : Entity
{
    [Column("language")]
    public string Language { get; set; }
    [Column("cafe_id")]
    public int CafeId { get; set; }
    
    public ICollection<MenuDish> MenuDishes { get; set; }
    public ICollection<MenuDrink> MenuDrinks { get; set; }
}