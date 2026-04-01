using System.ComponentModel.DataAnnotations.Schema;
namespace CafeWithCats.Models;

[Table("dishes")]
public class Dish : Entity
{
    [Column("name")] public string Name { get; set; } = string.Empty;
    [Column("price")]
    public decimal Price { get; set; }
    [Column("weight")]
    public int Weight { get; set; }
    [Column("ingredients")]
    public string Ingredients { get; set; }
}