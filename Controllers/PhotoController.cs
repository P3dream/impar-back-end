using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using impar_back_end.Services;
using impar_back_end.Models.Photo.Entity;
using impar_back_end.Models.PageOptions;

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

        /// <summary>
        /// Retrieves a paginated list of photos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Photo>), 200)]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos([FromQuery] PageOptionsDto pageOptionsDto)
        {
            var photos = await _photoService.GetPhotos(pageOptionsDto.Page, pageOptionsDto.PageSize);
            return Ok(photos);
        }

        /// <summary>
        /// Retrieves a photo by its ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Photo), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Photo>> GetPhoto(int id)
        {
            var photo = await _photoService.GetPhoto(id);
            if (photo == null)
            {
                return NotFound();
            }
            return Ok(photo);
        }

        /// <summary>
        /// Creates a new photo.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Photo), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Photo>> PostPhoto([FromBody] Photo photo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _photoService.CreatePhoto(photo);
                return CreatedAtAction(nameof(GetPhoto), new { id = photo.Id }, photo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing photo.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutPhoto(int id, [FromBody] Photo photo)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes an existing photo.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            try
            {
                var result = await _photoService.DeletePhoto(id);
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