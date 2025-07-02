using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alisto.Api.Models
{
    public class IssuePhoto
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string IssueReportId { get; set; }

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(500)]
        public string FilePath { get; set; }

        public long FileSize { get; set; }

        [Required]
        [MaxLength(100)]
        public string MimeType { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("IssueReportId")]
        public virtual IssueReport IssueReport { get; set; }
    }
}