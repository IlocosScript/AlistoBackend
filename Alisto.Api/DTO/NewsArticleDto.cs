using System;
using System.Collections.Generic;

namespace Alisto.Api.DTOs
{
    public class NewsArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string? FullContent { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public string PublishedTime { get; set; }
        public string Location { get; set; }
        public string? ExpectedAttendees { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public bool IsFeatured { get; set; }
        public bool IsTrending { get; set; }
        public int ViewCount { get; set; }
    }
}