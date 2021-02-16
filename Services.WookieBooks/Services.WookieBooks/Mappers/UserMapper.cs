using Models.WookieBooks;
using Models.WookieBooks.Dto;
using System.Linq;

namespace Services.WookieBooks.Mappers
{
    public class UserMapper
    {
        static public ListUserDto MapListUserDtoFromUser(User user)
        {
            if (user != null)
            {
                return new ListUserDto()
                {
                    Id = user.Id,
                    Login = user.Login,
                    FullName = user.FullName,
                    BooksOwned = user.OwnedBooks
                        .Select(x => x.Title)
                        .ToList()
                };
            }
            else
            {
                return null;
            }
        }
    }
}