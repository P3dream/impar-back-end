using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using impar_back_end.Services;
using impar_back_end.Models.Car.Entity;
using impar_back_end.Models.Car.DTOs;
using impar_back_end.Models.Car.Dto;
using impar_back_end.Models.PageOptions;
using Microsoft.AspNetCore.Authorization;

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

        /// <summary>
        /// Retrieves a paginated list of cars.
        /// </summary>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Car>), 200)]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars([FromQuery] PageOptionsDto pageOptionsDto)
        {
            try
            {
                var cars = await _carService.GetCars(pageOptionsDto.Page, pageOptionsDto.PageSize);
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a car by its ID.
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Car), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            try
            {
                var car = await _carService.GetCar(id);
                if (car == null)
                {
                    return NotFound();
                }
                return Ok(car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new car.
        /// </summary>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(Car), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Car>> PostCar([FromBody] CreateCarDto createCarDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var car = await _carService.CreateCar(createCarDto);
                return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing car.
        /// </summary>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutCar(int id, [FromBody] UpdateCarDto updateCarDto)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes an existing car.
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                var result = await _carService.DeleteCar(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
