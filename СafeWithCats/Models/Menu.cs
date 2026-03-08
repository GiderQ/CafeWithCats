using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWithCats.Models;

[Table("menus")]
public class Menu
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("language")]
    public bool Language { get; set; }

    [Column("drink_id")]
    public int DrinkId { get; set; }

    [Column("cafe_id")]
    public int CafeId { get; set; }

    [Column("dish_id")]
    public int DishId { get; set; }
}