using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using impar_back_end.Services;
using impar_back_end.Models.Photo.Entity;

namespace impar_back_end.Controllers
{
    [Route("api/Photo")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly PhotoService _photoService;

        public PhotoController(PhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var photos = await _photoService.GetPhotos(page, pageSize);
            return Ok(photos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Photo>> GetPhoto(int id)
        {
            var photo = await _photoService.GetPhoto(id);
            if (photo == null)
            {
                return NotFound();
            }
            return Ok(photo);
        }

        [HttpPost]
        public async Task<ActionResult<Photo>> PostPhoto([FromBody] Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _photoService.CreatePhoto(photo);
            return CreatedAtAction(nameof(GetPhoto), new { id = photo.Id }, photo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoto(int id, [FromBody] Photo photo)
        {
            if (id != photo.Id)
            {
                return BadRequest();
            }

            var result = await _photoService.UpdatePhoto(id, photo);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var result = await _photoService.DeletePhoto(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
