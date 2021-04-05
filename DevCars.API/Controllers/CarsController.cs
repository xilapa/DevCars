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


namespace DevCars.API.Controllers
{
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly DevCarsDbContext dbContext;

        public CarsController(DevCarsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var cars = dbContext.Cars;

            var carsViewModel = cars
                .Where(c => c.Status == CarStatusEnum.Available)
                .Select(c => new CarItemViewModel(c.Id,c.Brand,c.Model,c.Price))
                .ToList();

            return Ok(carsViewModel);
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
            var car = dbContext.Cars.SingleOrDefault(c => c.Id == id);

            if (car == null) return NotFound();

            car.Update(im.Color, im.Price);
            dbContext.SaveChanges();

            return NoContent();
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
