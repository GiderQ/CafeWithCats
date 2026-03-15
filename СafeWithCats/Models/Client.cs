using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWithCats.Models;

[Table("clients")]
public class Client : Entity
{
    [Column("phone")]
    public string Phone { get; set; }

    [Column("email")]
    public string Email { get; set; }
}