using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alisto.Api.Enums;

namespace Alisto.Api.Models
{
    public class CivilRegistryRequest
    {
        [Key]
        public string AppointmentId { get; set; }

        public CivilDocumentType DocumentType { get; set; }

        [Required]
        [MaxLength(100)]
        public string Purpose { get; set; }

        public int NumberOfCopies { get; set; } = 1;

        [MaxLength(50)]
        public string? RegistryNumber { get; set; }

        public DateTime? RegistryDate { get; set; }

        [MaxLength(200)]
        public string? RegistryPlace { get; set; }

        // Navigation properties
        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }
    }
}