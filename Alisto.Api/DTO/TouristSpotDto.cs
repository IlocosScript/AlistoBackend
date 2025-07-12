using System;
using System.Collections.Generic;

namespace Alisto.Api.DTOs
{
    public class TouristSpotDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Rating { get; set; }
        public string Location { get; set; }
        public string? Coordinates { get; set; }
        public string Address { get; set; }
        public string? OpeningHours { get; set; }
        public string? EntryFee { get; set; }
        public List<string> Highlights { get; set; } = new List<string>();
        public string? TravelTime { get; set; }
        public bool IsActive { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}