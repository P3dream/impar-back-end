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

        public async Task<bool> DeleteCard(int carId, int photoId)
        {
            try
            {
                var photoDeleted = await _photoService.DeletePhoto(photoId);
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
                Console.WriteLine($"Error deleting card: {ex.Message}");
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
                Console.WriteLine($"Erro ao criar carro a partir de foto: {ex.Message}");
                return false;
            }
        }



        public async Task<IEnumerable<object>> GetCards(PageOptionsDto pageOptions)
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

            if (pageOptions != null)
            {
                var pageSize = pageOptions.PageSize;
                var pageNumber = pageOptions.Page;

                query = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize);
            }

            return await query.ToListAsync();
        }

        public async Task<bool> UpdateCard(UpdateCardDto updateCardDto)
        {
            try
            {
                string base64Image = await ConvertImageToBase64(updateCardDto.Image);
                var photo = await _photoService.UpdatePhoto(updateCardDto.PhotoId, new UpdatePhotoDto { Base64 = base64Image });

                if (photo == null)
                {
                    return false;
                }

                // Corrigindo o ID do carro
                var updateCarDto = new UpdateCarDto
                {
                    Id = updateCardDto.CarId, // Usando o ID correto do carro
                    Name = updateCardDto.CarName,
                    Status = updateCardDto.Status,
                    PhotoId = updateCardDto.PhotoId // Garantindo que o PhotoId seja passado corretamente
                };

                bool carUpdateResult = await _carService.UpdateCar(updateCardDto.CarId, updateCarDto);

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