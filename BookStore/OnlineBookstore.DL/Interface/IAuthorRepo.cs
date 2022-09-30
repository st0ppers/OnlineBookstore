using BookStore.Models;
using BookStore.Models.Models;

namespace OnlineBookstore.DL.Interface
{
    public interface IAuthorRepo
    {
        private static List<Author> _authors;
        public Guid Id { get; set; }
        public Author GetById(int id);
        public Author AddUser(Author author);
        public Author UpdateUser(Author author);
        public Author DeleteUser(int authroId);
        public Guid GetGuidId();
    }
}
