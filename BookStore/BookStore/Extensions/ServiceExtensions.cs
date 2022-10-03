﻿using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using OnlineBookstore.DL.Interface;
using OnlineBookstore.DL.Repositories.InMemoryRepositories;

namespace BookStore.Extensions
{
    //extension classes need to be static
    public static class ServiceExtensions
    {

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IPersonRepo, PersonRepo>();
            services.AddSingleton<IAuthorRepo, AuthorRepo>();
            services.AddSingleton<IBookRepo, BookRepo>();
            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection service)
        {
            service.AddSingleton<IAuthorService, AuthorServices>();
            service.AddSingleton<IPersonService, PersonService>();
            service.AddSingleton<IBookService, BookService>();
            return service;
        }


    }
}