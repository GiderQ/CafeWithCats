using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWithCats.Models;

[Table("persons")]
public class Person
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("lastname")]
    public string LastName { get; set; }

    [Column("firstname")]
    public string FirstName { get; set; }

    [Column("age")]
    public int Age { get; set; }

    [Column("gender")]
    public string Gender { get; set; }
}