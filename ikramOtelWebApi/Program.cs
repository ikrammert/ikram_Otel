using DAL_CQRS.EventConsumers;
using DAL_CQRS.Handlers.CommandHandlers;
using DAL_CQRS.Handlers.QueryHandlers;
using DAL_CQRS.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetOtelByIDQueryHandler).GetTypeInfo().Assembly));

builder.Services.AddDbContext<CommandDbContext>(optionsAction: options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MariaDb");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var connectionString = builder.Configuration.GetConnectionString("MongoDb");
builder.Services.AddSingleton<QueryDbContext>(sp => new QueryDbContext(connectionString, "Playgorund"));
builder.Services.AddSingleton<EventStore>(sp => new EventStore(connectionString, "Playgorund"));

builder.Services.AddHostedService<EventConsumerService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
