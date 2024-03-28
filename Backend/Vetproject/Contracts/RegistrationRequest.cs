using System.ComponentModel.DataAnnotations;

namespace Vetproject.Contracts;

public record RegistrationRequest(
    [Required] string Email,
    [Required] string UserName,
    [Required] string Password,
    [Required] DateTime BirthDate,
    [Required] string Address
);