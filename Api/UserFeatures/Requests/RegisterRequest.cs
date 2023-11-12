using System.ComponentModel.DataAnnotations;

namespace WebApiLayer.UserFeatures.Requests;

public class LoginRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
