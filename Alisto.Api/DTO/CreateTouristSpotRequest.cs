using System.ComponentModel.DataAnnotations;

namespace Alisto.Api.DTOs
{
    public class CreateTouristSpotRequest
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; }

        public decimal Rating { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; }

        [Required]
        [MaxLength(100)]
        public string Coordinates { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string OpeningHours { get; set; }

        [Required]
        [MaxLength(100)]
        public string EntryFee { get; set; }

        public List<string> Highlights { get; set; } = new List<string>();

        [Required]
        [MaxLength(100)]
        public string TravelTime { get; set; }

        public bool IsActive { get; set; } = true;
    }
} 