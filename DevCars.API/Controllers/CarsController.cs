using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevCars.API.Entities;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace DevCars.API.Controllers
{
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly DevCarsDbContext dbContext;
        private readonly string connectionString;

        public CarsController(DevCarsDbContext dbContext, IConfiguration configuration) 
        {
            this.dbContext = dbContext;
            this.connectionString = configuration.GetConnectionString("DevCars");
        }


        /// <summary>
        /// Cadastrar Carro
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            //var cars = dbContext.Cars;

            //var carsViewModel = cars
            //    .Where(c => c.Status == CarStatusEnum.Available)
            //    .Select(c => new CarItemViewModel(c.Id,c.Brand,c.Model,c.Price))
            //    .ToList();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var query = "SELECT Id, Brand, Model, Price FROM Cars WHERE Status = 0";
                var carsViewModel = sqlConnection.Query<CarItemViewModel>(query);
                return Ok(carsViewModel);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById (int id)
        {
            var car = dbContext.Cars.SingleOrDefault(c => c.Id == id);

            if (car == null) return NotFound();

            var carDetailsViewModel = new CarDetailsViewModel(car.Id, car.Brand, car.Model, car.Color, car.Year, car.Price, car.ProductionDate);

            return Ok(carDetailsViewModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post([FromBody] AddCarInputModel im)
        {
            var car = new Car(im.Brand, im.Model, im.VinCode, im.Color, im.Year, im.Price, im.ProductionDate);
            dbContext.Cars.Add(car);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetById),new { id = car.Id },im);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] UpdateCarInputModel im, int id)
        {
            //var car = dbContext.Cars.SingleOrDefault(c => c.Id == id);

            //if (car == null) return NotFound();

            //car.Update(im.Color, im.Price);
            //dbContext.SaveChanges();

            //return NoContent();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var queryCar = "SELECT * FROM Cars WHERE Id = @id";
                var car = sqlConnection.Query<Car>(queryCar, new { Id = id}).SingleOrDefault();
                if (car == null) return NotFound();

                var queryUpdate = "UPDATE Cars SET Color = @color, Price = @price WHERE Id = @id";
                sqlConnection.Execute(queryUpdate, new { color = im.Color, price = im.Price, id = id });
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var car = dbContext.Cars.SingleOrDefault(c => c.Id == id);
            if (car == null) return NotFound();

            car.SetAsSuspended();
            dbContext.SaveChanges();

            return NoContent();
        }

    }
}
