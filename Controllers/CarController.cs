using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using impar_back_end.Services;
using impar_back_end.Models.Car.Entity;
using impar_back_end.Models.Car.DTOs;
using impar_back_end.Models.Car.Dto;

namespace impar_back_end.Controllers
{
    [Route("api/Car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var cars = await _carService.GetCars(page, pageSize);
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _carService.GetCar(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        [HttpPost]
        public async Task<ActionResult<Car>> PostCar([FromBody] CreateCarDto createCarDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var car = await _carService.CreateCar(createCarDto);
                return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, [FromBody] UpdateCarDto updateCarDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _carService.UpdateCar(id, updateCarDto);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var result = await _carService.DeleteCar(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}