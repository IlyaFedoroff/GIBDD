using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GIBDD.Server.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        [Required]
        public string LicensePlate { get; set; } = null!;
        [Required]
        public string EngineNumber { get; set; } = null!;
        [Required]
        public string Color { get; set; } = null!;
        [Required]
        public string Brand { get; set; } = null!;
        [Required]
        public string TechnicalPassportNumber { get; set; } = null!;
        [Required]
        
        public DateTime ManufactureDate { get; set; }

        [JsonIgnore]
        public ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();
    }
}
