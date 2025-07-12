using Alisto.Api.Enums;

namespace Alisto.Api.DTOs
{
    public class UpdateIssueStatusRequest
    {
        public IssueStatus Status { get; set; }
        public string? AssignedDepartment { get; set; }
        public string? AssignedTo { get; set; }
        public DateTime? EstimatedResolution { get; set; }
        public string? ResolutionNotes { get; set; }
    }
} 