using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alisto.Api.Enums;

namespace Alisto.Api.Models
{
    public class TaxServiceRequest
    {
        [Key]
        public string AppointmentId { get; set; }

        public TaxServiceType ServiceType { get; set; }

        public int TaxYear { get; set; }

        [MaxLength(100)]
        public string? PropertyType { get; set; }

        [MaxLength(500)]
        public string? PropertyAddress { get; set; }

        [MaxLength(50)]
        public string? PropertyTDN { get; set; }

        [MaxLength(200)]
        public string? BusinessName { get; set; }

        [MaxLength(20)]
        public string? BusinessTIN { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal? AssessedValue { get; set; }

        [MaxLength(100)]
        public string? ExemptionType { get; set; }

        // Navigation properties
        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }
    }
}