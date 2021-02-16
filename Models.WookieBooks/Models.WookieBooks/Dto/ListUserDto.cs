using System;
using System.Collections.Generic;
using System.Text;

namespace Models.WookieBooks.Dto
{
    public class ListUserDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Login { get; set; }

        public List<string> BooksOwned { get; set; }
    }
}
