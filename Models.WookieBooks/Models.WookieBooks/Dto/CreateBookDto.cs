using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.WookieBooks.Dto
{
    public class CreateBookDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        public byte[] CoverImage { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int UserId { get; set; }
    }
}
