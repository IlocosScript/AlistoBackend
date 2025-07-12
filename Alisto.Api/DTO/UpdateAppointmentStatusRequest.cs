using Alisto.Api.Enums;

namespace Alisto.Api.DTOs
{
    public class UpdateAppointmentStatusRequest
    {
        public AppointmentStatus Status { get; set; }
        public string? Notes { get; set; }
    }
} 