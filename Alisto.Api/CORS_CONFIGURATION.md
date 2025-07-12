# CORS Configuration

## Overview

The Alisto API has been configured with a permissive CORS policy that allows access from any origin, making it suitable for development and production environments where cross-origin requests are needed.

## Current CORS Policy

### Policy Name: `AllowAll`

The CORS policy is configured to allow:

- **Any Origin**: All origins are allowed (`AllowAnyOrigin()`)
- **Any Method**: All HTTP methods are allowed (`AllowAnyMethod()`)
- **Any Header**: All headers are allowed (`AllowAnyHeader()`)
- **Custom Exposed Headers**: Specific headers are exposed to the client
- **Origin Validation**: Any origin is allowed (`SetIsOriginAllowed(origin => true)`)

### Configuration in Program.cs

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithExposedHeaders("Content-Disposition", "Content-Length", "Content-Type", "X-Total-Count", "X-Total-Pages", "X-Current-Page")
               .SetIsOriginAllowed(origin => true); // Allow any origin
    });
});
```

### Middleware Usage

```csharp
app.UseCors("AllowAll");
```

## Exposed Headers

The following headers are exposed to the client:

- `Content-Disposition` - For file downloads
- `Content-Length` - For content length information
- `Content-Type` - For content type information
- `X-Total-Count` - For pagination (total count)
- `X-Total-Pages` - For pagination (total pages)
- `X-Current-Page` - For pagination (current page)

## Security Considerations

### ⚠️ **Important Security Notes**

This CORS configuration is **very permissive** and should be used with caution:

1. **Development Only**: This configuration is suitable for development environments
2. **Production Considerations**: For production, consider restricting origins to specific domains
3. **Authentication**: Ensure proper authentication and authorization are in place
4. **Rate Limiting**: Implement rate limiting to prevent abuse

### Recommended Production Configuration

For production environments, consider using a more restrictive CORS policy:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("ProductionPolicy", builder =>
    {
        builder.WithOrigins("https://yourdomain.com", "https://www.yourdomain.com")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithExposedHeaders("Content-Disposition", "Content-Length", "Content-Type", "X-Total-Count", "X-Total-Pages", "X-Current-Page")
               .AllowCredentials(); // Only if you need to send cookies/credentials
    });
});
```

## Testing CORS

### Browser Console Test

You can test CORS functionality in the browser console:

```javascript
// Test GET request
fetch('http://localhost:7000/api/news')
  .then(response => response.json())
  .then(data => console.log(data))
  .catch(error => console.error('Error:', error));

// Test POST request
fetch('http://localhost:7000/api/news', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
  },
  body: JSON.stringify({
    title: 'Test News',
    description: 'Test Description',
    fullContent: 'Test Content',
    location: 'Test Location',
    category: 'Events',
    author: 'Test Author',
    isFeatured: false,
    isTrending: false
  })
})
.then(response => response.json())
.then(data => console.log(data))
.catch(error => console.error('Error:', error));
```

### cURL Test

```bash
# Test with custom origin
curl -H "Origin: http://localhost:3000" \
     -H "Access-Control-Request-Method: POST" \
     -H "Access-Control-Request-Headers: Content-Type" \
     -X OPTIONS \
     http://localhost:7000/api/news
```

## Environment-Specific Configuration

### Development Environment

The current configuration is ideal for development:

```csharp
// Development - Allow everything
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithExposedHeaders("Content-Disposition", "Content-Length", "Content-Type", "X-Total-Count", "X-Total-Pages", "X-Current-Page");
    });
});
```

### Production Environment

For production, use environment-specific configuration:

```csharp
// Production - Restrict origins
if (app.Environment.IsProduction())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("ProductionPolicy", builder =>
        {
            builder.WithOrigins("https://yourdomain.com")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .WithExposedHeaders("Content-Disposition", "Content-Length", "Content-Type", "X-Total-Count", "X-Total-Pages", "X-Current-Page");
        });
    });
}
else
{
    // Development - Allow everything
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .WithExposedHeaders("Content-Disposition", "Content-Length", "Content-Type", "X-Total-Count", "X-Total-Pages", "X-Current-Page");
        });
    });
}
```

## Troubleshooting

### Common CORS Issues

1. **Preflight Requests**: Ensure OPTIONS requests are handled properly
2. **Credentials**: If using cookies/authentication, add `.AllowCredentials()`
3. **Headers**: Make sure all required headers are allowed
4. **Methods**: Ensure all required HTTP methods are allowed

### Debugging

Enable CORS debugging in development:

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAll");
    // Add CORS debugging middleware if needed
}
```

## Summary

The current CORS configuration allows maximum flexibility for development and testing:

- ✅ **All Origins Allowed**: Any domain can access the API
- ✅ **All Methods Allowed**: GET, POST, PUT, DELETE, etc.
- ✅ **All Headers Allowed**: Custom headers are supported
- ✅ **Pagination Headers Exposed**: X-Total-Count, X-Total-Pages, X-Current-Page
- ✅ **File Upload Support**: Content-Disposition header for downloads

This configuration is perfect for development, testing, and scenarios where you need maximum cross-origin flexibility. 