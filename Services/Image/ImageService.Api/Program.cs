using System.IO.Abstractions;
using ImageService.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ImageDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Image")));

builder.Services.AddScoped<IFileSystem, FileSystem>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
