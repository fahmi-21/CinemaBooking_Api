using Application;
using Application.Abstractions.Persistence;
using Api.MiddleWare;
using Infrastructure;
using Infrastructure.Persistence.Initialization;
using Scalar.AspNetCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider
        .GetRequiredService<IDbInitislizer>();

    await initializer.InitializeAsync();
}

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
            app.MapOpenApi();
            app.MapScalarApiReference();
            app.UseSwagger();
            app.UseSwaggerUI();
}

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    
