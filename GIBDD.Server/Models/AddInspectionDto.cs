namespace GIBDD.Server.Models
{
    public class AddInspectionDto
    {
        public int CarId { get; set; }
        public int OwnerId { get; set; }
        public int OfficerId { get; set; }
        public DateTime InspectionDate { get; set; }
        public string Result { get; set; } = null!;
    }
}
