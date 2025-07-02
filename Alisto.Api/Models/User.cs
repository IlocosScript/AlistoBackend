using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alisto.Api.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

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

        [Required]
        [Phone]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLoginAt { get; set; }

        [MaxLength(500)]
        public string? ProfileImageUrl { get; set; }

        [MaxLength(100)]
        public string? EmergencyContactName { get; set; }

        [MaxLength(20)]
        public string? EmergencyContactNumber { get; set; }

        // Navigation properties
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<IssueReport> IssueReports { get; set; } = new List<IssueReport>();
        public virtual ICollection<UserSession> Sessions { get; set; } = new List<UserSession>();
    }
}