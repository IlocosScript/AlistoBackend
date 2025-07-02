using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alisto.Api.Enums;

namespace Alisto.Api.Models
{
    public class BusinessPermitRequest
    {
        [Key]
        public string AppointmentId { get; set; }

        public BusinessServiceType ServiceType { get; set; }

        [Required]
        [MaxLength(200)]
        public string BusinessName { get; set; }

        [Required]
        [MaxLength(100)]
        public string BusinessType { get; set; }

        [Required]
        [MaxLength(500)]
        public string BusinessAddress { get; set; }

        [Required]
        [MaxLength(100)]
        public string OwnerName { get; set; }

        [MaxLength(20)]
        public string? TinNumber { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal? CapitalInvestment { get; set; }

        // Navigation properties
        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }
    }
}