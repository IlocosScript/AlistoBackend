using System;

namespace Alisto.Api.DTOs
{
    public class NotificationDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public bool IsRead { get; set; }
        public string? ActionUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}