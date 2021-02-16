using MockData.WookieBooks;
using MockData.WookieBooks.Repositories;
using Models.WookieBooks.Dto;
using Services.WookieBooks.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Services.WookieBooks.Mappers;

namespace Services.WookieBooks
{
    public class AuthService : IAuthService
    {
        private readonly UsersRepository _usersRepository;

        public AuthService(AppDbContext db)
        {
            _usersRepository = new UsersRepository(db);
        }

        public AuthorizedUserDto Login(LoginAttemptDto dto)
        {
            var user = _usersRepository
                .GetByLoginAndPassword(dto.Login, dto.Password);

            return UserMapper.MapAuthorizedUserDtoFromUser(user);
        }
    }
}
