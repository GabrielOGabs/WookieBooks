using Models.WookieBooks.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models.WookieBooks
{
    public class Book : IAppEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        public byte[] CoverImage { get; set; }

        [Required]
        public decimal Price { get; set; }

        public User CreatedBy { get; set; }
    }
}
