using Api.WookieBooks.Configuration;
using Api.WookieBooks.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models.WookieBooks.Dto;
using Services.WookieBooks.Interfaces;
using System.Collections.Generic;

namespace Api.WookieBooks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly AppSettings _appSettings;
        private readonly ILogger<AuthController> _logger;
        
        public AuthController(ILogger<AuthController> logger, IAuthService authService, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _authService = authService;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Authorizes an user to access all other locked endpoints using login and password
        /// </summary>
        /// <param name="dto">Login and Password information to authorize.</param>
        /// <returns>A logged user information with a JWT Bearer Token</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AuthorizedUserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Post([FromBody] LoginAttemptDto dto)
        {
            if(dto == null)
            {
                ModelState.AddModelError("Authentication", "Login and Password are required");
                return BadRequest(ModelState);
            }

            var authorizedUser = _authService.Login(dto);

            if (authorizedUser == null)
            {
                ModelState.AddModelError("Authentication", "Invalid login and password combination.");
                return NotFound(ModelState);
            }

            var tokenGenerator = new JwtTokenHelper(_appSettings);
            tokenGenerator.GenerateToken(authorizedUser);

            return Ok(authorizedUser);
        }
    }
}