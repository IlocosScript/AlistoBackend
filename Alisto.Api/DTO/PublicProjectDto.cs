using System;

namespace Alisto.Api.DTOs
{
    public class PublicProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Cost { get; set; }
        public string Contractor { get; set; }
        public string Status { get; set; }
        public int? Progress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string? Location { get; set; }
        public string ProjectType { get; set; }
        public string FundingSource { get; set; }
    }
}