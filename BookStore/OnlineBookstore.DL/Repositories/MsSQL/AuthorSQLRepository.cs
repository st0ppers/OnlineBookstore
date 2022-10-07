using System.Data.SqlClient;
using BookStore.Models.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnlineBookstore.DL.Interface;

namespace OnlineBookstore.DL.Repositories.MsSQL
{
    public class AuthorSqlRepository : IAuthorRepo
    {
        private readonly ILogger<AuthorSqlRepository> _logger;
        private readonly IConfiguration _configuration;
        private IBookRepo _bookRepo;
        public AuthorSqlRepository(ILogger<AuthorSqlRepository> logger, IConfiguration configuration, IBookRepo bookRepo)
        {
            _logger = logger;
            _configuration = configuration;
            _bookRepo = bookRepo;
        }
        public async Task<IEnumerable<Author?>> GetAllAuthors()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM Authors WITH (NOLOCK)";
                    await conn.OpenAsync();
                    return await conn.QueryAsync<Author>(query);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllAuthors)}:{e.Message}");
            }
            return Enumerable.Empty<Author>();
        }

        public async Task<Author?> GetById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WITH (NOLOCK) WHERE [Id] = @Id", new { Id = id });
                }

            }
            catch (Exception e)
            {
                _logger.LogError($" {e.Message}");
                return null;
            }

        }

        public async Task<bool> GetByIdBool(int authorId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    await conn.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WITH (NOLOCK) WHERE [Id] = @Id", new { Id = authorId });
                }
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($" {e.Message}");
                throw;
            }
        }

        public async Task<Author?> GetAuthorByName(string name)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WITH (NOLOCK) WHERE [Name] = @Name", new { Name = name });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($" {e.Message}");
                throw;
            }
        }

        public async Task<Author?> AddAuthor(Author? author)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return conn.QueryFirstOrDefaultAsync<Author>(
                        $"INSERT INTO Authors (Name,Age,DateOfBirth,NickName) VALUES(@Name,@Age,@DateOfBirth,@NickName)"
                        , author).Result;

                }
            }
            catch (Exception e)
            {
                _logger.LogError($" {e.Message}");
                throw;
            }
        }

        public async Task<Author?> UpdateAuthor(Author? author)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    await conn.ExecuteAsync($"UPDATE Authors SET Name=@Name,Age=@Age,DateOfBirth=@DateOfBirth,NickName=@NickName WHERE Id=@Id"
                        ,
                        author);
                    return author;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($" {e.Message}");
                throw;
            }
        }

        public async Task<Author?> DeleteAuthor(int authorId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await GetById(authorId);

                    if (await _bookRepo.GetByAuthorId(authorId))
                    {
                        return null;
                    }
                    (await conn.QueryAsync<Author>($"DELETE FROM Authors  WHERE Id=@Id",
                        new { Id = authorId })).SingleOrDefault();

                    await conn.QueryAsync("DELETE FROM Books WHERE AuthorId=@AuthorId", new { AuthorId = authorId });
                    return result;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($" {e.Message}");
                throw;
            }
        }

        public async Task<bool> AddMultipleAuthors(IEnumerable<Author> authorCollection)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query = "INSERT INTO Authors (Name,Age,DateOfBirth,NickName) VALUES(@Name,@Age,@DateOfBirth,@NickName)";

                    foreach (var item in authorCollection)
                    {
                        await conn.ExecuteAsync(query, item);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro from {nameof(AddMultipleAuthors)} message: {e.Message}");
                throw;
            }
        }
    }
}
