using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWithCats.Models;

[Table("schedules")]
public class Schedule : Entity
{
    [Column("start_time")]
    public DateTime ShiftStart { get; set; }

    [Column("end_time")]
    public DateTime ShiftEnd { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }

    [Column("weekday")]
    public string DayOfWeek { get; set; }
    
    public ICollection<Cat> Cats { get; set; }
}