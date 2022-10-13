using BookStore.BL.Interfaces;
using BookStore.BL.Kafka;
using BookStore.BL.Kafka.KafkaSettings;
using BookStore.BL.Services;
using BookStore.Models.Models;
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
            service.AddTransient<IIdentityService, IdentityService>();
            service.AddSingleton<ConsumerService<int, int>>();
            service.AddSingleton<ProducerServices<int, int>>();
            service.AddSingleton<KafkaSettings>();
            return service;
        }


    }
}
