using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alisto.Api.Enums;

namespace Alisto.Api.Models
{
    public class IssueReport
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? UserId { get; set; } // Optional for anonymous reports

        [Required]
        [MaxLength(20)]
        public string ReferenceNumber { get; set; }

        public IssueCategory Category { get; set; }

        public UrgencyLevel UrgencyLevel { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [MaxLength(500)]
        public string Location { get; set; }

        [MaxLength(100)]
        public string? Coordinates { get; set; } // GPS coordinates

        [MaxLength(200)]
        public string? ContactInfo { get; set; }

        public IssueStatus Status { get; set; } = IssueStatus.Submitted;

        public Priority Priority { get; set; }

        [MaxLength(100)]
        public string? AssignedDepartment { get; set; }

        [MaxLength(100)]
        public string? AssignedTo { get; set; }

        public DateTime? EstimatedResolution { get; set; }

        public DateTime? ActualResolution { get; set; }

        public string? ResolutionNotes { get; set; }

        public bool PubliclyVisible { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        public virtual ICollection<IssuePhoto> Photos { get; set; } = new List<IssuePhoto>();
        public virtual ICollection<IssueUpdate> Updates { get; set; } = new List<IssueUpdate>();
    }
}