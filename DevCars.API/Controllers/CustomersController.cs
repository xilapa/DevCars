using DevCars.API.Entities;
using DevCars.API.Models.InputModels;
using DevCars.API.Persistence;
using DevCars.API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var customer = new Customer(im.FullName, im.Document, im.BirthDate);
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
            return NoContent();
        }

        // post api/customer/1/orders
        [HttpPost("{id}/orders")]
        public IActionResult PostOrder(int id, [FromBody] AddOrderInputModel im)
        {
            if (id != im.IdCustomer) return BadRequest();

            // obtendo carro do bd para selecionar o preço
            var car = dbContext.Cars
                .SingleOrDefault(c => c.Id == im.IdCar);

            if (car == null) return NotFound();
            if (car.Status != CarStatusEnum.Available) return BadRequest("O carro selecionado não esta disponível para venda");

            // gerando lista de itens extras com base no input model
            var extraItems = im.ExtraItems
                .Select(e => new ExtraOrderItem(e.Description, e.Price))
                .ToList();

            var order = new Order(im.IdCar, im.IdCustomer, car.Price, extraItems);
            dbContext.Orders.Add(order);
            car.Sold();
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetOrder), new { id = im.IdCustomer, orderId = order.Id }, im);
        }

        // post api/customer/1/orders/2
        [HttpGet("{id}/orders/{orderId}")]
        public IActionResult GetOrder(int id, int orderId)
        {
            var order = dbContext.Orders
                .Include(o => o.ExtraItems)
                .AsNoTracking()
                .SingleOrDefault(o => o.Id == orderId);
            if (order == null) return NotFound();

            var extraItems = order.ExtraItems.Select(e => e.Description).ToList();

            var orderDetailsViewModel = new OrderDetailsViewModel(order.IdCar,order.IdCustomer, order.TotalCost, extraItems);

            return Ok(orderDetailsViewModel);
        }

    }
}
