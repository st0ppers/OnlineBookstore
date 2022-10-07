using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using OnlineBookstore.DL.Interface;
using OnlineBookstore.DL.Repositories.InMemoryRepositories;
using OnlineBookstore.DL.Repositories.MsSQL;

namespace BookStore.Extensions
{
    //extension classes need to be static
    public static class ServiceExtensions
    {

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IPersonRepo, PersonRepo>();
            services.AddSingleton<IAuthorRepo, AuthorSqlRepository>();
            services.AddSingleton<IBookRepo, BookSqlRepository>();
            services.AddSingleton<IUserInfoRepository, UserInfoSqlRepository>();
            services.AddSingleton<IEmployeeRepository, EmployeeSqlRepository>();
            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection service)
        {
            service.AddSingleton<IUserInfoService, UserInfoService>();
            service.AddSingleton<IEmployeeService, EmployeeService>();
            service.AddSingleton<IPersonService, PersonService>();
            return service;
        }


    }
}
