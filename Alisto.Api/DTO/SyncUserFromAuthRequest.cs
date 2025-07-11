using System;
using System.ComponentModel.DataAnnotations;

namespace Alisto.Api.DTOs
{
    public class SyncUserFromAuthRequest
    {
        [Required]
        [MaxLength(255)]
        public string ExternalId { get; set; }

        [Required]
        [MaxLength(100)]
        public string AuthProvider { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string? MiddleName { get; set; }

        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(500)]
        public string? ProfileImageUrl { get; set; }

        [MaxLength(100)]
        public string? EmergencyContactName { get; set; }

        [MaxLength(20)]
        public string? EmergencyContactNumber { get; set; }
    }
} 