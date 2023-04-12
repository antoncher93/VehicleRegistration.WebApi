using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi;
using VehicleRegistration.WebApi.Extensions;
using VehicleRegistration.WebApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build()
    .Get<ApplicationConfig>();

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddDbContext<ApplicationDbContext>(
    optionsAction: options => options.UseSqlServer(
        connectionString: config.SqlConnectionString));

builder.Services.AddRepositories();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();