using AutoMapper;
using BookStore.Models.Models;
using BookStore.Models.Requests;

namespace BookStore.AutoMapper
{
    internal class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<AddAuthorRequest, Author>();
            CreateMap<AddBookRequest, Book>();
            CreateMap<UpdateAuthorRequest, Author>();
            CreateMap<AddPersonRequest, Person>();
            CreateMap<Author, Author>();
        }
    }
}
