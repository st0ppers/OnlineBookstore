using System.Data;
using AutoMapper;
using BookStore.AutoMapper;
using BookStore.BL.Services;
using BookStore.Controllers;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OnlineBookstore.DL.Interface;

namespace BookStore.Test
{
    public class BookTest
    {

        private IList<Book> _books = new List<Book>()
        {
            new()
            {
                Id = 1,
                Title = "Title",
                AuthorId = 13,
                LastUpdated = DateTime.Now,
                Price = 20,
                Quantity = 1,
            },
            new()
            {
                Id = 2,
                Title = "Another Title",
                AuthorId = 14,
                LastUpdated = DateTime.Now,
                Price = 25,
                Quantity = 13,
            },
        };

        private readonly IMapper _mapper;
        private readonly Mock<ILogger<BookController>> _logger;
        private readonly Mock<IBookRepo> _bookRepositoryMock;

        //BookServices(IAuthorRepo authorRepo, IMapper mapper, ILogger<BookServices> logger)
        public BookTest()
        {
            var mockMapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMappings()));
            _mapper = mockMapperConfig.CreateMapper();
            _logger = new Mock<ILogger<BookController>>();
            _bookRepositoryMock = new Mock<IBookRepo>();
        }

        [Fact]
        public async Task Book_GetAll_Count_Check()
        {
            //setup
            var excpectedCount = 2;

            _bookRepositoryMock.Setup(x => x.GetAllBooks())
                .ReturnsAsync(() => _books);
            //inject 
            var service = new BookService(_bookRepositoryMock.Object, _mapper);

            var controller = new BookController(service, _logger.Object);
            //Act
            var result = await controller.GetAll();

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var books = okObjectResult.Value as IEnumerable<Book>;

            Assert.NotNull(books);
            Assert.NotEmpty(books);
            Assert.Equal(excpectedCount, books.Count());

        }
        [Fact]
        public async Task Book_GetById_Check()
        {
            //setup[
            var bookId = 1;

            _bookRepositoryMock.Setup(x => x.GetById(bookId))
                .ReturnsAsync(_books.First(x => x.Id == bookId));

            //inject 
            var service = new BookService(_bookRepositoryMock.Object, _mapper);

            var controller = new BookController(service, _logger.Object);
            //Act
            var result = await controller.GetById(bookId);

            //assert 
            var okObjectRes = result as OkObjectResult;
            Assert.NotNull(okObjectRes);

            var book = okObjectRes.Value as Book;
            Assert.NotNull(book);
            Assert.Equal(bookId, book.Id);
        }
        [Fact]
        public async Task Book_GetAuthorById_NotFound()
        {
            //setup[
            var bookId = 23;

            _bookRepositoryMock.Setup(x => x.GetById(bookId))
                .ReturnsAsync(_books.FirstOrDefault(x => x.Id == bookId));

            //inject 
            var service = new BookService(_bookRepositoryMock.Object, _mapper);

            var controller = new BookController(service, _logger.Object);
            //Act
            var result = await controller.GetById(bookId);

            //assert 
            var notFoundObjectResult = result as NotFoundResult;
            Assert.NotNull(notFoundObjectResult);

        }
        [Fact]
        public async Task Book_AddBookOk()
        {
            //setup
            var bookId = 3;
            var bookRequest = new AddBookRequest()
            {
                Id = bookId,
                Title = "some Title",
                AuthorId = 14,
                LastUpdated = DateTime.Now,
                Price = 25,
                Quantity = 13,
            };


            _bookRepositoryMock.Setup(x => x.AddBook(It.IsAny<Book>()))
                .Callback(() => _books.Add(new Book()
                {
                    Id = bookId,
                    Title = bookRequest.Title,
                    AuthorId = bookRequest.AuthorId,
                    LastUpdated = bookRequest.LastUpdated,
                    Price = bookRequest.Price,
                    Quantity = bookRequest.Quantity,
                })).ReturnsAsync(() => _books.FirstOrDefault(x => x.Id == bookId));

            //inject 
            var service = new BookService(_bookRepositoryMock.Object, _mapper);
            var controller = new BookController(service, _logger.Object);

            //act
            var result = await controller.AddBook(bookRequest);

            //asert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resltValue = okObjectResult.Value as AddBookResponse;
            Assert.NotNull(resltValue);
            Assert.Equal(bookId, resltValue.Book.Id);

        }
        [Fact]
        public async Task Book_AddBookWhenExist()
        {
            var bookId = 1;
            //setup
            var bookRequest = new AddBookRequest()
            {
                Id = bookId,
                Title = "Title",
                AuthorId = 13,
                LastUpdated = DateTime.Now,
                Price = 20,
                Quantity = 1,
            };

            _bookRepositoryMock.Setup(x => x.GetById(bookRequest.Id)).ReturnsAsync(
                _books.FirstOrDefault(x => x.Id == bookRequest.Id));

            _bookRepositoryMock.Setup(x => x.AddBook(It.IsAny<Book>()))
                .Callback(() => _books.Add(new Book()
                {
                    Id = bookId,
                    Title = bookRequest.Title,
                    AuthorId = bookRequest.AuthorId,
                    LastUpdated = bookRequest.LastUpdated,
                    Price = bookRequest.Price,
                    Quantity = bookRequest.Quantity,
                })).ReturnsAsync(() => _books.FirstOrDefault(x => x.Id == bookId));

            //inject 
            var service = new BookService(_bookRepositoryMock.Object, _mapper);
            var controller = new BookController(service, _logger.Object);

            //act
            var result = await controller.AddBook(bookRequest);

            //asert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);

            var resutlValue = notFoundObjectResult.Value as AddBookResponse;
            Assert.NotNull(resutlValue);
        }

        [Fact]
        public async Task Book_update_ok()
        {
            //setup
            var bookId = 1;
            var bookRequest = new AddBookRequest()
            {
                Id = bookId,
                Title = "Title",
                AuthorId = 13,
                LastUpdated = DateTime.Now,
                Price = 40,
                Quantity = 1,
            };



            _bookRepositoryMock.Setup(x => x.UpdateBook(It.IsAny<Book>()))
                .Callback(() =>
                {
                    var book = _books.FirstOrDefault(x => x.Id == bookId);
                    book.Price = bookRequest.Price;

                }).ReturnsAsync(() => _books.FirstOrDefault(x => x.Id == bookId));

            _bookRepositoryMock.Setup(x => x.GetById(bookId)).ReturnsAsync(
                _books.FirstOrDefault(x => x.Id == bookId));
            _bookRepositoryMock.Setup(x => x.GetByTitle(bookRequest.Title)).ReturnsAsync(
                _books.FirstOrDefault(x => x.Id == bookId));


            var service = new BookService(_bookRepositoryMock.Object, _mapper);
            var controller = new BookController(service, _logger.Object);

            //act
            var result = await controller.UpdateBook(bookRequest);

            //asert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resutlValue = okObjectResult.Value as AddBookResponse;
            Assert.NotNull(resutlValue);
            Assert.Equal(resutlValue.Book.Price, bookRequest.Price);
        }

        [Fact]
        public async Task Book_update_Bad()
        {
            //setup
            var bookId = 5;
            var bookRequest = new AddBookRequest()
            {
                Id = bookId,
                Title = "Title",
                AuthorId = 13,
                LastUpdated = DateTime.Now,
                Price = 100,
                Quantity = 1,

            };

            _bookRepositoryMock.Setup(x => x.UpdateBook(It.IsAny<Book>()))
                .Callback(() =>
                {
                    var author = _books.FirstOrDefault(x => x.Id == bookId);
                    author.Price = bookRequest.Price;

                }).ReturnsAsync(() => _books.FirstOrDefault(x => x.Id == bookId));

            _bookRepositoryMock.Setup(x => x.GetById(bookRequest.Id)).ReturnsAsync(
                _books.FirstOrDefault(x => x.Id == bookId));

            _bookRepositoryMock.Setup(x => x.GetByTitle(bookRequest.Title)).ReturnsAsync(
                _books.FirstOrDefault(x => x.Id == bookId));


            var service = new BookService(_bookRepositoryMock.Object, _mapper);
            var controller = new BookController(service, _logger.Object);

            //act
            var result = await controller.UpdateBook(bookRequest);

            //asert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);

            var resutlValue = notFoundObjectResult.Value as AddBookResponse;
            Assert.NotNull(resutlValue);
            Assert.Equal(2, _books.Count);
        }

        [Fact]
        public async Task Book_Delete_Ok()
        {
            //setup
            var bookId = 1;
            var expectedToDelete = _books.FirstOrDefault(x => x.Id == bookId);

            _bookRepositoryMock.Setup(x => x.GetById(bookId))
                .ReturnsAsync(_books.FirstOrDefault(x => x.Id == bookId));

            _bookRepositoryMock.Setup(x => x.DeleteBook(bookId))
                .Callback(() =>
                {
                    _books.RemoveAt(expectedToDelete.Id);

                })!.ReturnsAsync(() => _books.FirstOrDefault(x => x.Id == bookId));

            //inject
            var service = new BookService(_bookRepositoryMock.Object, _mapper);
            var controller = new BookController(service, _logger.Object);

            //act
            var result = await controller.DeleteBook(bookId);

            //asert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultValue = okObjectResult.Value as Book;
            Assert.NotNull(resultValue);
            Assert.Equal(1, _books.Count);
        }

        [Fact]
        public async Task Book_Delete_Bad()
        {
            //setup
            var bookId = 4;
            var expectedToDelete = _books.FirstOrDefault(x => x.Id == bookId);

            _bookRepositoryMock.Setup(x => x.GetById(bookId))
                .ReturnsAsync(_books.FirstOrDefault(x => x.Id == bookId));

            _bookRepositoryMock.Setup(x => x.DeleteBook(bookId))
                .Callback(() =>
                {
                    _books.RemoveAt(bookId - 1);
                })!
                .ReturnsAsync(() => _books.FirstOrDefault(x => x.Id == bookId));

            //inject
            var service = new BookService(_bookRepositoryMock.Object, _mapper);
            var controller = new BookController(service, _logger.Object);

            //act
            var result = await controller.DeleteBook(bookId);

            //asert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);

            var resultValue = notFoundResult.Value as AddBookResponse;
            Assert.Null(resultValue);
        }

    }
}
