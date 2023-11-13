using System.ComponentModel.DataAnnotations;

namespace WebApiLayer.UserFeatures.Requests;

public class RegisterRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    public string? ReenterPassword { get; set; }
}
