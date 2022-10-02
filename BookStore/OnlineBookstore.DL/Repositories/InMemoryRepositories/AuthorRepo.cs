using BookStore.Models.Models;
using OnlineBookstore.DL.Interface;

namespace OnlineBookstore.DL.Repositories.InMemoryRepositories
{
    public class AuthorRepo : IAuthorRepo
    {
        public List<Author> _authors = new List<Author>()
        {
            new()
            {
                Age = 20,
                DateOfBirth = DateTime.Now,
                Id = 1,
                Name = "gosho",
                Nickname = "Goshetoo"
            },new()
            {
                Age = 26,
                DateOfBirth = DateTime.Now,
                Id = 2,
                Name = "pehso",
                Nickname = "Pesheto"
            },new()
            {
                Age = 30,
                DateOfBirth = DateTime.Now,
                Id = 3,
                Name = "Tosho",
                Nickname = "tosheto"
            },new()
            {
                Age = 40,
                DateOfBirth = DateTime.Now,
                Id = 4,
                Name = "ivan",
                Nickname = "ivancho"
            }
        };
        public Author GetById(int id)
        {
            return _authors.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _authors;
        }
        public Author AddAuthor(Author author)
        {
            _authors.Add(author);
            return author;
        }

        public Author UpdateAuthor(Author author)
        {
            var existingAuthor = _authors.FirstOrDefault(x => x.Id == author.Id);
            if (existingAuthor == null)
            {
                return null;
            }

            _authors.Remove(existingAuthor);
            _authors.Add(author);
            return author;
        }

        public Author DeleteAuthor(int authorId)
        {
            var input = _authors.FirstOrDefault(x => x.Id == authorId);
            _authors.Remove(input);
            return input;
        }
    }
}
