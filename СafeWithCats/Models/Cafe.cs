using System.ComponentModel.DataAnnotations.Schema;

namespace CafeWithCats.Models;

[Table("cafes")]
public class Cafe : Entity
{
    [Column("address")]
    public string Address { get; set; } = null!;

    [Column("work_time")]
    public DateTime WorkTime { get; set; }

    [Column("cat_id")]
    public int CatId { get; set; }
    
    [Column("staff_id")]
    public int StaffId { get; set; }
    
    public Menu Menu { get; set; }

}