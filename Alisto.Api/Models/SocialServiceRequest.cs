using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alisto.Api.Enums;

namespace Alisto.Api.Models
{
    public class SocialServiceRequest
    {
        [Key]
        public string AppointmentId { get; set; }

        public SocialServiceType ServiceType { get; set; }

        [MaxLength(100)]
        public string? DisabilityType { get; set; }

        [MaxLength(100)]
        public string? AssistanceType { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? MonthlyIncome { get; set; }

        public int? FamilySize { get; set; }

        // Navigation properties
        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }
    }
}