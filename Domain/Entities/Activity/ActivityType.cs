using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Activity;

[Table("ActivityType")]
public class ActivityType : BaseEntity
{
    public string Name { get; set; }
}
