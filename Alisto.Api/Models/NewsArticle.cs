using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Alisto.Api.Enums;

namespace Alisto.Api.Models
{
    public class NewsArticle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string? Summary { get; set; }

        [Required]
        public string FullContent { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        public DateTime? PublishedDate { get; set; }

        [MaxLength(20)]
        public string? PublishedTime { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; }

        [MaxLength(100)]
        public string? ExpectedAttendees { get; set; }

        [Required]
        public NewsCategory Category { get; set; }

        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        [Column(TypeName = "json")]
        public string Tags { get; set; } = "[]";

        [Required]
        public bool IsFeatured { get; set; } = false;

        [Required]
        public bool IsTrending { get; set; } = false;

        public int ViewCount { get; set; } = 0;

        public ContentStatus Status { get; set; } = ContentStatus.Draft;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Helper methods for Tags
        [NotMapped]
        public List<string> TagsList
        {
            get => JsonSerializer.Deserialize<List<string>>(Tags) ?? new List<string>();
            set => Tags = JsonSerializer.Serialize(value);
        }
    }
}