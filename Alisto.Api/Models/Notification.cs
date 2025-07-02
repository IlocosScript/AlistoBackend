using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alisto.Api.Enums;

namespace Alisto.Api.Models
{
    public class Notification
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        public NotificationType Type { get; set; }

        public bool IsRead { get; set; } = false;

        [MaxLength(500)]
        public string? ActionUrl { get; set; }

        public string? ActionData { get; set; } // JSON

        public DateTime? ExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ReadAt { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}