using Domain.Common.BaseEntity;
using Domain.Entities.Transaction;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Activity;

[Table("Activity")]
public class ActivityEntity : BaseEntity
{
    public string Name { get; set; }
    public int Status { get; set; }
    // 1 Activity Type - M Activity
    public Guid ActivityTypeId { get; set; }
    public ActivityType ActivityType { get; set; }
    // 1 Transaction - 1 or M Activity
    public Guid TransactionId { get; set; }
    public TransactionEntity Transaction { get;set; }
}
