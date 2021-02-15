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
    [Route("[controller]")]
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
        public IEnumerable<ListBookDto> Get()
        {
            return _booksService.GetAll();
        }
    }
}
