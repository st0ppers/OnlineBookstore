using System.Data.SqlClient;
using BookStore.Models.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnlineBookstore.DL.Interface;

namespace OnlineBookstore.DL.Repositories.MsSQL
{
    public class BookSqlRepository : IBookRepo
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BookSqlRepository> _logger;

        public BookSqlRepository(ILogger<BookSqlRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM Books WITH (NOLOCK)";
                    await conn.OpenAsync();
                    return await conn.QueryAsync<Book>(query);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from{nameof(GetAllBooks)} with error message: {e.Message}");
                throw;
            }
        }

        public async Task<Book> GetById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Book>("SELECT * FROM Books WITH (NOLOCK) WHERE Id=@Id", new { Id = id });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from{nameof(GetById)} with error message: {e.Message}");
                throw;
            }
        }

        public async Task<Book> GetByTitle(string tilte)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Book>("SELECT * FROM Books WITH (NOLOCK) WHERE Title=@Title", new { Title = tilte });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from{nameof(GetByTitle)} with error message: {e.Message}");
                throw;
            }
        }

        public async Task<Book?> AddBook(Book book)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return (await conn.QueryAsync<Book>(
                        "INSERT INTO Books (AuthorId,Title,LastUpdated,Quantity,Price) " +
                        "VALUES(@AuthorId,@Title,@LastUpdated,@Quantity,@Price)", book)).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(AddBook)} with message: {e.Message}");
                throw;
            }
        }

        public async Task<Book> UpdateBook(Book book)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var resut =await conn.QueryAsync(
                        "UPDATE Books SET AuthorId=@AuthorId,Title=@Title,LastUpdated=@LastUpdated,Quantity=@Quantity,Price=@Price WHERE Id=@Id"
                        , book);

                    return resut.SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(UpdateBook)} with message: {e.Message}");
                throw;
            }
        }

        public async Task<Book> DeleteBook(int bookId)
        {
            throw new NotImplementedException();
        }
    }
}
