using System.Collections.Generic;

namespace Alisto.Api.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalUsers { get; set; }
        public int ActiveAppointments { get; set; }
        public int PendingIssues { get; set; }
        public int CompletedProjects { get; set; }
        public decimal CustomerSatisfactionRating { get; set; }
        public List<ServiceUsageDto> TopServices { get; set; } = new List<ServiceUsageDto>();
    }
}