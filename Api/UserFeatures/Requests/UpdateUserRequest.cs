using AutoWrapper.Filters;
using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateUserRequest : IMapTo<UserEntity>
    {
        
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Avatar { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        [RegularExpression(@"^(03|05|07|08|09)\d{8}$", ErrorMessage = "Invalid phone number")]
        public string? Phone { get; set; }
        [Range(0,2)]
        public int? Status { get; set; }
    }
}
