using System;
using System.ComponentModel.DataAnnotations;
using Alisto.Api.Enums;

namespace Alisto.Api.Models
{
    public class Announcement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        public AnnouncementType Type { get; set; }

        public Priority Priority { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [MaxLength(100)]
        public string? TargetAudience { get; set; }

        [Required]
        [MaxLength(100)]
        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}