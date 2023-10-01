using CompanyInfo.API.Models;
using CompanyInfo.API.Services;

namespace CompanyInfo.API
{
    public class CarsDataStore : IDataStore
    {
        public List<CompanyDto> Companies { get; set; }

        public static CarsDataStore currentStore { get; } = new CarsDataStore();

        public CarsDataStore()
        {
            Companies = new List<CompanyDto>()
            {
                new CompanyDto()
                {
                    ID = 1,
                    Name = "Benz" ,
                    CarModels = new List<CarModelDto>()
                    {
                        new CarModelDto()
                        {
                            ID=1,
                            Model = "S501",
                            Price = 1233123,
                            ProductionDate = "2010",
                            Description = "This car is a Legend"
                        },
                          new CarModelDto()
                        {
                            ID=2,
                            Model = "C200",
                            Price = 205200,
                            ProductionDate = "2013",
                            Description = "This car is a Legend"
                        }
                    }
                },new CompanyDto()
                {
                    ID = 2,
                    Name = "Toyota" ,
                    CarModels = new List<CarModelDto>()
                    {
                        new CarModelDto()
                        {
                            ID=3,
                            Model = "Prado",
                            Price = 90000,
                            ProductionDate = "2011",
                            Description = "This car is a Legend"
                        },
                          new CarModelDto()
                        {
                            ID=4,
                            Model = "Corolla",
                            Price = 200000,
                            ProductionDate = "2021",
                            Description = "This car is a Legend"
                        }
                    }
                },new CompanyDto()
                {
                    ID = 3,
                    Name = "Kia" ,
                    CarModels = new List<CarModelDto>()
                    {
                        new CarModelDto()
                        {
                            ID=5,
                            Model = "Cerato",
                            Price = 120000,
                            ProductionDate = "2015",
                            Description = "This car is a Legend"
                        },
                          new CarModelDto()
                        {
                            ID=6,
                            Model = "Optima",
                            Price = 166000,
                            ProductionDate = "2017",
                            Description = "This car is a Legend"
                        }
                    }
                }

            };
        }
    }
}
