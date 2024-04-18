using System.Collections.Generic;
using System.Threading.Tasks;
using impar_back_end.Context;
using impar_back_end.Models.Car.Dto;
using impar_back_end.Models.Car.DTOs;
using impar_back_end.Models.Car.Entity;
using impar_back_end.Models.Photo.Entity;
using Microsoft.EntityFrameworkCore;

namespace impar_back_end.Services
{
    public class CarService
    {
        private readonly ApplicationDbContext _context;
        private readonly PhotoService _photoService;

        public CarService(ApplicationDbContext context, PhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }

        public async Task<IEnumerable<Car>> GetCars(int page, int pageSize)
        {
            return await _context.Cars
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Car> GetCar(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task<Car> CreateCar(CreateCarDto createCarDto)
        {
            Photo photo = await _photoService.GetPhoto(createCarDto.PhotoId);
            if(photo == null) {
                throw new BadHttpRequestException("Invalid Photo id provided.");
            }

            bool carExists = await _context.Cars.AnyAsync(x => x.PhotoId == createCarDto.PhotoId);
            if (carExists)
            {
                throw new InvalidOperationException("This PhotoId is already associated with a car.");
            }

            Car car = new Car
            {
                PhotoId = createCarDto.PhotoId,
                Name = createCarDto.Name,
                Status = createCarDto.Status
            };
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;

        }

        public async Task<bool> UpdateCar(int id, UpdateCarDto updateCarDto)
        {
            if (id != updateCarDto.Id)
            {
                throw new ArgumentException("Id provided is diferent from provided on url");
            }

            Photo photo = await _photoService.GetPhoto(updateCarDto.PhotoId);
            if (photo == null)
            {
                throw new InvalidOperationException("It was not possible get this photo");
            }

            var existingCar = await _context.Cars.FindAsync(id);
            if (existingCar == null)
            {
                throw new BadHttpRequestException("Invalid car id provided.");
            }

            existingCar.Name = updateCarDto.Name;
            existingCar.Status = updateCarDto.Status;
            existingCar.PhotoId = updateCarDto.PhotoId;

            _context.Cars.Update(existingCar);
            await _context.SaveChangesAsync();
            return true;    
        }

        public async Task<bool> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                throw new BadHttpRequestException("Invalid car id provided.");
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}