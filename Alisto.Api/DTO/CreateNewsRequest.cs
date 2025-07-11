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

        [Required]
        public string Summary { get; set; }

        [Required]
        public string FullContent { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public DateTime PublishedDate { get; set; }

        [Required]
        public string PublishedTime { get; set; }

        [Required]
        public string Location { get; set; }

        public string? ExpectedAttendees { get; set; }

        [Required]
        public NewsCategory Category { get; set; }

        [Required]
        public string Author { get; set; }

        public List<string> Tags { get; set; } = new List<string>();

        public bool IsFeatured { get; set; }

        public bool IsTrending { get; set; }
    }
}