using System;
using System.ComponentModel.DataAnnotations;

namespace Alisto.Api.Models
{
    public class AppUsageStatistics
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime Date { get; set; }

        public int TotalUsers { get; set; }

        public int ActiveUsers { get; set; }

        public int NewRegistrations { get; set; }

        public int AppointmentsBooked { get; set; }

        public int IssuesReported { get; set; }

        public int NewsViews { get; set; }

        [MaxLength(100)]
        public string MostUsedService { get; set; }

        public int PeakUsageHour { get; set; }
    }
}