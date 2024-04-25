using System.ComponentModel.DataAnnotations;

namespace Vetproject.Contracts;

public record RegistrationRequest(
    [Required]string Email, 
    [Required]string Username, 
    [Required]string Password);
