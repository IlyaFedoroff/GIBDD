namespace GIBDD.Server.Models
{
    public class OwnerDto
    {
        public int OwnerId { get; set; }
        //public string FullName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string DriverLicenseNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime BirthYear { get; set; }
        public string Gender { get; set; } = null!;
    }
}
