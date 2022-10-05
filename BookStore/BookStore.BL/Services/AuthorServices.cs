using System.Collections;
using System.Net;
using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using Microsoft.Extensions.Logging;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.Services
{
    public class AuthorServices : IAuthorService
    {
        private readonly IMapper _mapper;
        public readonly IAuthorRepo _authorRepo;
        private readonly ILogger<AuthorServices> _logger;
        public AuthorServices(IAuthorRepo authorRepo, IMapper mapper, ILogger<AuthorServices> logger)
        {
            _authorRepo = authorRepo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<Author?>> GetAllAuthors()
        {
            return await _authorRepo.GetAllAuthors();
        }

        public async Task<Author?> GetById(int id)
        {
            return await _authorRepo.GetById(id);
        }

        public async Task<Author?> GetAuthorByName(string name)
        {
            return await _authorRepo.GetAuthorByName(name); 
        }

        public async Task<AddAuthorResponse?> AddAuthor(AddAuthorRequest? authorRequest)
        {
            if (await _authorRepo.GetAuthorByName(authorRequest.Name) != null)
            {
                return new AddAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Bad request"
                };
            }

            var author = _mapper.Map<Author>(authorRequest);
            var result = await _authorRepo.AddAuthor(author);
            return new AddAuthorResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Auhtor = result
            };
        }

        public async Task<AddAuthorResponse?> UpdateAuthor(AddAuthorRequest? authorRequest)
        {
            if (await _authorRepo.GetAuthorByName(authorRequest.Name) != null)
            {
                return new AddAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Bad request"
                };
            }
            var author = _mapper.Map<Author>(authorRequest);
            var result = await _authorRepo.UpdateAuthor(author);
            return new AddAuthorResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Auhtor = result
            };
        }

        public async Task<Author?> DeleteAuthor(int authorId)
        {
            return await _authorRepo.DeleteAuthor(authorId);
        }

        public async Task<bool> AddMultipleAuthors(IEnumerable<Author> authorCollection)
        {
            return await _authorRepo.AddMultipleAuthors(authorCollection);
        }
    }
}
