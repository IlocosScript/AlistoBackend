using System;
using Alisto.Api.Enums;

namespace Alisto.Api.DTOs
{
    public class CivilRegistryRequestDto
    {
        public string AppointmentId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public CivilDocumentType DocumentType { get; set; }
        public string Purpose { get; set; }
        public int NumberOfCopies { get; set; }
        public string? RegistryNumber { get; set; }
        public DateTime? RegistryDate { get; set; }
        public string? RegistryPlace { get; set; }
        public AppointmentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 