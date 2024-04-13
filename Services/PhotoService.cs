// PhotoService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using impar_back_end.Context;
using Microsoft.EntityFrameworkCore;
using impar_back_end.Models.Photo.Entity;

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

        public async Task<Photo> CreatePhoto(Photo photo)
        {
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();
            return photo;
        }

        public async Task<bool> UpdatePhoto(int id, Photo photo)
        {
            var existingPhoto = await _context.Photos.FindAsync(id);
            if (existingPhoto == null)
            {
                return false;
            }

            existingPhoto.Base64 = photo.Base64;

            _context.Photos.Update(existingPhoto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePhoto(int id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo == null)
            {
                return false;
            }

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
