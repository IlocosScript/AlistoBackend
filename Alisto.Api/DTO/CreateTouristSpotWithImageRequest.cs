using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Alisto.Api.DTOs
{
    public class CreateTouristSpotWithImageRequest
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

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

        public List<string> Highlights { get; set; } = new List<string>();

        [MaxLength(100)]
        public string? TravelTime { get; set; }

        public bool IsActive { get; set; } = true;

        // Single image upload
        public IFormFile? Image { get; set; }
    }
} 