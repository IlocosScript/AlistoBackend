using System.ComponentModel.DataAnnotations;

namespace Alisto.Api.DTOs
{
    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}