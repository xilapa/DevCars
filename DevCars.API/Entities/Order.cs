using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Entities
{
    public class Order : BaseEntity
    {
        protected Order(): base() { }
        public Order(int idCar, int idCustomer, decimal carPrice, List<ExtraOrderItem> extraItems)
        {
            IdCar = idCar;
            IdCustomer = idCustomer;
            ExtraItems = extraItems;

            TotalCost = extraItems.Sum(e => e.Price) + carPrice;                      
        }

        public int IdCar { get; private set; }
        public Car Car { get; set; }
        public int IdCustomer { get; private set; }
        public Customer Customer { get; set; }
        public decimal TotalCost { get; private set; }
        public List<ExtraOrderItem> ExtraItems { get; private set; }

    }

    public class ExtraOrderItem : BaseEntity
    {
        protected ExtraOrderItem() { }
        public ExtraOrderItem(string description, decimal price)
        {
            Description = description;
            Price = price;
        }

        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int IdOrder { get; private set; }
    }
}
