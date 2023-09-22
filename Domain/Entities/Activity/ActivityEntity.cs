using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Activity;

[Table("Activity")]
public class ActivityEntity : BaseEntity
{
    public string Name { get; set; }
    public int Status { get; set; }
}
