using System;

namespace Alisto.Api.DTOs
{
    public class AppointmentDto
    {
        public string Id { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string Status { get; set; }
        public decimal TotalFee { get; set; }
        public string PaymentStatus { get; set; }
        public string ServiceName { get; set; }
        public string ServiceCategory { get; set; }
        public string ApplicantFirstName { get; set; }
        public string ApplicantLastName { get; set; }
        public string ApplicantContactNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}