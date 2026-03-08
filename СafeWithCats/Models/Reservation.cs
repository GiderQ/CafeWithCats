using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWithCats.Models;

[Table("reservations")]
public class Reservation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("reserved_time")]
    public TimeSpan ReservedTime { get; set; }

    [Column("cafe_id")]
    public int CafeId { get; set; }
}