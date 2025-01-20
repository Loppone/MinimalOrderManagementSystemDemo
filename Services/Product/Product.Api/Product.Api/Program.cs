using ProductService.Api.Messaging.ImageSaved;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Product"),
        opt => opt.EnableRetryOnFailure())
    .EnableDetailedErrors()
    .EnableSensitiveDataLogging());

builder.Services.AddCarter();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<CreateProductValidation>();
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(CreateProductValidation).Assembly);

builder.Services.AddMassTransit(bus =>
{
    bus.SetKebabCaseEndpointNameFormatter();

    bus.AddConsumer<ImageSavedConsumer>();

    bus.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), host =>
        {
            host.Username(builder.Configuration["MessageBroker:UserName"]!);
            host.Password(builder.Configuration["MessageBroker:Password"]!);
        });

        cfg.ConfigureEndpoints(ctx);
    });
});

// Exception Handling
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Dependency Injection
builder.Services.AddScoped<IQueryRepository<Product>, QueryRepository<Product, ProductDbContext>>();
builder.Services.AddScoped<ICommandRepository<Product>, CommandRepository<Product, ProductDbContext>>();
builder.Services.AddScoped<IQueryRepository<Category>, QueryRepository<Category, ProductDbContext>>();
builder.Services.AddScoped<ICommandRepository<Category>, CommandRepository<Category, ProductDbContext>>();

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
    await app.Seed();
}

// Abilita il middleware CORS
app.UseCors("AllowSpecificOrigin");


app.UseHttpsRedirection();

app.UseExceptionHandler(opt => { });

app.MapCarter();

app.Run();