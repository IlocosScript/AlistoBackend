using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alisto.Api.Enums;

namespace Alisto.Api.Models
{
    public class AppFeedback
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? UserId { get; set; }

        public FeedbackType Type { get; set; }

        [Required]
        [MaxLength(200)]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        [Range(1, 5)]
        public int? Rating { get; set; }

        [MaxLength(255)]
        public string? ContactEmail { get; set; }

        public FeedbackStatus Status { get; set; } = FeedbackStatus.New;

        public string? Response { get; set; }

        [MaxLength(100)]
        public string? RespondedBy { get; set; }

        public DateTime? RespondedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}