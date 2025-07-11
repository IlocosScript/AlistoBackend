using System;
using System.Collections.Generic;

namespace Alisto.Api.DTOs
{
    public class ServiceCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<CityServiceDto> Services { get; set; } = new List<CityServiceDto>();
    }
}