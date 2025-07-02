using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alisto.Api.Models
{
    public class ServiceFeedback
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string AppointmentId { get; set; }

        [Required]
        public string UserId { get; set; }

        public int ServiceId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; } // 1-5 scale

        public string? Comment { get; set; }

        public bool IsAnonymous { get; set; } = false;

        public bool IsPublic { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ServiceId")]
        public virtual CityService Service { get; set; }
    }
}