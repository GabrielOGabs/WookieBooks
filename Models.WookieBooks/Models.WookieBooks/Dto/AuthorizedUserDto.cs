using System;
using System.Collections.Generic;
using System.Text;

namespace Models.WookieBooks.Dto
{
    public class AuthorizedUserDto
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string FullName { get; set; }

        public string AuthToken { get; set; }
    }
}
