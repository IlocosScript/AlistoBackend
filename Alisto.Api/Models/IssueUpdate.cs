using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alisto.Api.Enums;

namespace Alisto.Api.Models
{
    public class IssueUpdate
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string IssueReportId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UpdatedBy { get; set; }

        public IssueUpdateType UpdateType { get; set; }

        [Required]
        public string Message { get; set; }

        public bool IsPublic { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("IssueReportId")]
        public virtual IssueReport IssueReport { get; set; }
    }
}