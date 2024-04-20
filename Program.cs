using System.Text;
using JwtExample.Abstractions;
using JwtExample.Settings;
using JwtExample.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>()!;
        var jwtAudience = builder.Configuration.GetSection("Jwt:Audience").Get<string>()!;
        var jwtSecretKey = builder.Configuration.GetSection("Jwt:SecretKey").Get<string>()!;

        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddScoped<IJwtProvider, JwtProvider>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
