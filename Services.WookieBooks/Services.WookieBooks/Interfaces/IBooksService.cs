using Models.WookieBooks.Dto;
using System;
using System.Collections.Generic;

namespace Services.WookieBooks.Interfaces
{
    public interface IBooksService
    {
        List<ListBookDto> GetAll();

        ListBookDto Get(int id);

        int Delete(int id);

        int Update(UpdateBookDto book);

        int Create(CreateBookDto book);
    }
}
