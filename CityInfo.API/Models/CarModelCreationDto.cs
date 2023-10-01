using System.ComponentModel.DataAnnotations;

namespace CompanyInfo.API.Models
{
    public class CarModelCreationDto
    {
        [Required(ErrorMessage = "Please Enter Model")]
        [MaxLength(50)]
        public string Model { get; set; } = string.Empty;

        public string ProductionDate { get; set; } = string.Empty;

        public int Price { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
