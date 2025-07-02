using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alisto.Api.DTOs
{
    public class CreateIssueReportRequest
    {
        [Required]
        public string Category { get; set; }

        [Required]
        public string UrgencyLevel { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        public string? Coordinates { get; set; }

        public string? ContactInfo { get; set; }

        public List<string> PhotoIds { get; set; } = new List<string>();
    }
}