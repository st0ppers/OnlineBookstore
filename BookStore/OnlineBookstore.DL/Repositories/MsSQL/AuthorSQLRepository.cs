using System.Data.SqlClient;
using AutoMapper;
using BookStore.Models.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnlineBookstore.DL.Interface;

namespace OnlineBookstore.DL.Repositories.MsSQL
{
    public class AuthorSqlRepository : IAuthorRepo
    {
        private readonly ILogger<AuthorSqlRepository> _logger;
        private readonly IConfiguration _configuration;
        public AuthorSqlRepository(ILogger<AuthorSqlRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
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
                    return (await conn.QueryAsync<Author>($"INSERT INTO Authors (Name,Age,DateOfBirth,NickName) VALUES(@Name,@Age,@DateOfBirth,@NickName)"
                        , author)).SingleOrDefault();

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
                    return (await conn.QueryAsync($"UPDATE Authors SET Name=@Name,Age=@Age,DateOfBirth=@DateOfBirth,NickName=@NickName WHERE Id=@Id"
                        ,
                        author)).SingleOrDefault();
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
                    (await conn.QueryAsync<Author>($"DELETE FROM Authors  WHERE Id=@Id",
                        new { Id = authorId })).SingleOrDefault();
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
            return true;
        }
    }
}
