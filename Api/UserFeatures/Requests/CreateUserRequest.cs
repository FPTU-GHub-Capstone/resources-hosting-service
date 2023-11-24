using AutoWrapper.Filters;
using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreateUserRequest : IMapTo<UserEntity>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Uid { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Avatar { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(03|05|07|08|09)\d{8}$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }
        public Guid? GameId { get; set; }
    }
}
