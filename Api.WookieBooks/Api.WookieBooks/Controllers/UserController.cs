using Api.WookieBooks.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Models.WookieBooks.Dto;
using Services.WookieBooks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.WookieBooks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;

        private readonly ILogger<UserController> _logger;
        
        public UserController(ILogger<UserController> logger, IUsersService usersService)
        {
            _logger = logger;
            _usersService = usersService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Get a collection of all registered users.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ListUserDto>))]
        public IActionResult Get()
        {
            var userList = _usersService.GetAll();
            return Ok(userList);
        }

        /// <summary>
        /// Get a specific user.
        /// </summary>
        /// <param name="id">The ID of the user</param>
        /// <returns>A single user with its registered information.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListUserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var user = _usersService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="dto">The model representing this new user</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] CreateUserDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_usersService.CheckIfExists(dto.Login))
            {
                ModelState.AddModelError(string.Empty, "There is already a user with this title on the database!");
                return StatusCode(404, ModelState);
            }

            try
            {
                _usersService.Create(dto);
                return Ok();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, $"Something went wrong on adding the user: {e.Message}");
                return StatusCode(500, ModelState);
            }
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="id">The id of the user to be updated</param>
        /// <param name="dto">The model representing the user to be updated</param>
        /// <returns></returns>
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Patch(int id, [FromBody] UpdateUserDto dto)
        {
            if (dto == null || !ModelState.IsValid || id != dto.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_usersService.CheckIfIdExists(id))
            {
                return NotFound();
            }

            if (_usersService.CheckIfExists(dto.Login, dto.Id))
            {
                ModelState.AddModelError(string.Empty, "There is already a user with this login on the database!");
                return StatusCode(404, ModelState);
            }

            try
            {
                _usersService.Update(dto);
                return Ok();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, $"Something went wrong on updating the user: {e.Message}");
                return StatusCode(500, ModelState);
            }
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id">The id of the user to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            if (!_usersService.CheckIfIdExists(id))
            {
                return NotFound();
            }

            try
            {
                _usersService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, $"Something went wrong on deleting the user: {e.Message}");
                return StatusCode(500, ModelState);
            }
        }
    }
}
