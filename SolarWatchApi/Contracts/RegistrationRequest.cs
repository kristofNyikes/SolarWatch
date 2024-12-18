using System.ComponentModel.DataAnnotations;

namespace SolarWatchApi.Contracts;

public record RegistrationRequest([Required] string Email, [Required] string Username, [Required] string Password);