using System.Collections.Generic;

namespace Alisto.Api.DTOs
{
    public class CityServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Fee { get; set; }
        public string ProcessingTime { get; set; }
        public List<string> RequiredDocuments { get; set; } = new List<string>();
        public string OfficeLocation { get; set; }
        public string? ContactNumber { get; set; }
        public string OperatingHours { get; set; }
    }
}