using System;

namespace Alisto.Api.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactNumber { get; set; }
    }
}