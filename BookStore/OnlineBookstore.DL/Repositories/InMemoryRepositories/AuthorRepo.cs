using BookStore.Models.Models;
using OnlineBookstore.DL.Interface;

namespace OnlineBookstore.DL.Repositories.InMemoryRepositories
{
    public class AuthorRepo : IAuthorRepo
    {
        public List<Author> _authors = new List<Author>();
        public Guid Id { get; set; }

        public AuthorRepo()
        {
            Id = Guid.NewGuid();
        }
        public Author GetById(int id)
        {
            return _authors.FirstOrDefault(x => x.Id == id);
        }

        public Author AddUser(Author author)
        {
            _authors.Add(author);
            return author;
        }

        public Author UpdateUser(Author author)
        {
            var existingAuthor = _authors.FirstOrDefault(x => x.Id == author.Id);
            if (existingAuthor==null)
            {
                return null;
            }

            _authors.Remove(existingAuthor);
            _authors.Add(author);
            return author;
        }

        public Author DeleteUser(int authroId)
        {
            var input = _authors.FirstOrDefault(x => x.Id == authroId);
            _authors.Remove(input);
            return input;
        }

        public Guid GetGuidId()
        {
            return Id;
        }
    }
}
