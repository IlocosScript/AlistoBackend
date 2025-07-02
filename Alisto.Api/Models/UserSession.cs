using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alisto.Api.Models
{
    public class UserSession
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Token { get; set; }

        [Required]
        [MaxLength(500)]
        public string RefreshToken { get; set; }

        public DateTime ExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(500)]
        public string? DeviceInfo { get; set; }

        [MaxLength(45)]
        public string? IpAddress { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}