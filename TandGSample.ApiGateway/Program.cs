using MMLib.Ocelot.Provider.AppConfiguration;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddOcelotWithSwaggerSupport(options =>
    {
        options.Folder = "OcelotConfiguration";
    })
    .AddJsonFile("ocelot.json")
    .AddJsonFile("appsettings.json", true, true);
});

// Add services to the container.

builder.Services.AddOcelot(builder.Configuration).AddAppConfiguration();
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddControllers();

// app
var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";

});
app.UseOcelot().Wait();
app.Run();
