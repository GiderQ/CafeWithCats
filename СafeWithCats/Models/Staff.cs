using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWithCats.Models;

[Table("staff")]
public class Staff : Entity
{
    [Column("work_phone")]
    public string WorkPhone { get; set; }

    [Column("work_email")]
    public string WorkEmail { get; set; }

    [Column("salary")]
    public decimal Salary { get; set; }

    [Column("position")]
    public string Position { get; set; }

    [Column("schedule_id")]
    public int ScheduleId { get; set; }
}