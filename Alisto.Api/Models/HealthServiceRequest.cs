using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alisto.Api.Enums;

namespace Alisto.Api.Models
{
    public class HealthServiceRequest
    {
        [Key]
        public string AppointmentId { get; set; }

        public HealthServiceType ServiceType { get; set; }

        [MaxLength(100)]
        public string? CertificatePurpose { get; set; }

        [MaxLength(100)]
        public string? AssistanceType { get; set; }

        public string? MedicalHistory { get; set; }

        public string? CurrentMedications { get; set; }

        [MaxLength(500)]
        public string? Allergies { get; set; }

        public string? Symptoms { get; set; }

        [MaxLength(100)]
        public string? PreferredDoctor { get; set; }

        // Navigation properties
        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }
    }
}