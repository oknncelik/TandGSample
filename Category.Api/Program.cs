using Category.Business.Abstruct;
using Category.Business.Concreate;
using Category.Common.Configs.Models;
using Category.Data;
using Category.Data.Abstruct.Repositories;
using Category.Data.Concreate.Repositories;
using Category.Entities.MapperProfiles;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "T&G Category Service",
        Version = "v1",
        Description = "T&G Category Service Provider",
        Contact = new OpenApiContact
        {
            Name = "Okan Çelik",
            Email = "okan.celik@outlook.com"
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

#region AppSettings
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var mongoDbSettings = new MongoDbSettings();
configuration.GetSection(nameof(MongoDbSettings)).Bind(mongoDbSettings);
builder.Services.AddSingleton(mongoDbSettings); //Mongo settings add to container...
#endregion

#region Dependencies
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
builder.Services.AddSingleton<ICategoryManager, CategoryManager>();
#endregion

builder.Services.AddAutoMapper(new[] { typeof(AutoMapperProfile) }); // Automapper config...
builder.Services.AddHostedService<Initialiazer>(); // Set Dummy Data...

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
