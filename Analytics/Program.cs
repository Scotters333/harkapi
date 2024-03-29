using Analytics;
using Analytics.Commands;
using Analytics.Repositories;
using Analytics.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddDbContext<DataContext>(opt => 
    opt.UseInMemoryDatabase("Data"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

// Register services
builder.Services.AddScoped<IFileService, FileService>();

// Register repositories
builder.Services.AddScoped<IEnergyRepository, EnergyRepository>();
builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();

// Register commands
builder.Services.AddScoped<IImportEnergyCommand, ImportEnergyCommand>();
builder.Services.AddScoped<IImportWeatherCommand, ImportWeatherCommand>();
builder.Services.AddScoped<IUpdateEnergyAnomoliesCommand, UpdateEnergyAnomoliesCommand>();

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

app.UseRouting();

app.UseCors(options =>
    options.WithOrigins("*")
        .AllowAnyHeader()
        .AllowAnyMethod());

app.Run();
