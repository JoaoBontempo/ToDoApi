using Application.Usecases.ToDos;
using Domain.Gateway;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Dto.Response;
using Presentation.Mapping;
using Presentation.Middlewares;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

string connectionString =
    $"Server={Environment.GetEnvironmentVariable("DB_SERVER")};" +
    $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
    $"User Id={Environment.GetEnvironmentVariable("DB_USER")};" +
    $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
    $"TrustServerCertificate=True;";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
);


builder.Services.AddAutoMapper(cfg => { }, typeof(ToDoMappingProfile));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<ITodoGateway, ToDoRepository>();
builder.Services.AddScoped<ISaveToDoUsecase, SaveToDoUsecase>();
builder.Services.AddScoped<IFindToDoByIdUsecase, FindToDoByIdUsecase>();
builder.Services.AddScoped<IFindAllToDosUsecase, FindAllToDosUsecase>();
builder.Services.AddScoped<IDeleteToDoUsecase, DeleteToDoUsecase>();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(m => (m.Value?.Errors?.Count ?? 0) > 0)
                .ToDictionary(
                    m => m.Key,
                    m => m.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            var errorMessage = string.Join("; ", errors.SelectMany(e => e.Value!));

            var response = new AppDefaultResponse(errorMessage);

            return new BadRequestObjectResult(response)
            {
                ContentTypes = { "application/json" }
            };
        };
    });

var app = builder.Build();

app.UseMiddleware<AppExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowSpecificOrigins");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
