namespace GIBDD.Server.Models
{
    //public class InspectionDto
    //{
    //    public int InspectionId { get; set; }
    //    public int CarId { get; set; }
    //    public int OwnerId { get; set; }
    //    public int OfficerId { get; set; }
    //    public DateTime InspectionDate { get; set; }
    //    public string Result { get; set; } = null!;
    //}

    public class InspectionDto
    {
        public int InspectionId { get; set; } // Уникальный идентификатор осмотра
        public DateTime InspectionDate { get; set; } // Дата осмотра
        public string Result { get; set; } = null!;// Результат осмотра (например, "Пройдено" или "Не пройдено")

        public string CarBrand { get; set; } = null!; // Марка автомобиля
        public string LicensePlate { get; set; } = null!; // Госномер автомобиля

        public string OfficerName { get; set; } = null!; // Полное имя инспектора
        public string OwnerFullName { get; set; } = null!;// Полное имя владельца
    }


}
