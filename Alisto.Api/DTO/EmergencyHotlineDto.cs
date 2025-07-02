namespace Alisto.Api.DTOs
{
    public class EmergencyHotlineDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public bool IsEmergency { get; set; }
        public string Department { get; set; }
        public string OperatingHours { get; set; }
    }
}