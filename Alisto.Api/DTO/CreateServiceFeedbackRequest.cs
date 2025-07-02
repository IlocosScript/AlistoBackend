using System.ComponentModel.DataAnnotations;

namespace Alisto.Api.DTOs
{
    public class CreateServiceFeedbackRequest
    {
        [Required]
        public string AppointmentId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }

        public bool IsAnonymous { get; set; } = false;
    }
}