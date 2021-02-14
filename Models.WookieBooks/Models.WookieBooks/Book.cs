using System;

namespace Models.WookieBooks
{
    public class Book
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public byte[] CoverImage { get; set; }

        public decimal Price { get; set; }
    }
}
