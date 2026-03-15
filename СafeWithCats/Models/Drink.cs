using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWithCats.Models;

[Table("drinks")]
public class Drink : Entity
{
    [Column("is_alcoholic")]
    public bool IsAlcoholic { get; set; }

    [Column("ingredients")]
    public string Ingredients { get; set; }

    [Column("volume")]
    public int Volume { get; set; }

    [Column("price")]
    public decimal Price { get; set; }
}