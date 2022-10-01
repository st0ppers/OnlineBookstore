using BookStore.BL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Services
{
    internal class AuthorServices : IAuthorService
    {
        public readonly IAuthorService _authorRepo;
        public AuthorServices(IAuthorService authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public Guid GetGuidId()
        {
            return _authorRepo.GetGuidId();
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _authorRepo.GetAllAuthors();
        }

        public Author GetById(int id)
        {
            return _authorRepo.GetById(id);
        }

        public Author AddAuthor(Author author)
        {
            return _authorRepo.AddAuthor(author);
        }

        public Author UpdateAuthor(Author author)
        {
            return _authorRepo.UpdateAuthor(author);
        }

        public Author DeleteAuthor(int authorId)
        {
            return _authorRepo.DeleteAuthor(authorId);
        }
    }
}
