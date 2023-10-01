namespace CompanyInfo.API.Models
{
    public class CompanyDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int NumberOfModels
        {
            get
            {
                return CarModels.Count;
            }
        }

        public ICollection<CarModelDto> CarModels { get; set; } = new List<CarModelDto>();




    }
}
