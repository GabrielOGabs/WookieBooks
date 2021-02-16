using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.WookieBooks.Dto
{
    public class UpdateUserDto
    {
        [Required]
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}