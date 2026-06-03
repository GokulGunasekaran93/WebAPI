using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using ManagementService.Business;
using ManagementService.DataAccess.DatabaseContext;
using ManagementService.DataAccess.Repository;
using ManagementService.Model;
using ManagementService.Options;

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

builder.Services.AddApiVersioning(options =>
{
    // if client not set it will set below
    options.DefaultApiVersion = new ApiVersion(1, 0);
    // allows to omit the version when unspecified.. def will be used automaitcally
    options.AssumeDefaultVersionWhenUnspecified = true;
    // add info about support api in response headers
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    // make sure api version repl;aced in url routes
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();


// Swagger 
// UI for testing API'
// Generates API documentation
// user JSOn internally
// swagger UI reads the JSOn and shows endpoints visually

// openAPi
// is a std /specification
// swagger is tool implementing openapi


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();  -- this generates endpoints like /openapi/v1.json this is inbuilt
    // if we use swagger buckle then it l create issue .
    // this is ligjy weight api's
    
    // use swagger buckle
    // for api versioning, enterprise apps, custom swagger config , authentication support
    
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    
    // for versioning
    app.UseSwagger();
    //for normal swagger
    //app.UseSwaggerUI();
    // to enable swagger add swagger nuget 
    // use below line 
    app.UseSwaggerUI(options =>
    {
        // for versioning
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
        
        // for general swagger use   app.UseSwaggerUI();
        // location and  name
        // options.SwaggerEndpoint("/openapi/v1.json", "v1");  
    });
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