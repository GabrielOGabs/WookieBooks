using Models.WookieBooks;
using System;
using System.Collections.Generic;
using System.Text;

namespace MockData.WookieBooks.Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        public UsersRepository(AppDbContext db) 
            : base(db)
        {
        }
    }
}
