//using AutoMapper;
//using BookStore.AutoMapper;
//using BookStore.BL.Services;
//using BookStore.Controllers;
//using BookStore.Models.Models;
//using BookStore.Models.Requests;
//using BookStore.Models.Responses;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;
//using OnlineBookstore.DL.Interface;
//using NotFoundResult = Microsoft.AspNetCore.Mvc.NotFoundResult;

//namespace BookStore.Test
//{
//    public class AuthorTest
//    {
//        private IList<Author> _authors = new List<Author>()
//        {
//            new()
//            {
//                Id = 1,
//                Age = 13,
//                DateOfBirth = DateTime.Now,
//                Name = "Gosho",
//                Nickname = "Gopeto"
//            },
//            new()
//            {
//                Id = 2,
//                Age = 33,
//                DateOfBirth = DateTime.Now,
//                Name = "Pesho",
//                Nickname = "Peshkata"
//            },
//        };

//        private readonly IMapper _mapper;
//        private readonly Mock<ILogger<AuthorService>> _logger;
//        private readonly Mock<IAuthorRepo> _authorRepositoryMock;

//        //BookServices(IAuthorRepo authorRepo, IMapper mapper, ILogger<BookServices> logger)
//        public AuthorTest()
//        {
//            var mockMapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMappings()));
//            _mapper = mockMapperConfig.CreateMapper();
//            _logger = new Mock<ILogger<AuthorService>>();
//            _authorRepositoryMock = new Mock<IAuthorRepo>();
//        }

//        [Fact]
//        public async Task Author_GetAll_Count_Check()
//        {
//            //setup
//            var excpectedCount = 2;

//            _authorRepositoryMock.Setup(x => x.GetAllAuthors())
//                .ReturnsAsync(() => _authors);
//            //inject 
//            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _logger.Object);

//            var controller = new AuthorController(service, _mapper);
//            //Act
//            var result = await controller.Get();

//            //Assert
//            var okObjectResult = result as OkObjectResult;
//            Assert.NotNull(okObjectResult);

//            var authors = okObjectResult.Value as IEnumerable<Author>;

//            Assert.NotNull(authors);
//            Assert.NotEmpty(authors);
//            Assert.Equal(excpectedCount, authors.Count());

//        }
//        [Fact]
//        public async Task Author_GetById_Check()
//        {
//            //setup[
//            var authorId = 1;

//            _authorRepositoryMock.Setup(x => x.GetById(authorId))
//                .ReturnsAsync(_authors.First(x => x.Id == authorId));

//            //inject 
//            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _logger.Object);

//            var controller = new AuthorController(service, _mapper);
//            //Act
//            var result = await controller.GetByID(authorId);

//            //assert 
//            var okObjectRes = result as OkObjectResult;
//            Assert.NotNull(okObjectRes);

//            var author = okObjectRes.Value as Author;
//            Assert.NotNull(author);
//            Assert.Equal(authorId, author.Id);
//        }
//        [Fact]
//        public async Task Author_GetAuthorById_NotFound()
//        {
//            //setup[
//            var authorId = 23;

//            _authorRepositoryMock.Setup(x => x.GetById(authorId))
//                .ReturnsAsync(_authors.FirstOrDefault(x => x.Id == authorId));

//            //inject 
//            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _logger.Object);

//            var controller = new AuthorController(service, _mapper);
//            //Act
//            var result = await controller.GetByID(authorId);

//            //assert 
//            var notFoundObjectResult = result as NotFoundResult;
//            Assert.NotNull(notFoundObjectResult);

//        }
//        [Fact]
//        public async Task Author_AddAuthorOk()
//        {
//            //setup
//            var authorId = 3;
//            var authorRequest = new AddAuthorRequest()
//            {
//                NickName = "New nickname",
//                Age = 13,
//                DateOfBirth = DateTime.Now,
//                Name = "Test Another Name"
//            };


//            _authorRepositoryMock.Setup(x => x.AddAuthor(It.IsAny<Author>()))
//                .Callback(() => _authors.Add(new Author()
//                {
//                    Id = authorId,
//                    Name = authorRequest.Name,
//                    Age = authorRequest.Age,
//                    DateOfBirth = authorRequest.DateOfBirth,
//                    Nickname = authorRequest.NickName
//                })).ReturnsAsync(() => _authors.FirstOrDefault(x => x.Id == authorId));

//            //inject 
//            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _logger.Object);
//            var controller = new AuthorController(service, _mapper);

//            //act
//            var result = await controller.Add(authorRequest);

//            //asert
//            var okObjectResult = result as OkObjectResult;
//            Assert.NotNull(okObjectResult);

//            var resltValue = okObjectResult.Value as AddAuthorResponse;
//            Assert.NotNull(resltValue);
//            Assert.Equal(authorId, resltValue.Auhtor.Id);

//        }
//        [Fact]
//        public async Task Author_AddAuthorWhenExist()
//        {
//            var authorId = 1;
//            //setup
//            var authorRequest = new AddAuthorRequest()
//            {
//                Age = 13,
//                DateOfBirth = DateTime.Now,
//                Name = "Gosho",
//                NickName = "Gopeto"
//            };

//            _authorRepositoryMock.Setup(x => x.GetAuthorByName(authorRequest.Name)).ReturnsAsync(
//                _authors.FirstOrDefault(x => x.Name == authorRequest.Name));

//            _authorRepositoryMock.Setup(x => x.AddAuthor(It.IsAny<Author>()))
//                .Callback(() => _authors.Add(new Author()
//                {
//                    Id = authorId,
//                    Name = authorRequest.Name,
//                    Age = authorRequest.Age,
//                    DateOfBirth = authorRequest.DateOfBirth,
//                    Nickname = authorRequest.NickName
//                })).ReturnsAsync(() => _authors.FirstOrDefault(x => x.Id == authorId));

