using MockData.WookieBooks;
using MockData.WookieBooks.Repositories;
using Models.WookieBooks;
using Models.WookieBooks.Dto;
using Services.WookieBooks.Interfaces;
using Services.WookieBooks.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.WookieBooks
{
    public class BooksService : IBooksService
    {
        private readonly BooksRepository _booksRepository;
        private readonly UsersRepository _usersRepository;

        public BooksService(AppDbContext db)
        {
            _booksRepository = new BooksRepository(db);
            _usersRepository = new UsersRepository(db);
        }

        public bool CheckIfExists(string title, int? updatingId = null)
        {
            return _booksRepository.CheckIfExists(title, updatingId);
        }

        public bool CheckIfIdExists(int id)
        {
            return _booksRepository.CheckIfIdExists(id);
        }

        public int Create(CreateBookDto dto)
        {
            var user = _usersRepository.Get(dto.UserId);

            var book = new Book()
            {
                Author = dto.Author,
                CoverImage = dto.CoverImage,
                Description = dto.Description,
                Price = dto.Price,
                Title = dto.Title,
                CreatedBy = user
            };

            _booksRepository.Add(book);

            return book.Id;
        }

        public int Delete(int id)
        {
            return _booksRepository.Delete(id);
        }

        public ListBookDto Get(int id)
        {
            var book = _booksRepository.Get(id);
            return BookMapper.MapListBookDtoFromBook(book);
        }

        public List<ListBookDto> GetAll()
        {
            return _booksRepository
                .GetAll()
                .Select(b => BookMapper.MapListBookDtoFromBook(b))
                .ToList();
        }

        public int Update(UpdateBookDto dto)
        {
            var book = _booksRepository.Get(dto.Id);

            book.Author = dto.Author;
            book.CoverImage = dto.CoverImage;
            book.Description = dto.Description;
            book.Price = dto.Price;
            book.Title = dto.Title;

            return _booksRepository.Update(book);
        }
    }
}
