using System;

namespace Alisto.Api.DTOs
{
    public class IssuePhotoDto
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string PhotoUrl { get; set; }
        public string? Caption { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}