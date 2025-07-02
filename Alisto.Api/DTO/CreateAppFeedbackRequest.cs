using System.ComponentModel.DataAnnotations;

namespace Alisto.Api.DTOs
{
    public class CreateAppFeedbackRequest
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        [Range(1, 5)]
        public int? Rating { get; set; }

        public string? ContactEmail { get; set; }
    }
}