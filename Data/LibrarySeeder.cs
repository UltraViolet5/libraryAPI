using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.Models;

namespace LibraryAPI.Data
{
    public class LibrarySeeder
    {
        private readonly LibraryContext _context;
        private readonly User _admin;
        private IEnumerable<User> _usersList { get; set; }

        public LibrarySeeder(LibraryContext context)
        {
            _context = context;
            _admin = new User()
            {
                Id = "ab7f43cb-2eb6-480d-ad1f-0406421b699b",
                Email = "admin",
                // Password = admin
                PasswordHash = "$2a$11$3C0BAwSbIIGYT8wiYt2xauRoyhCJMPC9SknVBW5JcAjOTN47oDfgS",
                UserName = "Admin Admin",
                BirthDate = new DateTime(2001, 2, 14),
                Localization = "Cracow"
            };
            _usersList = GetUsers();
        }

        public void Seed()
        {
            if (_context.Database.CanConnect())
            {

                if (!_context.Book.Any())
                {
                    var books = GetBooks();
                    _context.Book.AddRange(books);
                    _context.SaveChanges();
                }

                if (!_context.Bookcase.Any())
                {
                    var bookcases = GetBookcases();
                    _context.Bookcase.AddRange(bookcases);
                    _context.SaveChanges();
                }

                if (!_context.Borrowing.Any())
                {
                    var borrowings = GetBorrowings();
                    _context.Borrowing.AddRange(borrowings);
                    _context.SaveChanges();
                }

                if (!_context.User.Any())
                {
                    var users = GetUsers();
                    _context.User.AddRange(users);
                    _context.SaveChanges();
                }

                if (!_context.Friends.Any())
                {
                    var friends = GetFriends();
                    _context.Friends.AddRange(friends);
                    _context.SaveChanges();
                }
            }
        }

        private IEnumerable<User> GetUsers()
        {
            return new List<User>()
            {
                _admin,
                new User()
                {
                    Id = "bcc79cc5-3aef-4e2a-b41b-f34dd5d36afd",
                    UserName = "Janek",
                    Email = "Email",
                    PasswordHash = "$2a$11$3C0BAwSbIIGYT8wiYt2xauRoyhCJMPC9SknVBW5JcAjOTN47oDfgS",
                    Localization = "Cracow"
                },
                new User()
                {
                    Id = "1e138a90-60bb-4021-b8df-6dd1cc3bd7b2",
                    UserName = "Brian",
                    Email = "Email2",
                    PasswordHash = "$2a$11$3C0BAwSbIIGYT8wiYt2xauRoyhCJMPC9SknVBW5JcAjOTN47oDfgS",
                    Localization = "Warsaw"
                },
                new User()
                {
                    Id = "1e232a90-60bb-3597-b8df-6dd1cc2bd7b2",
                    UserName = "Pep",
                    Email = "Email3",
                    PasswordHash = "$2a$11$3C0BAwSbIIGYT8wiYt2xauRoyhCJMPC9SknVBW5JcAjOTN47oDfgS",
                    Localization = "Cracow"
                }
            };
        }

        private IEnumerable<Friends> GetFriends()
        {
            return new List<Friends>()
            {
                new Friends()
                {
                    UserId = _admin.Id,
                    FriendId = _usersList.ToArray()[1].Id
                },
                new Friends()
                {
                    UserId = _admin.Id,
                    FriendId = _usersList.ToArray()[2].Id
                },
                new Friends()
                {
                    UserId = _admin.Id,
                    FriendId = _usersList.ToArray()[3].Id
                }
            };
        }

        private IEnumerable<Borrowing> GetBorrowings()
        {
            return new List<Borrowing>()
            {
                new Borrowing()
                {
                    BookId = 5,
                    BorrowerId = _admin.Id,
                    ClientId = _usersList.ToList()[0].Id,
                    Date = new DateTime(2021, 5, 4),
                    ReturnDate = new DateTime(2021, 9, 3),
                },
                new Borrowing()
                {
                    BookId = 4,
                    BorrowerId = _admin.Id,
                    ClientId = _usersList.ToList()[1].Id,
                    Date = new DateTime(2021, 5, 2),
                    ReturnDate = new DateTime(2021, 10, 21),
                },
                new Borrowing()
                {
                    BookId = 3,
                    BorrowerId = _admin.Id,
                    ClientId = _usersList.ToList()[2].Id,
                    Date = new DateTime(2021, 5, 1),
                    ReturnDate = new DateTime(2021, 8, 12),
                }
            };
        }

        private IEnumerable<Bookcase> GetBookcases()
        {
            return new List<Bookcase>()
            {
                new Bookcase()
                {
                    Name = "Main"
                }
            };
        }

        private IEnumerable<Book> GetBooks()
        {
            List<Book> books = new List<Book>();

            books.Add(new Book()
            {
                // Id = 0,
                Title = "First book Title",
                Authors = "Jarek, Krzysiek",
                PublishingYear = new DateTime(2020, 1, 1),
                Read = true,
                Available = false,
                OwnerId = _admin.Id,
                Category = Category.Crime
            });
            books.Add(new Book()
            {
                // Id = 1,
                Title = "Second super book",
                Authors = "Jarek",
                PublishingYear = new DateTime(2020, 1, 1),
                Available = true,
                Read = true,
                OwnerId = _admin.Id,
                Category = Category.Novel
            });
            books.Add(new Book()
            {
                // Id = 2,
                Title = "Third book",
                Authors = "Krzysiek",
                PublishingYear = new DateTime(2020, 1, 1),
                Available = true,
                Read = false,
                OwnerId = _admin.Id,
                Category = Category.Documentary
            });
            books.Add(new Book()
            {
                // Id = 3,
                Title = "Book1",
                Authors = "Jarek, Krzysiek",
                PublishingYear = new DateTime(2020, 1, 1),
                Read = true,
                Available = false,
                OwnerId = _admin.Id,
                Category = Category.Crime
            });
            books.Add(new Book()
            {
                // Id = 4,
                Title = "Book2",
                Authors = "Jarek",
                PublishingYear = new DateTime(2020, 1, 1),
                Available = true,
                Read = true,
                OwnerId = _admin.Id,
                Category = Category.Novel
            });
            books.Add(new Book()
            {
                // Id = 5,
                Title = "Book3",
                Authors = "Krzysiek",
                PublishingYear = new DateTime(2020, 1, 1),
                Available = true,
                Read = false,
                OwnerId = _admin.Id,
                Category = Category.Documentary
            });


            return books;
        }
    }
}
