using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWithCats.Models;

[Table("persons")]
public class Person : Entity
{
    [Column("lastname")]
    public string LastName { get; set; }

    [Column("firstname")]
    public string FirstName { get; set; }

    [Column("age")]
    public int Age { get; set; }

    [Column("gender")]
    public string Gender { get; set; }
}