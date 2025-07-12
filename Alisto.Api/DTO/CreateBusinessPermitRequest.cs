using System.ComponentModel.DataAnnotations;
using Alisto.Api.Enums;

namespace Alisto.Api.DTOs
{
    public class CreateBusinessPermitRequest
    {
        [Required]
        public string AppointmentId { get; set; }

        [Required]
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

        public decimal? CapitalInvestment { get; set; }
    }
} 