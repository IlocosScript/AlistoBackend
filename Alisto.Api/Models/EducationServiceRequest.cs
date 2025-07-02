using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alisto.Api.Enums;

namespace Alisto.Api.Models
{
    public class EducationServiceRequest
    {
        [Key]
        public string AppointmentId { get; set; }

        public EducationServiceType ServiceType { get; set; }

        [Required]
        [MaxLength(100)]
        public string StudentFirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string StudentLastName { get; set; }

        [MaxLength(100)]
        public string? StudentMiddleName { get; set; }

        public DateTime StudentBirthDate { get; set; }

        [Required]
        [MaxLength(500)]
        public string StudentAddress { get; set; }

        [MaxLength(50)]
        public string? GradeLevel { get; set; }

        [MaxLength(200)]
        public string? SchoolName { get; set; }

        [MaxLength(10)]
        public string? Gpa { get; set; }

        [MaxLength(100)]
        public string? ScholarshipType { get; set; }

        [MaxLength(100)]
        public string? VocationalCourse { get; set; }

        [Required]
        [MaxLength(100)]
        public string ParentName { get; set; }

        [MaxLength(100)]
        public string? ParentOccupation { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? MonthlyIncome { get; set; }

        public int? FamilySize { get; set; }

        // Navigation properties
        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }
    }
}