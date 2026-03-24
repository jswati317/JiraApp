using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using TaskManager.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Add Services
// -----------------------------

// Controllers
builder.Services.AddControllers()
    .AddFluentValidation();

builder.Services.AddValidatorsFromAssemblyContaining<TaskItemValidator>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();
builder.Services.AddScoped<TaskService>();

// -----------------------------
// Build App
// -----------------------------
var app = builder.Build();

// -----------------------------
// Middleware
// -----------------------------

// Swagger (only in development)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Optional HTTPS redirect (can ignore warning for now)
app.UseHttpsRedirection();

// Authorization (keep for future auth)
app.UseAuthorization();

// Map Controllers
app.MapControllers();

// -----------------------------
// Run App
// -----------------------------
app.Run();