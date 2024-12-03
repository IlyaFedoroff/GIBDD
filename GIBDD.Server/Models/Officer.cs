using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GIBDD.Server.Models
{
    public class Officer
    {
        [Key]
        public int OfficerId { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public string? MiddleName { get; set; }

        [Required]
        public string Position { get; set; } = null!;
        [JsonIgnore]
        public ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();
    }
}
