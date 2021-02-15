using System;
using System.Collections.Generic;
using System.Text;

namespace Models.WookieBooks.Dto
{
    public class CreateBookDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public byte[] CoverImage { get; set; }

        public decimal Price { get; set; }

        public int UserId { get; set; }
    }
}
