using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWithCats.Models;

[Table("dishes")]
public class Dish
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("weight")]
    public int Weight { get; set; }

    [Column("ingredients")]
    public string Ingredients { get; set; }
}