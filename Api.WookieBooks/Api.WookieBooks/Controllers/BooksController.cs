using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult Get()
        {
            var bookList = _booksService.GetAll();
            return Ok(bookList);
        }

        [HttpGet("{id:int}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var book = _booksService.Get(id);
            
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
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

        [HttpPatch("{id:int}", Name = "Patch")]
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

        [HttpDelete("{id:int}")]
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
