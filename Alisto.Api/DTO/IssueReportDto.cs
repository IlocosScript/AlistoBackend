using System;
using System.Collections.Generic;

namespace Alisto.Api.DTOs
{
    public class IssueReportDto
    {
        public string Id { get; set; }
        public string ReferenceNumber { get; set; }
        public string Category { get; set; }
        public string UrgencyLevel { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string? Coordinates { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string? AssignedDepartment { get; set; }
        public DateTime? EstimatedResolution { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<IssuePhotoDto> Photos { get; set; } = new List<IssuePhotoDto>();
    }
}