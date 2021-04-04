using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Entities
{
    public class Order
    {
        public Order(int id, int idCar, int idCustomer, decimal carPrice, List<ExtraOrderItem> extraItems)
        {
            Id = id;
            IdCar = idCar;
            IdCustomer = idCustomer;
            ExtraItems = extraItems;

            TotalCost = extraItems.Sum(e => e.Price) + carPrice;
                      
        }

        public int Id { get; private set; }
        public int IdCar { get; private set; }
        public int IdCustomer { get; private set; }
        public decimal TotalCost { get; private set; }
        public List<ExtraOrderItem> ExtraItems { get; private set; }

    }

    public class ExtraOrderItem
    {
        public ExtraOrderItem(string description, decimal price, int idOrder)
        {
            Description = description;
            Price = price;
            IdOrder = idOrder;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int IdOrder { get; private set; }
    }
}
