using System.ComponentModel.DataAnnotations;

namespace WebApiLayer.UserFeatures.Requests;

public class RegisterRequest
{
    [Required]
    [MinLength(6)]
    [MaxLength(40)]
    public string Username { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(40)]
    public string Password { get; set; }

    [MinLength(6)]
    [MaxLength(40)]
    public string? ReenterPassword { get; set; }
}
