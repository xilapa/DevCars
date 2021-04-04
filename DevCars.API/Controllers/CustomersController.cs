using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly DevCarsDbContext dbContext;
        public CustomersController(DevCarsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddCustomerInputModel im)
        {
            var lastId = dbContext.Customers.Last().Id;
            var customer = new Customer(lastId + 1, im.FullName, im.Document, im.BirthDate);
            dbContext.Customers.Add(customer);
            return NoContent();
        }

        // post api/customer/1/orders
        [HttpPost("{id}/orders")]
        public IActionResult PostOrder(int id, [FromBody] AddOrderInputModel im)
        {
            if (id != im.IdCustomer) return BadRequest();

            // obtendo carro do bd para selecionar o preço
            var car = dbContext.Cars.SingleOrDefault(c => c.Id == im.IdCar);

            // obtendo cliente para selecionar o último id de order
            var customer = dbContext.Customers
                .SingleOrDefault(c => c.Id == im.IdCustomer);
            var lastOrder = customer.Orders.LastOrDefault();
            var lastId = 1;
            if (lastOrder != null) lastId = lastOrder.Id;


            // gerando lista de itens extras com base no input model
            var extraItems = im.ExtraItems
                .Select(e => new ExtraOrderItem(e.Description, e.Price, lastId + 1))
                .ToList();

            var order = new Order(lastId + 1, im.IdCar, im.IdCustomer, car.Price, extraItems);

            customer.Purchase(order);
            car.Sold();

            return CreatedAtAction(nameof(GetOrder), new { id = im.IdCustomer, orderId = order.Id }, im);
        }

        // post api/customer/1/orders/2
        [HttpGet("{id}/orders/{orderId}")]
        public IActionResult GetOrder(int id, int orderId)
        {
            var customer = dbContext.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();

            var order = customer.Orders.SingleOrDefault(o => o.Id == orderId);
            if (order == null) return NotFound();

            var extraItems = order.ExtraItems.Select(e => e.Description).ToList();

            var orderDetailsViewModel = new OrderDetailsViewModel(order.IdCar,order.IdCustomer, order.TotalCost, extraItems);

            return Ok(orderDetailsViewModel);
        }

    }
}
