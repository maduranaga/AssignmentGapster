using CodeFirstApproach.Expception;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;
using TvMaze.Api.Extensions;
using TvMaze.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TvMazeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconn") 
        ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.")));

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddRateLimiting(builder.Configuration);
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    Assembly.GetExecutingAssembly(),
    Assembly.Load("TvMaze.Application")
));

builder.Host.UseSerilog();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TvMaze Api Assigmnet");
    c.RoutePrefix = string.Empty;
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseRateLimiter();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
