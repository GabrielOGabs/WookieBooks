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
                var adminUser = new User
                {
                    FullName = "API Administrator",
                    Login = "admin",
                    Password = "Welcome#123"
                };

                var hpUser = new User
                {
                    FullName = "Harry Potter Fanatic",
                    Login = "h.potter",
                    Password = "HP#123"
                };

                var shUser = new User
                {
                    FullName = "Stephen Hawking Enthusiast",
                    Login = "s.hawking",
                    Password = "HP#123"
                };

                context.Users.Add(adminUser);
                context.Users.Add(hpUser);
                context.Users.Add(shUser);

                var books = new List<Book>
                {
                    new Book
                    {
                        Title = "Harry Potter and the Philosopher's Stone",
                        Description = "Escape to Hogwarts with the unmissable series that has sparked a lifelong reading journey for children and families all over the world. The magic starts here.",
                        Author = "J.K. Rowling",
                        CoverImage = new byte[0],
                        Price = 11.20m,
                        CreatedBy = hpUser,
                    },
                    new Book
                    {
                        Title = "Harry Potter and the Chamber of Secrets",
                        Description = "Harry Potter's summer has included the worst birthday ever, doomy warnings from a house-elf called Dobby, and rescue from the Dursleys by his friend Ron Weasley in a magical flying car!",
                        Author = "J.K. Rowling",
                        CoverImage = new byte[0],
                        Price = 12.38m,
                        CreatedBy = hpUser,
                    },
                    new Book
                    {
                        Title = "Harry Potter and the Prisoner of Azkaban",
                        Description = "When the Knight Bus crashes through the darkness and screeches to a halt in front of him, it's the start of another far from ordinary year at Hogwarts for Harry Potter.",
                        Author = "J.K. Rowling",
                        CoverImage = new byte[0],
                        Price = 12.38m,
                        CreatedBy = hpUser,
                    },
                    new Book
                    {
                        Title = "Harry Potter and the Goblet of Fire",
                        Description = "The Triwizard Tournament is to be held at Hogwarts. Only wizards who are over seventeen are allowed to enter – but that doesn't stop Harry dreaming that he will win the competition.",
                        Author = "J.K. Rowling",
                        CoverImage = new byte[0],
                        Price = 31.68m,
                        CreatedBy = hpUser,
                    },
                    new Book
                    {
                        Title = "Harry Potter and the Order of the Phoenix",
                        Description = "Dark times have come to Hogwarts. After the Dementors' attack on his cousin Dudley, Harry Potter knows that Voldemort will stop at nothing to find him.",
                        Author = "J.K. Rowling",
                        CoverImage = new byte[0],
                        Price = 15.84m,
                        CreatedBy = hpUser,
                    },
                    new Book
                    {
                        Title = "Harry Potter and the Half-Blood Prince",
                        Description = "When Dumbledore arrives at Privet Drive one summer night to collect Harry Potter, his wand hand is blackened and shrivelled, but he does not reveal why.",
                        Author = "J.K. Rowling",
                        CoverImage = new byte[0],
                        Price = 17.82m,
                        CreatedBy = hpUser,
                    },
                    new Book
                    {
                        Title = "Harry Potter and the Deathly Hallows",
                        Description = "As he climbs into the sidecar of Hagrid's motorbike and takes to the skies, leaving Privet Drive for the last time, Harry Potter knows that Lord Voldemort and the Death Eaters are not far behind.",
                        Author = "J.K. Rowling",
                        CoverImage = new byte[0],
                        Price = 17.82m,
                        CreatedBy = hpUser,
                    },
                    new Book
                    {
                        Title = "A Brief History of Time",
                        Description = "This landmark book is for those of us who prefer words to equations; this is the story of the ultimate quest for knowledge, the ongoing search for the secrets at the heart of time and space.",
                        Author = "Stephen W. Hawking",
                        CoverImage = new byte[0],
                        Price = 19.80m,
                        CreatedBy = shUser,
                    },
                    new Book
                    {
                        Title = "The Grand Design",
                        Description = "When and how did the universe begin? Why are we here? What is the nature of reality? Is the apparent “grand design” of our universe evidence of a benevolent creator who set things in motion—or does science offer another explanation?",
                        Author = "Stephen W. Hawking",
                        CoverImage = new byte[0],
                        Price = 10.99m,
                        CreatedBy = shUser,
                    }
                    ,
                    new Book
                    {
                        Title = "Black Holes and Baby Universes: And Other Essays",
                        Description = "In his phenomenal bestseller A Brief History of Time, Stephen Hawking literally transformed the way we think about physics, the universe, reality itself. In these thirteen essays and one remarkable extended interview, the man widely regarded as the most brilliant theoretical physicist since Einstein returns to reveal an amazing array of possibilities for understanding our universe.",
                        Author = "Stephen W. Hawking",
                        CoverImage = new byte[0],
                        Price = 10.99m,
                        CreatedBy = shUser,
                    }
                };

                context.Books.AddRange(books);

                context.SaveChanges();
            }
        }
    }
}
