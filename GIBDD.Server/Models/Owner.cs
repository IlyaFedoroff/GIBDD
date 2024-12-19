using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GIBDD.Server.Models
{
    public class Owner
    {
        [Key]
        public int OwnerId { get; set; }
        [Required]
        public string DriverLicenseNumber { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public string? MiddleName { get; set; }
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public DateTime BirthYear { get; set; }
        [Required]
        public string Gender { get; set; } = null!;
        [JsonIgnore]
        public ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();
    }
}
