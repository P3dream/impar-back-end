// PhotoService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using impar_back_end.Context;
using Microsoft.EntityFrameworkCore;
using impar_back_end.Models.Photo.Entity;
using impar_back_end.Models.Photo.Dto;

namespace impar_back_end.Services
{
    public class PhotoService
    {
        private readonly ApplicationDbContext _context;

        public PhotoService(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<Photo>> GetPhotos(int page, int pageSize)
        {
            return await _context.Photos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Photo> GetPhoto(int id)
        {
            return await _context.Photos.FindAsync(id);
        }




        public async Task<Photo> CreatePhoto(CreatePhotoDto createPhotoDto)
        {
            var photo = new Photo
            {
                Base64 = createPhotoDto.Base64
            };
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();
            return photo;
        }

        public async Task<bool> UpdatePhoto(int id, UpdatePhotoDto updatePhotoDto)
        {
            var existingPhoto = await _context.Photos.FindAsync(id);
            if (existingPhoto == null)
            {
                throw new BadHttpRequestException("Invalid Photo id provided.");
            }

            existingPhoto.Base64 = updatePhotoDto.Base64;

            _context.Photos.Update(existingPhoto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePhoto(int id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo == null)
            {
                throw new BadHttpRequestException("Invalid Photo id provided.");
            }

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
