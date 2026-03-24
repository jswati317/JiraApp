using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Azure Details to be filed
// builder.Services.AddSingleton<ITaskRepository>(
//     new AzureTaskRepository(builder.Configuration["AzureStorage"]));

// InLocal Memory 
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();

builder.Services.AddScoped<TaskService>();

var app = builder.Build();

app.MapControllers();

app.Run();