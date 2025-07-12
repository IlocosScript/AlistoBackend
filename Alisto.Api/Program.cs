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
// JWT Authentication Configuration
var jwtIssuer = builder.Configuration["JwtSettings:Issuer"];
if (!string.IsNullOrEmpty(jwtIssuer))
{
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = jwtIssuer;
            options.RequireHttpsMetadata = false; // Allow HTTP for development
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true
            };
        });
}
else
{
    // If no JWT issuer is configured, add a basic authentication scheme
    builder.Services.AddAuthentication();
}

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

// Register Local File Upload service
builder.Services.AddScoped<Alisto.Api.Services.ILocalFileUploadService, Alisto.Api.Services.LocalFileUploadService>();

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

// Serve static files from wwwroot
app.UseStaticFiles();

// Serve static files from uploads folder
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
    RequestPath = "/uploads"
});

app.UseAuthorization();

app.MapControllers();

app.Run();
