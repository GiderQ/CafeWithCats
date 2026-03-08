using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWithCats.Models;

[Table("cafes")]
public class Cafe
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("address")]
    public string Address { get; set; }

    [Column("work_time")]
    public DateTime WorkTime { get; set; }

    [Column("cat_id")]
    public int CatId { get; set; }

    [Column("staff_id")]
    public int StaffId { get; set; }
}