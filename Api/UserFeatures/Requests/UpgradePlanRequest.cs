using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpgradePlanRequest
    {
        [Required]
        [Range(0, 2)]
        public GamePlan GamePlan { get; set; }
    }
}