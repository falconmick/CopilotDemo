using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", Results<Ok<WeatherForecast[]>, BadRequest> (
        [FromQuery(Name = "hours")]int hours, [FromHeader(Name = "X-REGION")]string region) =>
    {
        if (hours < 0 || hours > 24) { return TypedResults.BadRequest(); }
        
        WeatherForecast[] forecast = GenerateWeatherForecasts(summaries);
        return TypedResults.Ok(forecast);
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.MapGet("/getpokemon", Results<Ok<string[]>, NotFound> (
        [FromQuery(Name = "generation")]int generation) =>
    {
        if (generation < 0 || generation > 4) { return TypedResults.NotFound(); }
        
        return TypedResults.Ok(new string[] { "Pikachu", "Charmander", "Squirtle", "Bulbasaur" });
    })
    .WithName("GetPokemon")
    .WithOpenApi();

app.Run();

WeatherForecast[] GenerateWeatherForecasts(string[] strings)
{
    return Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                strings[Random.Shared.Next(strings.Length)]
            ))
        .ToArray();
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public partial class Program { }