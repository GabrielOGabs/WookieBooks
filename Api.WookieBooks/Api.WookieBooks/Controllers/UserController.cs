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
        /// Get a specific user.
        /// </summary>
        /// <param name="id">The ID of the user</param>
        /// <returns>A single user with its registered information.</returns>
        [HttpGet("{id:int}", Name = "GetUser")]
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
    }
}
