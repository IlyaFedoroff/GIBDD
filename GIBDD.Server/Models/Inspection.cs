using System.ComponentModel.DataAnnotations;

namespace GIBDD.Server.Models
{
    public class Inspection
    {
        [Key]
        public int InspectionId { get; set; }
        
        public DateTime InspectionDate { get; set; }
        public string Result { get; set; } = null!;
        
        public int CarId { get; set; }
        public Car Car { get; set; } = null!;
        
        public int OwnerId { get; set; }
        public Owner Owner { get; set; } = null!;
        
        public int OfficerId { get; set; }
        public Officer Officer { get; set; } = null!;
    }
}
