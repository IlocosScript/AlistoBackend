using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alisto.Api.Enums;

namespace Alisto.Api.DTOs
{
    public class CreateNewsRequest
    {
        [Required]
        public string Title { get; set; }

        public string? Summary { get; set; }

        [Required]
        public string FullContent { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime? PublishedDate { get; set; }

        public string? PublishedTime { get; set; }

        [Required]
        public string Location { get; set; }

        public string? ExpectedAttendees { get; set; }

        [Required]
        public NewsCategory Category { get; set; }

        [Required]
        public string Author { get; set; }

        public List<string> Tags { get; set; } = new List<string>();

        [Required]
        public bool IsFeatured { get; set; }

        [Required]
        public bool IsTrending { get; set; }
    }
}