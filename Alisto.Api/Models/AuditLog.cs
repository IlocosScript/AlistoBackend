using System;
using System.ComponentModel.DataAnnotations;

namespace Alisto.Api.Models
{
    public class AuditLog
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Action { get; set; }

        [Required]
        [MaxLength(100)]
        public string EntityType { get; set; }

        [Required]
        [MaxLength(100)]
        public string EntityId { get; set; }

        public string? OldValues { get; set; } // JSON

        public string? NewValues { get; set; } // JSON

        [MaxLength(45)]
        public string? IpAddress { get; set; }

        [MaxLength(500)]
        public string? UserAgent { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}