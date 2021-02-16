using Models.WookieBooks.Dto;

namespace Services.WookieBooks.Interfaces
{
    public interface IAuthService
    {
        AuthorizedUserDto Login(LoginAttemptDto dto);
    }
}
