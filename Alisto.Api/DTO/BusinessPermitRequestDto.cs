using System;
using Alisto.Api.Enums;

namespace Alisto.Api.DTOs
{
    public class BusinessPermitRequestDto
    {
        public string AppointmentId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public BusinessServiceType ServiceType { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public string BusinessAddress { get; set; }
        public string OwnerName { get; set; }
        public string? TinNumber { get; set; }
        public decimal? CapitalInvestment { get; set; }
        public AppointmentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 