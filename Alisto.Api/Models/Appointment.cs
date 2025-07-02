using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alisto.Api.Enums;

namespace Alisto.Api.Models
{
    public class Appointment
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string UserId { get; set; }

        public int ServiceId { get; set; }

        [Required]
        [MaxLength(20)]
        public string ReferenceNumber { get; set; }

        public DateTime AppointmentDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string AppointmentTime { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalFee { get; set; }

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public string? Notes { get; set; }

        // Applicant Information
        [Required]
        [MaxLength(100)]
        public string ApplicantFirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string ApplicantLastName { get; set; }

        [MaxLength(100)]
        public string? ApplicantMiddleName { get; set; }

        [Required]
        [MaxLength(20)]
        public string ApplicantContactNumber { get; set; }

        [MaxLength(255)]
        public string? ApplicantEmail { get; set; }

        [Required]
        [MaxLength(500)]
        public string ApplicantAddress { get; set; }

        // Service-specific data (JSON field)
        [Column(TypeName = "json")]
        public string ServiceSpecificData { get; set; } = "{}";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedAt { get; set; }

        public DateTime? CancelledAt { get; set; }

        [MaxLength(500)]
        public string? CancellationReason { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ServiceId")]
        public virtual CityService Service { get; set; }

        public virtual CivilRegistryRequest? CivilRegistryRequest { get; set; }
        public virtual BusinessPermitRequest? BusinessPermitRequest { get; set; }
        public virtual HealthServiceRequest? HealthServiceRequest { get; set; }
        public virtual EducationServiceRequest? EducationServiceRequest { get; set; }
        public virtual SocialServiceRequest? SocialServiceRequest { get; set; }
        public virtual TaxServiceRequest? TaxServiceRequest { get; set; }
    }
}