using Models.WookieBooks.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.WookieBooks
{
    public class User : IAppEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public List<Book> OwnedBooks { get; set; }

        public User()
        {
            OwnedBooks = new List<Book>();
        }
    }
}
