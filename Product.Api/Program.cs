using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Product.Api.Modules;
using Product.Common.Configs.Models;
using Product.Data;
using Product.Data.Contexts;
using Product.Entities.MapperProfiles;
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
        Title = "T&G Product Service",
        Version = "v1",
        Description = "T&G Product Service Provider",
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
var msSqlSettings = new MsSqlSettings();
configuration.GetSection(nameof(MsSqlSettings)).Bind(msSqlSettings);
builder.Services.AddSingleton(msSqlSettings); //MsSql settings add to container...
#endregion

#region Dependencies Injects
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModule())); // load autofac dependencies....
#endregion

builder.Services.AddAutoMapper(new[] { typeof(AutoMapperProfile) }); // Automapper config...
builder.Services.AddDbContext<ProductContext>(options => options.UseSqlServer(msSqlSettings.ConnectionString, opt => opt.EnableRetryOnFailure()));

builder.Services.AddHostedService<Initialiazer>(); // Set Dummy Data...

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var context = app.Services.GetRequiredService<ProductContext>();
await context.Database.MigrateAsync(); // Migrations run

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
