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
    public class UsersService : IUsersService
    {
        private readonly UsersRepository _usersRepository;

        public UsersService(AppDbContext db)
        {
            _usersRepository = new UsersRepository(db);
        }

        public bool CheckIfExists(string login, int? updatingId = null)
        {
            return _usersRepository.CheckIfExists(login, updatingId);
        }

        public bool CheckIfIdExists(int id)
        {
            return _usersRepository.CheckIfIdExists(id);
        }

        public int Create(CreateUserDto dto)
        {
            var user = new User()
            {
                Login = dto.Login,
                FullName = dto.FullName,
                Password = dto.Password
            };

            _usersRepository.Add(user);

            return user.Id;
        }

        public int Delete(int id)
        {
            return _usersRepository.Delete(id);
        }

        public ListUserDto Get(int id)
        {
            var user = _usersRepository.Get(id);
            return UserMapper.MapListUserDtoFromUser(user);
        }

        public List<ListUserDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Update(UpdateUserDto dto)
        {
            var user = _usersRepository.Get(dto.Id);

            if (!string.IsNullOrWhiteSpace(dto.Login))
                user.Login = dto.Login;

            if (!string.IsNullOrWhiteSpace(dto.FullName))
                user.FullName = dto.FullName;

            if (!string.IsNullOrWhiteSpace(dto.Password))
                user.Password = dto.Password;

            return _usersRepository.Update(user);
        }
    }
}
