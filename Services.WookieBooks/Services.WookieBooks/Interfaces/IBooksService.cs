using Models.WookieBooks.Dto;
using System;
using System.Collections.Generic;

namespace Services.WookieBooks.Interfaces
{
    public interface IBooksService
    {
        /// <summary>
        /// Get a list of Books from the repository
        /// </summary>
        /// <returns>A list of Dtos containing listing information of books</returns>
        List<ListBookDto> GetAll();

        /// <summary>
        /// Get a single Book from the repository
        /// </summary>
        /// <param name="id">Id number of the book</param>
        /// <returns>A Dto containing listing information of a book</returns>
        ListBookDto Get(int id);

        /// <summary>
        /// Deletes a book and saves the operation with the repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(int id);

        /// <summary>
        /// Update information from a book and save it into the repository
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        int Update(UpdateBookDto dto);

        /// <summary>
        /// Creates a new Book and add it to the repository
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>The Id from the recently added book</returns>
        int Create(CreateBookDto dto);

        /// <summary>
        /// Check if the book already exists int the Datatable
        /// </summary>
        /// <param name="title">The title of the book</param>
        /// <param name="updatingId">The id from the book that is being updated</param>
        /// <returns>true if already exists</returns>
        bool CheckIfExists(string title, int? updatingId = null);

        /// <summary>
        /// Check if there is a book with given Id
        /// </summary>
        /// <param name="id">The title of the book</param>
        /// <returns>true if already exists</returns>
        bool CheckIfIdExists(int id);
    }
}