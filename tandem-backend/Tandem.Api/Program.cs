using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Tandem.BusinessLogic;
using Tandem.Persistence;
using Tandem.Persistence.Entities;
using Tandem.Persistence.Seeder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddConsole();
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<Seeder>();

Console.WriteLine(builder.Configuration["ConnectionStrings:DefaultConnection"]);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddBusinessLogic();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 0;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<TandemContext>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "JwtBearer";
        options.DefaultChallengeScheme = "JwtBearer";
    })
    .AddJwtBearer("JwtBearer", options =>
    {
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
                // Local development server
                "http://localhost:5173",
                // Running inside docker compose
                "http://localhost")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

DbStartup.ProvisionSchema(app.Services.CreateScope().ServiceProvider);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    await SeedDatabase();
}

app.Run();
return;

async Task SeedDatabase()
{
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
    await seeder.Seed();
}
