using DevCars.API.Entities;
using DevCars.API.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DevCars.API.Persistence
{
    public class DevCarsDbContext : DbContext
    {
        public DevCarsDbContext(DbContextOptions<DevCarsDbContext> dbContextOptions) : base(dbContextOptions)
        {
            //Cars = new List<Car>
            //{
            //    new Car(1,"Subaru","Impreza","ABCD1234","Azul",2016,65000,new DateTime(2016,1,1)),
            //    new Car(2,"Honda","Civic","ABCE1235","Prata",2006,25000,new DateTime(2006,1,1)),
            //    new Car(3,"Hyundai","HB20","AJCD1834","Branca",2019,45000,new DateTime(2019,1,1))
            //};

            //Customers = new List<Customer>
            //{
            //    new Customer(1, "Dirceu Junior","12345567", new DateTime(1993,9,30)),
            //    new Customer(2, "Dirceu Junior","12345567", new DateTime(1993,9,30)),
            //    new Customer(3, "Dirceu Junior","12345567", new DateTime(1993,9,30))
            //};
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ExtraOrderItem> ExtraOrderItems { get; set; }
    }
}
