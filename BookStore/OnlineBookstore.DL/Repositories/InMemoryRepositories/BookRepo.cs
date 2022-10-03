using BookStore.Models.Models;
using OnlineBookstore.DL.Interface;

namespace OnlineBookstore.DL.Repositories.InMemoryRepositories
{
    public class BookRepo : IBookRepo
    {
        public static List<Book> _books;


        public BookRepo()
        {
            _books = new List<Book>()
            {
                new()
                {
                    Id = 1,
                    AuthorId = 1,
                    Title = "Ime"
                },
                new()
                {
                    Id = 2,
                    AuthorId = 1,
                    Title = "Novo Ime"
                },

                new()
                {
                    Id = 3,
                    AuthorId = 2,
                    Title = "Staro Ime"
                },


            };
        }
        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }

        public Book GetById(int id)
        {
            return _books.FirstOrDefault(x => x.Id == id);
        }

        public Book AddBook(Book book)
        {
            _books.Add(book);
            return book;
        }

        public Book GetByTitle(string title)
        {
            return _books.FirstOrDefault(x => x.Title == title);
        }

        public Book UpdateBook(Book book)
        {
            var existingBook = _books.FirstOrDefault(x => x.Id == book.Id);
            if (existingBook == null)
            {
                return null;
            }

            _books.Remove(existingBook);
            _books.Add(book);
            return book;

        }

        public Book DeleteBook(int bookId)
        {
            var book = _books.FirstOrDefault(x => x.Id == bookId);
            _books.Remove(book);
            return book;
        }
    }
}
