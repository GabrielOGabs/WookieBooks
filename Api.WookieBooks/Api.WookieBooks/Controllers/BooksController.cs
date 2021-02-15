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
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        private readonly ILogger<BooksController> _logger;
        
        public BooksController(ILogger<BooksController> logger, IBooksService booksService)
        {
            _logger = logger;
            _booksService = booksService;
        }

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns>Get a collection of all registered books.</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ListBookDto>))]
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
        [HttpGet("{id:int}", Name = "Get")]
        [ProducesResponseType(200, Type = typeof(ListBookDto))]
        [ProducesResponseType(404)]
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(404, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(500, Type = typeof(ModelStateDictionary))]
        public IActionResult Post([FromBody] CreateBookDto dto)
        {
            if(dto == null || !ModelState.IsValid)
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
        [HttpPatch("{id:int}", Name = "Patch")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(404, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(500, Type = typeof(ModelStateDictionary))]
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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500, Type = typeof(ModelStateDictionary))]
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
