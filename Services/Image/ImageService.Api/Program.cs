using FluentAssertions.Common;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ImageConfiguration>(
    builder.Configuration.GetSection("ImageStorage"));

builder.Services.AddDbContext<ImageDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Image")));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<SaveImageValidator>();
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

//builder.Services.AddValidatorsFromAssembly(typeof(SaveImageValidator).Assembly);

builder.Services.AddCarter();

//builder.Services.AddMessageBroker(builder.Configuration);
builder.Services.AddMassTransit(config =>
{
    config.SetKebabCaseEndpointNameFormatter();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), host =>
        {
            host.Username(builder.Configuration["MessageBroker:UserName"]!);
            host.Password(builder.Configuration["MessageBroker:Password"]!);
        });

        cfg.ConfigureEndpoints(ctx);
    });
});


// Dependency Injection
builder.Services.AddScoped<IFileSystem, FileSystem>();
builder.Services.AddScoped<IQueryRepository<Image>, QueryRepository<Image, ImageDbContext>>();
builder.Services.AddScoped<ICommandRepository<Image>, CommandRepository<Image, ImageDbContext>>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:8030") // URL di Swagger
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ImageDbContext>();
    dbContext.Database.EnsureCreated();
}

// Abilita il middleware CORS
app.UseCors("AllowSpecificOrigin");

app.MapCarter();

app.UseHttpsRedirection();

app.Run();
