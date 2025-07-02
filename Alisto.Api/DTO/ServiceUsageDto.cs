namespace Alisto.Api.DTOs
{
    public class ServiceUsageDto
    {
        public string ServiceName { get; set; }
        public int UsageCount { get; set; }
        public decimal SatisfactionRating { get; set; }
    }
}