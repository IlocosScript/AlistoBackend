using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Alisto.Api.Models
{
    public class TouristSpot
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        [Column(TypeName = "decimal(3,2)")]
        public decimal Rating { get; set; } = 5.0m;

        [Required]
        [MaxLength(200)]
        public string Location { get; set; }

        [MaxLength(100)]
        public string? Coordinates { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string? OpeningHours { get; set; }

        [MaxLength(100)]
        public string? EntryFee { get; set; }

        [Column(TypeName = "json")]
        public string Highlights { get; set; } = "[]";

        [MaxLength(100)]
        public string? TravelTime { get; set; }

        public bool IsActive { get; set; } = true;

        public int ViewCount { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Helper methods for Highlights
        [NotMapped]
        public List<string> HighlightsList
        {
            get => JsonSerializer.Deserialize<List<string>>(Highlights) ?? new List<string>();
            set => Highlights = JsonSerializer.Serialize(value);
        }
    }
}