

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyInfo.API.Entities
{
    public class CarModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        //Relation
        [ForeignKey("CompanyID")]
        public Company? Company { get; set; }
        public int CompanyID { get; set; }

        
        public CarModel(string model)
        {
            Model = model;
        }
    }
}
