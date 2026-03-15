using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWithCats.Models;

[Table("dishes")]
public class Dish : Entity
{
    [Column("price")]
    public decimal Price { get; set; }

    [Column("weight")]
    public int Weight { get; set; }

    [Column("ingredients")]
    public string Ingredients { get; set; }
}