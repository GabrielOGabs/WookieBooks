using Api.WookieBooks.Configuration;
using Api.WookieBooks.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models.WookieBooks.Dto;
using Services.WookieBooks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.WookieBooks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;
        private readonly AppSettings _appSettings;

        private readonly ILogger<BooksController> _logger;
        
        public BooksController(ILogger<BooksController> logger, IBooksService booksService, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _booksService = booksService;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns>Get a collection of all registered books.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ListBookDto>))]
        public IActionResult Get()
        {
            var bookList = _booksService.GetAll();
            return Ok(bookList);
        }

        /// <summary>
        /// Get a specific book.
        /// </summary>
        /// <param name="id">The ID of the book</param>
        /// <returns>A single book with its registered information.</returns>
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListBookDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var book = _booksService.Get(id);
            
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        /// <summary>
        /// Creates a new book
        /// </summary>
        /// <param name="dto">The model representing this new book</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] CreateBookDto dto)
        {
            var jwtHelper = new JwtTokenHelper(_appSettings);
            var currUserClaims = HttpContext.User.Identity as ClaimsIdentity;

            dto.UserId = jwtHelper.GetUserIdFromClaims(currUserClaims);

            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_booksService.CheckIfExists(dto.Title))
            {
                ModelState.AddModelError(string.Empty, "There is already a book with this title on the database!");
                return StatusCode(404, ModelState);
            }

            try
            {
                _booksService.Create(dto);
                return Ok();
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, $"Something went wrong on adding the book: {e.Message}");
                return StatusCode(500, ModelState);
            }
        }

        /// <summary>
        /// Update a book
        /// </summary>
        /// <param name="id">The id of the book to be updated</param>
        /// <param name="dto">The model representing the book to be updated</param>
        /// <returns></returns>
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Patch(int id, [FromBody] UpdateBookDto dto)
        {
            if (dto == null || !ModelState.IsValid || id != dto.Id)
            {
                return BadRequest(ModelState);
            }

            if(!_booksService.CheckIfIdExists(id))
            {
                return NotFound();
            }

            if (_booksService.CheckIfExists(dto.Title, dto.Id))
            {
                ModelState.AddModelError(string.Empty, "There is already a book with this title on the database!");
                return StatusCode(404, ModelState);
            }

            try
            {
                _booksService.Update(dto);
                return Ok();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, $"Something went wrong on updating the book: {e.Message}");
                return StatusCode(500, ModelState);
            }
        }

        /// <summary>
        /// Deletes a book
        /// </summary>
        /// <param name="id">The id of the book to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            if(!_booksService.CheckIfIdExists(id))
            {
                return NotFound();
            }

            try
            {
                _booksService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, $"Something went wrong on deleting the book: {e.Message}");
                return StatusCode(500, ModelState);
            }
        }
    }
}
