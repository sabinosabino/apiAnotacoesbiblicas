using System.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using projetobibliiaapi.Context;
using projetobibliiaapi.Context.Interface;
using projetobibliiaapi.Models;
using projetobibliiaapi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnection, Db>();
builder.Services.AddSingleton<IRepositories<Temas>, TemaRepositories>();
builder.Services.AddSingleton<IRepositories<Topicos>, TopicosRepositories>();
builder.Services.AddSingleton<IRepositories<Anotacoes>, AnotacoesRepositories>();

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
