using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWithCats.Models;

[Table("reservation_clients")]
public class ReservationClient
{
    [Column("reservation_id")]
    public int ReservationId { get; set; }

    [Column("client_id")]
    public int ClientId { get; set; }
}