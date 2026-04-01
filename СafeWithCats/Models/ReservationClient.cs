using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWithCats.Models;

[Table("reservation_clients")]
public class ReservationClient
{
    [Column("reservation_id")]
    public int ReservationId { get; set; }

    [Column("client_id")]
    public int ClientId { get; set; }
    
    [ForeignKey(nameof(ReservationId))]
    public Reservation Reservation { get; set; } = null!;

    [ForeignKey(nameof(ClientId))]
    public Client Client { get; set; } = null!;
}