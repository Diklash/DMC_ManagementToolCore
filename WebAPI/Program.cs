using CloudinaryDotNet;
using DataAccessLibrary.Helpers;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });


//Transient services are created each time they are requested from the service, they are ideal for lightweight, stateless services.
//builder.Services.AddTransient<IEmployeeRepository>(f => new EmployeeRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddTransient<IEmployeeWorkLogRepository>(f => new EmployeeWorkLogRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//AddScoped services are created once per client request (per scope), this means a new instance is created for each HTTP request, they are  are useful for services that maintain state 
builder.Services.AddScoped<IEmployeeRepository>(f => new EmployeeRepository(connectionString));
builder.Services.AddScoped<IEmployeeWorkLogRepository>(f => new EmployeeWorkLogRepository(connectionString));
builder.Services.AddScoped<IProjectSiteRepository>(f => new ProjectSiteRepository(connectionString));
//builder.Services.AddScoped<IAuthenticationRepository>(f => new AuthenticationRepository(connectionString));
builder.Services.AddScoped<ISiteActivityRepository>(f => new SiteActivityRepository(connectionString));


//builder.Services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    //.AddUserStore<UserStore>()
//    .AddDefaultTokenProviders(); // Only if you need token providers for things like password reset.


// Bind the Cloudinary Account
var cloudinaryAccount = new Account();
builder.Configuration.GetSection("Cloudinary").Bind(cloudinaryAccount);
builder.Services.AddTransient<IImageRepository>(f => new ImageRepository(cloudinaryAccount)); //stateless


//****** Configure Services to Use CORS *******
//CORS = Cross-Origin Resource Sharing
//If the front-end application is running on a different domain (or port) than your back-end, you might run into CORS issues
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("https://localhost:7018"// Replace with the actual port of your API
                   ) // You can add multiple origins here
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthentication(); //i add this after adding AuthenticationRepository
app.UseAuthorization();

app.MapControllers();

// And in the Configure method
app.UseCors("MyAllowSpecificOrigins");

app.Run();
