using System;
using System.ComponentModel.DataAnnotations;

namespace Alisto.Api.DTOs
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}