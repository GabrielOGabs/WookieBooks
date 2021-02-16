using System.ComponentModel.DataAnnotations;

namespace Models.WookieBooks.Dto
{
    public class LoginAttemptDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
