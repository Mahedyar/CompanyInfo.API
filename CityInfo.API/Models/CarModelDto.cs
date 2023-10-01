namespace CompanyInfo.API.Models
{
    public class CarModelDto
    {
        public int ID { get; set; }

        public string Model { get; set; } = string.Empty;

        public string ProductionDate { get; set; } = string.Empty;

        public int Price { get; set; }

        public string? Description { get; set; }
    }
}
