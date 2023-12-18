using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class ResetRecordRequest
    {
        [Required]
        public Guid[] ids { get; set; }
    }
}
