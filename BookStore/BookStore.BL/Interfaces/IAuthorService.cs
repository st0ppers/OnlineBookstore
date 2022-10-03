using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        public IEnumerable<Author?> GetAllAuthors();
        public Author? GetById(int id);
        public Author? GetAuthorByName(string name);
        public AddAuthorResponse? AddAuthor(AddAuthorRequest? authorRequest);
        public AddAuthorResponse? UpdateAuthor(AddAuthorRequest? authorRequest);
        public Author DeleteAuthor(int authorId);
        bool AddMultipleAuthors(IEnumerable<Author> authorCollection);
    }
}
