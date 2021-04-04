using DevCars.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Persistence
{
    public class DevCarsDbContext 
    {
        public DevCarsDbContext()
        {
            Cars = new List<Car>
            {
                new Car(1,"Subaru","Impreza","ABCD1234","Azul",2016,65000,new DateTime(2016,1,1)),
                new Car(2,"Honda","Civic","ABCE1235","Prata",2006,25000,new DateTime(2006,1,1)),
                new Car(3,"Hyundai","HB20","AJCD1834","Branca",2019,45000,new DateTime(2019,1,1))
            };

            Customers = new List<Customer>
            {
                new Customer(1, "Dirceu Junior","12345567", new DateTime(1993,9,30)),
                new Customer(2, "Dirceu Junior","12345567", new DateTime(1993,9,30)),
                new Customer(3, "Dirceu Junior","12345567", new DateTime(1993,9,30))
            };
        }
        public List<Car> Cars { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
