using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using impar_back_end.Services;
using impar_back_end.Models.User.Entity;
using impar_back_end.Models.User.Dto;

namespace impar_back_end.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(User), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            try
            {
                await _userService.Register(user);
                return CreatedAtAction(nameof(Login), new { username = user.Username }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to register user");
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<User>> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                var user = await _userService.Login(loginRequestDto);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Unauthorized("Invalid credentials");
            }
        }
    }
}