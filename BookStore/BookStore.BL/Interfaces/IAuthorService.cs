using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        public Task<IEnumerable<Author?>> GetAllAuthors();
        public Task<Author?> GetById(int id);
        public Task<Author?> GetAuthorByName(string name);
        public Task<AddAuthorResponse?> AddAuthor(AddAuthorRequest? authorRequest);
        public Task<AddAuthorResponse?> UpdateAuthor(AddAuthorRequest? authorRequest);
        public Task<AddAuthorResponse> DeleteAuthor(int id);
        public Task<bool> AddMultipleAuthors(IEnumerable<Author> authorCollection);
    }
}
