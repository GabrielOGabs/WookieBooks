using Models.WookieBooks.Dto;
using System;
using System.Collections.Generic;

namespace Services.WookieBooks.Interfaces
{
    public interface IUsersService
    {
        /// <summary>
        /// Get a single User from the repository
        /// </summary>
        /// <param name="id">Id number of the user</param>
        /// <returns>A Dto containing listing information of an user</returns>
        ListUserDto Get(int id);

        /// <summary>
        /// Deletes an user and saves the operation with the repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(int id);

        /// <summary>
        /// Update information from an user and save it into the repository
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        int Update(UpdateUserDto dto);

        /// <summary>
        /// Creates a new User and add it to the repository
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>The Id from the recently added user</returns>
        int Create(CreateUserDto dto);

        /// <summary>
        /// Check if the user already exists int the Datatable
        /// </summary>
        /// <param name="login">The login of the user</param>
        /// <param name="updatingId">The id from the user that is being updated</param>
        /// <returns>true if already exists</returns>
        bool CheckIfExists(string login, int? updatingId = null);
    }
}