using Models.WookieBooks;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MockData.WookieBooks.Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        public UsersRepository(AppDbContext db) 
            : base(db)
        {
        }

        public override User Get(int key)
        {
            return Context.Users
                .Include(u => u.OwnedBooks)
                .Where(u => u.Id == key)
                .SingleOrDefault();
        }

        public override List<User> GetAll()
        {
            return Context.Users
                .Include(u => u.OwnedBooks)
                .ToList();
        }

        public User GetByLoginAndPassword(string login, string password)
        {
            return Context.Users
                .Where(u => u.Login == login && u.Password == password)
                .SingleOrDefault();
        }

        public bool CheckIfExists(string login, int? updatingId)
        {
            if (updatingId.HasValue)
            {
                return Context.Users
                    .Any(b => b.Login == login && b.Id != updatingId.Value);
            }
            else
            {
                return Context.Users
                    .Any(b => b.Login == login);
            }
        }
    }
}
