namespace GIBDD.Server.Models
{
    public class InspectionHistoryDto
    {
        public DateTime InspectionDate { get; set; }
        public string Result { get; set; } = null!;
    }
}
