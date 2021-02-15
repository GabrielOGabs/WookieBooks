using Models.WookieBooks;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MockData.WookieBooks.Repositories
{
    public class BooksRepository : BaseRepository<Book>
    {
        public BooksRepository(AppDbContext appDbContext) 
            : base(appDbContext)
        {
        }

        public override Book Get(int key)
        {
            return Context.Books
                .Include(b => b.CreatedBy)
                .Where(b => b.Id == key)
                .SingleOrDefault();
        }

        public override List<Book> GetAll()
        {
            return Context.Books
                .Include(b => b.CreatedBy)
                .ToList();
        }

        public bool CheckIfExists(string title, int? updatingId = null)
        {
            if (updatingId.HasValue)
            {
                return Context.Books
                    .Any(b => b.Title == title && b.Id != updatingId.Value);
            }
            else
            {
                return Context.Books
                    .Any(b => b.Title == title);
            }
        }

        public bool CheckIfIdExists(int id)
        {
            return Context.Books
                .Any(b => b.Id == id);
        }
    }
}
