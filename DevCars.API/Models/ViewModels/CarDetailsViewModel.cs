using System;

namespace DevCars.API.Models.ViewModels
{
    public class CarDetailsViewModel
    {
        public CarDetailsViewModel(int id, string brand, string model, string color, int year, decimal price, DateTime productionDate)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Color = color;
            Year = year;
            Price = price;
            ProductionDate = productionDate;
        }

        public int Id { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public string Color { get; private set; }
        public int Year { get; private set; }
        public decimal Price { get; private set; }
        public DateTime ProductionDate { get; private set; }
    }
}