//            //inject 
//            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _logger.Object);
//            var controller = new AuthorController(service, _mapper);

//            //act
//            var result = await controller.Add(authorRequest);

//            //asert
//            var notFoundObjectResult = result as NotFoundObjectResult;
//            Assert.NotNull(notFoundObjectResult);

//            var resutlValue = notFoundObjectResult.Value as AddAuthorResponse;
//            Assert.NotNull(resutlValue);
//        }
//        [Fact]
//        public async Task Author_update_ok()
//        {
//            //setup
//            var auhtorId = 1;
//            var authorRequest = new AddAuthorRequest()
//            {
//                Age = 20,
//                DateOfBirth = DateTime.Now,
//                Name = "Gosho",
//                NickName = "Gopeto"
//            };



//            _authorRepositoryMock.Setup(x => x.UpdateAuthor(It.IsAny<Author>()))
//                .Callback(() =>
//                {
//                    var author = _authors.FirstOrDefault(x => x.Id == auhtorId);
//                    author.Age = authorRequest.Age;

//                }).ReturnsAsync(() => _authors.FirstOrDefault(x => x.Id == auhtorId));

//            _authorRepositoryMock.Setup(x => x.GetAuthorByName(authorRequest.Name)).ReturnsAsync(
//                _authors.FirstOrDefault(x => x.Name == authorRequest.Name));



//            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _logger.Object);
//            var controller = new AuthorController(service, _mapper);

//            //act
//            var result = await controller.Update(authorRequest);

//            //asert
//            var okObjectResult = result as OkObjectResult;
//            Assert.NotNull(okObjectResult);

//            var resutlValue = okObjectResult.Value as AddAuthorResponse;
//            Assert.NotNull(resutlValue);
//            Assert.Equal(resutlValue.Auhtor.Age, authorRequest.Age);

//        }
//        [Fact]
//        public async Task Author_update_Bad()
//        {
//            //setup
//            var auhtorId = 1;
//            var authorRequest = new AddAuthorRequest()
//            {
//                Age = 20,
//                DateOfBirth = DateTime.Now,
//                Name = "asdfasdf",
//                NickName = "Gopeto"
//            };

//            _authorRepositoryMock.Setup(x => x.UpdateAuthor(It.IsAny<Author>()))
//                .Callback(() =>
//                {
//                    var author = _authors.FirstOrDefault(x => x.Id == auhtorId);
//                    author.Age = authorRequest.Age;

//                }).ReturnsAsync(() => _authors.FirstOrDefault(x => x.Id == auhtorId));

//            _authorRepositoryMock.Setup(x => x.GetAuthorByName(authorRequest.Name)).ReturnsAsync(
//                _authors.FirstOrDefault(x => x.Name == authorRequest.Name));



//            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _logger.Object);
//            var controller = new AuthorController(service, _mapper);

//            //act
//            var result = await controller.Update(authorRequest);

//            //asert
//            var notFoundObjectResult = result as NotFoundObjectResult;
//            Assert.NotNull(notFoundObjectResult);

//            var resutlValue = notFoundObjectResult.Value as AddAuthorResponse;
//            Assert.NotNull(resutlValue);
//            Assert.Equal(2, _authors.Count);
//        }
//        [Fact]
//        public async Task Author_Delete_Ok()
//        {
//            //setup
//            var authorId = 1;
//            var expectedToDelete = _authors.FirstOrDefault(x => x.Id == authorId);

//            _authorRepositoryMock.Setup(x => x.GetById(authorId))
//                .ReturnsAsync(_authors.FirstOrDefault(x => x.Id == authorId));

//            _authorRepositoryMock.Setup(x => x.DeleteAuthor(authorId))
//                .Callback(() =>
//                {
//                    _authors.RemoveAt(expectedToDelete.Id);

//                })!.ReturnsAsync(() => _authors.FirstOrDefault(x => x.Id == authorId));

//            //inject
//            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _logger.Object);
//            var controller = new AuthorController(service, _mapper);

//            //act
//            var result = await controller.Delete(authorId);

//            //asert
//            var okObjectResult = result as OkObjectResult;
//            Assert.NotNull(okObjectResult);

//            var resultValue = okObjectResult.Value as Author;
//            Assert.NotNull(resultValue);
//            Assert.Equal(1, _authors.Count);
//        }
//        [Fact]
//        public async Task Author_Delete_Bad()
//        {
//            //setup
//            var authorId = 4;
//            var expectedToDelete = _authors.FirstOrDefault(x => x.Id == authorId);

//            _authorRepositoryMock.Setup(x => x.GetById(authorId))
//                .ReturnsAsync(_authors.FirstOrDefault(x => x.Id == authorId));

//            _authorRepositoryMock.Setup(x => x.DeleteAuthor(authorId))
//                .Callback(() =>
//                {
//                    _authors.RemoveAt(authorId - 1);
//                })!
//                .ReturnsAsync(() => _authors.FirstOrDefault(x => x.Id == authorId));

//            //inject
//            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _logger.Object);
//            var controller = new AuthorController(service, _mapper);

//            //act
//            var result = await controller.Delete(authorId);

//            //asert
//            var notFoundResult = result as NotFoundObjectResult;
//            Assert.NotNull(notFoundResult);

//            var resultValue = notFoundResult.Value as AddAuthorResponse;
//            Assert.Null(resultValue);
//        }

//    }
//}