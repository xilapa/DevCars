using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Models.ViewModels
{
    // modelo utilizado para retornar na listagem de carros
    public class CarItemViewModel
    {
        public CarItemViewModel(int id, string brand, string model, decimal price)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Price = price;
        }

        public int Id { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public decimal Price { get; private set; }
    }
}
