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
        public IEnumerable<Author?> GetAllAuthors()
        {
            try
            {
                throw new Exception();
                return _authorRepo.GetAllAuthors();
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}");
                throw;
            }
        }

        public Author? GetById(int id)
        {
            return _authorRepo.GetById(id);
        }

        public Author? GetAuthorByName(string name)
        {
            return _authorRepo.GetAuthorByName(name);
        }

        public AddAuthorResponse? AddAuthor(AddAuthorRequest? authorRequest)
        {
            if (_authorRepo.GetAuthorByName(authorRequest.Name) != null)
            {
                return new AddAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Bad request"
                };
            }

            var author = _mapper.Map<Author>(authorRequest);
            var result = _authorRepo.AddAuthor(author);
            return new AddAuthorResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Auhtor = result
            };
        }

        public AddAuthorResponse? UpdateAuthor(AddAuthorRequest? authorRequest)
        {
            if (_authorRepo.GetAuthorByName(authorRequest.Name) != null)
            {
                return new AddAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Bad request"
                };
            }

            var author = _mapper.Map<Author>(authorRequest);
            var result = _authorRepo.UpdateAuthor(author);
            return new AddAuthorResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Auhtor = result
            };
        }

        public Author DeleteAuthor(int authorId)
        {
            return _authorRepo.DeleteAuthor(authorId);
        }
    }
}
