using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Alisto.Api.Data.AlistoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DB") ) .ConfigureWarnings(warnings =>
                    warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));
 string JwtIssuer=builder.Configuration["JwtSettings:Issuer"]??""; // Replace with your actual issuer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = JwtIssuer;//"https://settling-ant-32.clerk.accounts.dev"; // or https://api.clerk.dev if using Clerk-hosted
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
          //  ValidIssuer ="https://settling-ant-32.clerk.accounts.dev",// builder.Configuration["JwtSettings:Issuer"], // Set your issuer here
            ValidateAudience = false, // or set to true if you want to validate against a specific audience
            ValidateLifetime = true
        };
    });

builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowDevOrigin", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .WithExposedHeaders("Content-Disposition", "Content-Length", "Content-Type")
                           .SetIsOriginAllowed(origin => true); // Allow any origin for development
                });
            });
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowDevOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
