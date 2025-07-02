using System;
using System.ComponentModel.DataAnnotations;

namespace Alisto.Api.Models
{
    public class EmergencyHotline
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public bool IsEmergency { get; set; } = false;

        [Required]
        [MaxLength(100)]
        public string Department { get; set; }

        [Required]
        [MaxLength(100)]
        public string OperatingHours { get; set; }

        public bool IsActive { get; set; } = true;

        public int SortOrder { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}