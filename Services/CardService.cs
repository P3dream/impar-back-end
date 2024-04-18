using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using impar_back_end.Context;
using impar_back_end.Models.Car.Entity;
using impar_back_end.Models.Photo.Entity;
using Microsoft.EntityFrameworkCore;
using impar_back_end.Models.Card;
using impar_back_end.Models.Photo.Dto;
using impar_back_end.Models.Car.DTOs;
using impar_back_end.Models.PageOptions;
using impar_back_end.Models.Car.Dto;

namespace impar_back_end.Services
{
    public class CardService
    {
        private readonly ApplicationDbContext _context;
        private readonly PhotoService _photoService;
        private readonly CarService _carService;

        public CardService(ApplicationDbContext context, PhotoService photoService, CarService carService)
        {
            _context = context;
            _photoService = photoService;
            _carService = carService;
        }

        public async Task<string> ConvertImageToBase64(IFormFile image)
        {
            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        public async Task<bool> DeleteCard(int carId)
        {
            try
            {
                var foundCar = await _carService.GetCar(carId);
                if(foundCar == null) {
                    return false;
                }
                var photoDeleted = await _photoService.DeletePhoto(foundCar.PhotoId);
                if (!photoDeleted)
                {
                    return false;
                }
                var carDeleted = await _carService.DeleteCar(carId);
                if (!carDeleted)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> CreateCarFromPhotoAndCar(CreateCardDto createCardDto)
        {
            try
            {
                string base64Image = await ConvertImageToBase64(createCardDto.Image);
                var photo = await _photoService.CreatePhoto(new CreatePhotoDto { Base64 = base64Image });
                await _carService.CreateCar(new CreateCarDto { PhotoId = photo.Id, Name = createCardDto.CarName, Status = createCardDto.Status });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public async Task<IEnumerable<object>> GetCards(PageOptionsDto pageOptions, string? searchString)
        {
            var query = _context.Cars
                .Join(_context.Photos,
                    car => car.PhotoId,
                    photo => photo.Id,
                    (car, photo) => new
                    {
                        Car = car,
                        Photo = photo
                    })
                .AsQueryable();

            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    query = query.Where(item =>
            //        item.Car.Name.Trim().Contains(searchString) || 
            //        item.Car.Status.    Contains(searchString)
            //    );
            //}

            if (pageOptions != null)
            {
                var pageSize = pageOptions.PageSize;
                var pageNumber = pageOptions.Page;

                query = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize);
            }

            return await query.ToListAsync();
        }

        public async Task<bool> UpdateCard(UpdateCardDto updateCardDto,int CarId)
        {
            try
            {
                var foundCar = await _carService.GetCar(CarId);
                if (foundCar == null)
                {
                    return false;
                }
                string base64Image = await ConvertImageToBase64(updateCardDto.Image);
                var photo = await _photoService.UpdatePhoto(foundCar.PhotoId, new UpdatePhotoDto { Base64 = base64Image });

                if (photo == null)
                {
                    return false;
                }

                
                var updateCarDto = new UpdateCarDto
                {
                    Id = CarId,
                    Name = updateCardDto.CarName,
                    Status = updateCardDto.Status,
                    PhotoId = foundCar.PhotoId 
                };

                bool carUpdateResult = await _carService.UpdateCar(CarId, updateCarDto);

                if (!carUpdateResult)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}