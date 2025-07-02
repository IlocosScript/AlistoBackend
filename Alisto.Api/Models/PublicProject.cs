using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alisto.Api.Enums;

namespace Alisto.Api.Models
{
    public class PublicProject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal Cost { get; set; }

        [Required]
        [MaxLength(200)]
        public string Contractor { get; set; }

        public ProjectStatus Status { get; set; }

        public int? Progress { get; set; } // Percentage 0-100

        public DateTime StartDate { get; set; }

        public DateTime? ExpectedEndDate { get; set; }

        public DateTime? ActualEndDate { get; set; }

        [MaxLength(200)]
        public string? Location { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProjectType { get; set; }

        [Required]
        [MaxLength(100)]
        public string FundingSource { get; set; }

        public bool IsPublic { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}