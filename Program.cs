using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SportsOrderApp.Core;
using SportsOrderApp.Data;
using SportsOrderApp.Models;
using SportsOrderApp.Repositories;
using SportsOrderApp.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------
// 1. Configure Database Context
// ---------------------------------------
builder.Services.AddDbContext<JSDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseLazyLoadingProxies());

// ---------------------------------------
// 2. Register Services and Repositories
// ---------------------------------------
builder.Services.AddRepositoryServices();
builder.Services.AddServiceLayer();

// ---------------------------------------
// 3. Add AutoMapper
// ---------------------------------------
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ---------------------------------------
// 4. Configure JwtSettings
// ---------------------------------------
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

// Register JwtSettings instance
builder.Services.AddSingleton(jwtSettings);

// ---------------------------------------
// 5. Configure Authentication with JWT
// ---------------------------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(5) // No extra time for token expiration
        };
    });

// ---------------------------------------
// 6. Add Authorization
// ---------------------------------------
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OTPOnly", policy =>
        policy.RequireClaim("TokenType", "OTPVerification"));

    options.AddPolicy("FullAccess", policy =>
        policy.RequireClaim("TokenType", "FullAccess"));
});

// ---------------------------------------
// 7. Add Controllers
// ---------------------------------------
builder.Services.AddControllers();

// ---------------------------------------
// 8. Configure Swagger with JWT Bearer Support
// ---------------------------------------
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "AamirAli_Task API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header. Enter 'Bearer' followed by a space and the token.",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                In = Microsoft.OpenApi.Models.ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// ---------------------------------------
// 9. Build Application
// ---------------------------------------
var app = builder.Build();

// ---------------------------------------
// 10. Configure Middleware
// ---------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Add Authentication middleware
app.UseAuthorization();

app.MapControllers();

app.Run();
