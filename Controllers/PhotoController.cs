using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using impar_back_end.Services;
using impar_back_end.Models.Photo.Entity;
using impar_back_end.Models.PageOptions;
using Microsoft.AspNetCore.Authorization;
using impar_back_end.Models.Photo.Dto;
using System.Net;

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
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Photo>), 200)]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos([FromQuery] PageOptionsDto pageOptionsDto)
        {
            var photos = await _photoService.GetPhotos(pageOptionsDto.Page, pageOptionsDto.PageSize);
            return Ok(photos);
        }

        /// <summary>
        /// Creates a new photo.
        /// </summary>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(Photo), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Photo>> PostPhoto([FromBody] CreatePhotoDto createPhotoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _photoService.CreatePhoto(createPhotoDto);
                return CreatedAtAction(nameof(GetPhoto), new { id = createPhotoDto.Id }, createPhotoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing photo.
        /// </summary>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutPhoto(int id, [FromBody] UpdatePhotoDto updatePhotoDto)
        {
            try
            {
                if (id != updatePhotoDto.Id)
                {
                    return BadRequest();
                }

                var result = await _photoService.UpdatePhoto(id, updatePhotoDto);
                if (!result)
                {
                    return BadRequest();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
         ///<summary>
         ///Recieves a photo id and returns the photo content in image format
         ///</summary>
        [Authorize]
        [HttpGet("/getImage/{id}")]
        public async Task<IActionResult> getImage(int id)
        {
            try
            {
                Photo photo = await _photoService.GetPhoto(id);
                if (photo == null)
                {
                    return NotFound();
                }
                return File(Convert.FromBase64String(photo.Base64), "image/jpeg");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deletes an existing photo.
        /// </summary>
        [Authorize]
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
                    return BadRequest();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Retrieves a photo by its ID.
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Photo), 200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<Photo>> GetPhoto(int id)
        {
            var photo = await _photoService.GetPhoto(id);
            if (photo == null)
            {
                return NoContent();
            }
            return Ok(photo);
        }
    }
}