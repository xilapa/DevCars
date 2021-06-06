using System.Collections.Generic;

namespace DevCars.API.Models.InputModels
{
    public class AddOrderInputModel
    {
        public int IdCar { get; set; }
        public int IdCustomer { get; set; }
        public List<ExtraItemInputModel> ExtraItems { get; set; }
    }

    public class ExtraItemInputModel
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
