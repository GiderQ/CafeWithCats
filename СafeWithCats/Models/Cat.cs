using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWithCats.Models;

[Table("cats")]
public class Cat
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("breed")]
    public string Breed { get; set; }

    [Column("gender")]
    public string Gender { get; set; }

    [Column("schedule_id")]
    public int ScheduleId { get; set; }
}