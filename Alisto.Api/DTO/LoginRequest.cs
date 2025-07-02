using System.ComponentModel.DataAnnotations;

namespace Alisto.Api.DTOs
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public string? DeviceInfo { get; set; }
    }
}