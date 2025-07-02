using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Alisto.Api.Models
{
    public class CityService
    {
        [Key]
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Fee { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProcessingTime { get; set; }

        [Column(TypeName = "json")]
        public string RequiredDocuments { get; set; } = "[]";

        public bool IsActive { get; set; } = true;

        [Required]
        [MaxLength(200)]
        public string OfficeLocation { get; set; }

        [MaxLength(20)]
        public string? ContactNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string OperatingHours { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("CategoryId")]
        public virtual ServiceCategory Category { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        // Helper methods for RequiredDocuments
        [NotMapped]
        public List<string> RequiredDocumentsList
        {
            get => JsonSerializer.Deserialize<List<string>>(RequiredDocuments) ?? new List<string>();
            set => RequiredDocuments = JsonSerializer.Serialize(value);
        }
    }
}