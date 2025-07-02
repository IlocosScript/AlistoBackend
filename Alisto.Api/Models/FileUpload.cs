using System;
using System.ComponentModel.DataAnnotations;

namespace Alisto.Api.Models
{
    public class FileUpload
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(255)]
        public string OriginalFileName { get; set; }

        [Required]
        [MaxLength(500)]
        public string FilePath { get; set; }

        public long FileSize { get; set; }

        [Required]
        [MaxLength(100)]
        public string MimeType { get; set; }

        public string? UploadedBy { get; set; }

        [MaxLength(100)]
        public string? EntityType { get; set; }

        [MaxLength(100)]
        public string? EntityId { get; set; }

        public bool IsTemporary { get; set; } = false;

        public DateTime? ExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}