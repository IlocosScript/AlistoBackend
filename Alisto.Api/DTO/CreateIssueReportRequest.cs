using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alisto.Api.Enums;

namespace Alisto.Api.DTOs
{
    public class CreateIssueReportRequest
    {
        public string? UserId { get; set; }

        [Required]
        public IssueCategory Category { get; set; }

        [Required]
        public UrgencyLevel UrgencyLevel { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        public string? Coordinates { get; set; }

        public string? ContactInfo { get; set; }

        [Required]
        public Priority Priority { get; set; }

        public bool PubliclyVisible { get; set; } = true;

        public List<string> PhotoIds { get; set; } = new List<string>();
    }
}