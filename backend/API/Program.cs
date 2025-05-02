using API.Extensions;
using API.Profiles;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using MongoDB.Driver;
using Serilog;
using Serilog.Filters;

var builder = WebApplication.CreateBuilder(args);

var appName = builder.Configuration["SystemName:AppName"] ?? "WorkersApp";

builder.Logging.ClearProviders();

builder.Logging.AddFilter("Microsoft", LogLevel.Warning);
builder.Logging.AddFilter("System", LogLevel.Warning);

// Serilog Configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.File($"logs/{appName}-.log", rollingInterval: RollingInterval.Day)
    .Filter.ByExcluding(Matching.FromSource("Microsoft.EntityFrameworkCore"))
    .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore"))
    .CreateLogger();

builder.Logging.AddSerilog();

// Force automatically generated paths (as with [controller]) to be lowercase
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add drivers to handle requests
builder.Services.AddControllers();

// Add authorization
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.ConfigureCors(builder.Configuration);
builder.Services.AddAplicacionServices();

builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new MongoClient(configuration.GetValue<string>("ProductDBConnection"));
});

builder.Services.AddScoped<IUnitOfWork>(sp =>
{
    var mongoClient = sp.GetRequiredService<IMongoClient>();
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new UnitOfWork(mongoClient, configuration);
});

builder.Services.AddScoped<IProductRepository>(sp =>
{
    var mongoClient = sp.GetRequiredService<IMongoClient>();
    var configuration = sp.GetRequiredService<IConfiguration>();
    var databaseName = configuration.GetValue<string>("ProductDBName");
    var database = mongoClient.GetDatabase(databaseName);
    var collection = database.GetCollection<Product>("products");
    return new Infrastructure.Repositories.ProductRepository(collection);
});

builder.Services.AddScoped(sp =>
{
    var mongoClient = sp.GetRequiredService<IMongoClient>();
    var configuration = sp.GetRequiredService<IConfiguration>();
    var databaseName = configuration.GetValue<string>("ProductDBName");
    var database = mongoClient.GetDatabase(databaseName);
    var collection = database.GetCollection<Category>("categories");
    return (Core.Interfaces.ICategoryRepository)new CategoryRepository(collection);
});

builder.Services.AddScoped<IUserRepository>(sp =>
{
    var mongoClient = sp.GetRequiredService<IMongoClient>();
    var configuration = sp.GetRequiredService<IConfiguration>();
    var databaseName = configuration.GetValue<string>("ProductDBName");
    var database = mongoClient.GetDatabase(databaseName);
    var collection = database.GetCollection<User>("users");
    return new UserRepository(collection);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
