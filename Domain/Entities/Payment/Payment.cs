using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Payment;

[Table("Payment")]
public class Payment : BaseEntity
{
    public int Amount { get; set; }
    public DateTime Date { get; set;}
    public string Content { get; set;}
    public string BankCode { get; set;}
    public string Status { get; set;}
}
