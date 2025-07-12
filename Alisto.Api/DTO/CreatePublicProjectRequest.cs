using System;
using System.ComponentModel.DataAnnotations;
using Alisto.Api.Enums;

namespace Alisto.Api.DTOs
{
    public class CreatePublicProjectRequest
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        [MaxLength(200)]
        public string Contractor { get; set; }

        public ProjectStatus Status { get; set; } = ProjectStatus.Planned;

        public int? Progress { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? ExpectedEndDate { get; set; }

        [MaxLength(200)]
        public string? Location { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProjectType { get; set; }

        [Required]
        [MaxLength(100)]
        public string FundingSource { get; set; }

        public bool IsPublic { get; set; } = true;
    }
} 