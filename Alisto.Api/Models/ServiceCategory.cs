using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alisto.Api.Models
{
    public class ServiceCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string IconName { get; set; }

        public bool IsActive { get; set; } = true;

        public int SortOrder { get; set; }

        // Navigation properties
        public virtual ICollection<CityService> Services { get; set; } = new List<CityService>();
    }
}