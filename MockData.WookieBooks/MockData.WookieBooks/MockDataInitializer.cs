using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models.WookieBooks;
using System;
using System.Collections.Generic;
using System.Text;

namespace MockData.WookieBooks
{
    public static class MockDataInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(serviceProvider
                .GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                var user = new User
                {
                    Id = 1,
                    FullName = "API Administrator",
                    Login = "admin",
                    Password = "Welcome#123"
                };

                context.Users.Add(user);

                var books = new List<Book>
                {
                    new Book
                    {
                        Title = "Harry Potter and the Philosopher's Stone",
                        Description = "Escape to Hogwarts with the unmissable series that has sparked a lifelong reading journey for children and families all over the world. The magic starts here.",
                        Author = "J.K. Rowling",
                        CoverImage = new byte[0],
                        Price = 12.34m,
                        CreatedBy = user,
                    },
                    new Book
                    {
                        Title = "Harry Potter and the Chamber of Secrets",
                        Description = "Harry Potter's summer has included the worst birthday ever, doomy warnings from a house-elf called Dobby, and rescue from the Dursleys by his friend Ron Weasley in a magical flying car!",
                        Author = "J.K. Rowling",
                        CoverImage = new byte[0],
                        Price = 16.90m,
                        CreatedBy = user,
                    },
                    new Book
                    {
                        Title = "Harry Potter and the Prisoner of Azkaban",
                        Description = "When the Knight Bus crashes through the darkness and screeches to a halt in front of him, it's the start of another far from ordinary year at Hogwarts for Harry Potter.",
                        Author = "J.K. Rowling",
                        CoverImage = new byte[0],
                        Price = 19.90m,
                        CreatedBy = user,
                    }
                };

                context.Books.AddRange(books);

                context.SaveChanges();
            }
        }
    }
}
