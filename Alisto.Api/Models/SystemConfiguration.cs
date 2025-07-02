using System;
using System.ComponentModel.DataAnnotations;
using Alisto.Api.Enums;

namespace Alisto.Api.Models
{
    public class SystemConfiguration
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(100)]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public ConfigDataType DataType { get; set; }

        public bool IsPublic { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}