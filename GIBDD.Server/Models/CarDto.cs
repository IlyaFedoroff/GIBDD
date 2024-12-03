namespace GIBDD.Server.Models
{
    public class CarDto
    {
        public int CarId { get; set; }
        public string LicensePlate { get; set; } = null!;
        public string EngineNumber { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string TechnicalPassportNumber { get; set; } = null!;
        public DateTime ManufactureDate { get; set; }
    }
}
