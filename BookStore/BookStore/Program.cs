using BookStore.BL.Interfaces;
using OnlineBookstore.DL.Interface;
using OnlineBookstore.DL.Repositories.InMemoryRepositories;
using BookStore.BL.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// addSingleton make instance of the object one time and it used everywhere
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IAuthorRepo, AuthorRepo>();
builder.Services.AddSingleton<IPersonRepo, PersonRepo>();
builder.Services.AddSingleton<IPersonService, PersonService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
