using ManagementService.Business;
using ManagementService.DataAccess.DatabaseContext;
using ManagementService.DataAccess.Repository;
using ManagementService.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//db config

builder.Services.Configure<MongoDBSettings>(
    options =>  
    {
        //options.ConnectionString = "mongodb://localhost:27017";
        //options.Database = "homeDB";
        options.ConnectionString = builder.Configuration["MongoDB:ConnectionString"].ToString();
        options.Database = builder.Configuration["MongoDB:Database"].ToString();

    });

// add cors 

builder.Services.AddCors(options =>
    options.AddPolicy("CorsPolicy",policy=>policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    ));

builder.Services.AddControllers();

builder.Services.AddTransient<IDatabaseContext, DatabaseContext>();

builder.Services.AddTransient<IUserBl, UserBl>();

builder.Services.AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}