using System;
using System.ComponentModel.DataAnnotations;

namespace Alisto.Api.DTOs
{
    public class CreateAppointmentRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string AppointmentTime { get; set; }

        public string? Notes { get; set; }

        [Required]
        public string ApplicantFirstName { get; set; }

        [Required]
        public string ApplicantLastName { get; set; }

        public string? ApplicantMiddleName { get; set; }

        [Required]
        public string ApplicantContactNumber { get; set; }

        public string? ApplicantEmail { get; set; }

        [Required]
        public string ApplicantAddress { get; set; }

        public string? ServiceSpecificData { get; set; }
    }
}