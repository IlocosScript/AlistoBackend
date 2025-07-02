using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alisto.Api.Enums;

namespace Alisto.Api.Models
{
    public class ServiceStatistics
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public int ServiceId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ServiceName { get; set; }

        public int TotalAppointments { get; set; }

        public int CompletedAppointments { get; set; }

        public int CancelledAppointments { get; set; }

        public int AverageProcessingTime { get; set; } // in hours

        [Column(TypeName = "decimal(3,2)")]
        public decimal? CustomerSatisfactionRating { get; set; }

        public StatisticsPeriod Period { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}