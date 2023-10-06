using DomainLayer.Entities;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests;

public class UpdateUserRequest : IMapTo<UserEntity>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Avatar { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Code { get; set; }
    public int Status { get; set; }
    public float Balance { get; set; }
}
