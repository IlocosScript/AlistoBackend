using System;
using System.Collections.Generic;

namespace Alisto.Api.DTOs
{
    public class CityServiceDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Fee { get; set; }
        public string ProcessingTime { get; set; }
        public List<string> RequiredDocuments { get; set; } = new List<string>();
        public string OfficeLocation { get; set; }
        public string? ContactNumber { get; set; }
        public string OperatingHours { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}