using Models.WookieBooks;
using Models.WookieBooks.Dto;

namespace Services.WookieBooks.Mappers
{
    public class BookMapper
    {
        static public ListBookDto MapListBookDtoFromBook(Book book)
        {
            return new ListBookDto()
            {
                Author = book.Author,
                CoverImage = book.CoverImage,
                CreatedBy = book.CreatedBy.Name,
                Description = book.Description,
                Id = book.Id,
                Price = book.Price,
                Title = book.Title
            };
        }
    }
}