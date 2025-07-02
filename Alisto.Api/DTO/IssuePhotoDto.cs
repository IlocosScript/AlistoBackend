using System;

namespace Alisto.Api.DTOs
{
    public class IssuePhotoDto
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}