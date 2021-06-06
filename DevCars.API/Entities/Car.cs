using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Entities
{
    public class Car : BaseEntity
    {
        protected Car() : base() {}
        public Car(string brand, string model, string vinCode, string color, int year, decimal price, DateTime productionDate)
        {
            Brand = brand;
            Model = model;
            VinCode = vinCode;
            Color = color;
            Year = year;
            Price = price;
            ProductionDate = productionDate;
            Status = CarStatusEnum.Available;
        }

        public string Brand { get; private set; }
        public string Model { get; private set; }
        public string VinCode { get; private set; }
        public string Color { get; private set; }
        public int Year { get; private set; }
        public decimal Price { get; private set; }
        public DateTime ProductionDate { get; private set; }
        public CarStatusEnum Status { get; private set; }


        public void Update(string color, decimal price)
        {
            this.Color = color;
            this.Price = price;
        }
        public void SetAsSuspended()
        {
            Status = CarStatusEnum.Suspended;
        }

        public void Sold()
        {
            Status = CarStatusEnum.Sold;
        }

    }
}
