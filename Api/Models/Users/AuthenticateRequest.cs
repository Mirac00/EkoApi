// Importowanie niezbędnych przestrzeni nazw
using System.ComponentModel.DataAnnotations;

namespace Api.Models.Users;

public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
