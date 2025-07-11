# API Response Rules - Alisto Backend

## Overview
This document defines the mandatory rules for ensuring all API endpoints in the Alisto backend follow the standardized `ApiResponse<T>` response format.

## 1. Response Format Standard

### 1.1 Mandatory Response Structure
Every API endpoint MUST return responses wrapped in the `ApiResponse<T>` class:

```csharp
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? RequestId { get; set; }
}
```

### 1.2 Response Patterns

#### Success Responses
```csharp
// GET endpoints
return Ok(new ApiResponse<T>
{
    Success = true,
    Data = data,
    Message = "Operation completed successfully"
});

// POST endpoints
return CreatedAtAction(nameof(GetEntity), new { id = entity.Id }, new ApiResponse<T>
{
    Success = true,
    Data = entityDto,
    Message = "Entity created successfully"
});
```

#### Error Responses
```csharp
// 400 Bad Request
return BadRequest(new ApiResponse<T>
{
    Success = false,
    Message = "Invalid request data",
    Errors = new List<string> { "Validation error details" }
});

// 404 Not Found
return NotFound(new ApiResponse<T>
{
    Success = false,
    Message = "Entity not found"
});

// 500 Internal Server Error
return StatusCode(500, new ApiResponse<T>
{
    Success = false,
    Message = "An error occurred while processing the request",
    Errors = new List<string> { ex.Message }
});
```

## 2. Controller Method Rules

### 2.1 Return Type Requirements
- All controller methods MUST return `IActionResult`
- NO direct returns of DTOs, models, or primitive types
- NO `ActionResult<T>` usage (use `IActionResult` with `ApiResponse<T>`)

### 2.2 Forbidden Patterns
```csharp
// ❌ FORBIDDEN - Direct return of data
return Ok(userDto);
return Ok(users);
return userDto;

// ❌ FORBIDDEN - ActionResult<T>
public ActionResult<UserDto> GetUser(string id)

// ✅ REQUIRED - Wrapped in ApiResponse
return Ok(new ApiResponse<UserDto>
{
    Success = true,
    Data = userDto,
    Message = "User retrieved successfully"
});
```

### 2.3 Required Patterns by HTTP Method

#### GET Endpoints
```csharp
[HttpGet]
public async Task<IActionResult> GetEntities([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
{
    try
    {
        // ... business logic ...
        
        var response = new ApiResponse<List<EntityDto>>
        {
            Success = true,
            Data = entities,
            Message = $"Retrieved {entities.Count} entities successfully"
        };

        // Add pagination headers
        Response.Headers["X-Total-Count"] = totalCount.ToString();
        Response.Headers["X-Total-Pages"] = totalPages.ToString();
        Response.Headers["X-Current-Page"] = page.ToString();

        return Ok(response);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error retrieving entities");
        return StatusCode(500, new ApiResponse<List<EntityDto>>
        {
            Success = false,
            Message = "An error occurred while retrieving entities",
            Errors = new List<string> { ex.Message }
        });
    }
}
```

#### POST Endpoints
```csharp
[HttpPost]
public async Task<IActionResult> CreateEntity([FromBody] CreateEntityRequest request)
{
    try
    {
        // ... business logic ...
        
        return CreatedAtAction(nameof(GetEntity), new { id = entity.Id }, new ApiResponse<EntityDto>
        {
            Success = true,
            Data = entityDto,
            Message = "Entity created successfully"
        });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error creating entity");
        return StatusCode(500, new ApiResponse<EntityDto>
        {
            Success = false,
            Message = "An error occurred while creating the entity",
            Errors = new List<string> { ex.Message }
        });
    }
}
```

#### PUT Endpoints
```csharp
[HttpPut("{id}")]
public async Task<IActionResult> UpdateEntity(string id, [FromBody] UpdateEntityRequest request)
{
    try
    {
        // ... business logic ...
        
        return Ok(new ApiResponse<EntityDto>
        {
            Success = true,
            Data = entityDto,
            Message = "Entity updated successfully"
        });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error updating entity with ID: {EntityId}", id);
        return StatusCode(500, new ApiResponse<EntityDto>
        {
            Success = false,
            Message = "An error occurred while updating the entity",
            Errors = new List<string> { ex.Message }
        });
    }
}
```

#### DELETE Endpoints
```csharp
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteEntity(string id)
{
    try
    {
        // ... business logic ...
        
        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Entity deleted successfully"
        });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error deleting entity with ID: {EntityId}", id);
        return StatusCode(500, new ApiResponse<object>
        {
            Success = false,
            Message = "An error occurred while deleting the entity",
            Errors = new List<string> { ex.Message }
        });
    }
}
```

#### PATCH Endpoints (Status Updates)
```csharp
[HttpPatch("{id}/status")]
public async Task<IActionResult> UpdateEntityStatus(string id, [FromBody] UpdateStatusRequest request)
{
    try
    {
        // ... business logic ...
        
        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Entity status updated successfully"
        });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error updating entity status with ID: {EntityId}", id);
        return StatusCode(500, new ApiResponse<object>
        {
            Success = false,
            Message = "An error occurred while updating the entity status",
            Errors = new List<string> { ex.Message }
        });
    }
}
```

## 3. Error Handling Rules

### 3.1 Exception Handling Pattern
Every controller method MUST include try-catch blocks:

```csharp
try
{
    // Business logic here
}
catch (Exception ex)
{
    _logger.LogError(ex, "Error description with context");
    return StatusCode(500, new ApiResponse<T>
    {
        Success = false,
        Message = "User-friendly error message",
        Errors = new List<string> { ex.Message }
    });
}
```

### 3.2 Validation Error Handling
```csharp
if (entity == null)
{
    return NotFound(new ApiResponse<EntityDto>
    {
        Success = false,
        Message = "Entity not found"
    });
}

if (!ModelState.IsValid)
{
    var errors = ModelState.Values
        .SelectMany(v => v.Errors)
        .Select(e => e.ErrorMessage)
        .ToList();
        
    return BadRequest(new ApiResponse<EntityDto>
    {
        Success = false,
        Message = "Validation failed",
        Errors = errors
    });
}
```

## 4. Pagination Rules

### 4.1 Pagination Headers
List endpoints MUST include pagination headers:

```csharp
Response.Headers["X-Total-Count"] = totalCount.ToString();
Response.Headers["X-Total-Pages"] = totalPages.ToString();
Response.Headers["X-Current-Page"] = page.ToString();
```

### 4.2 Pagination Query Parameters
```csharp
[HttpGet]
public async Task<IActionResult> GetEntities(
    [FromQuery] int page = 1, 
    [FromQuery] int pageSize = 10,
    [FromQuery] string? filter = null)
```

## 5. Response Type Guidelines

### 5.1 Data Types by Endpoint Type
- **List endpoints**: `ApiResponse<List<EntityDto>>`
- **Single entity endpoints**: `ApiResponse<EntityDto>`
- **Create endpoints**: `ApiResponse<EntityDto>` (return created entity)
- **Update endpoints**: `ApiResponse<EntityDto>` (return updated entity)
- **Delete endpoints**: `ApiResponse<object>` (no data, just message)
- **Status update endpoints**: `ApiResponse<object>` (no data, just message)
- **Enum/status list endpoints**: `ApiResponse<List<string>>`

### 5.2 Message Guidelines
- **Success messages**: Clear, action-oriented ("Entity created successfully")
- **Error messages**: User-friendly, non-technical
- **Include counts**: For list operations ("Retrieved 25 entities successfully")
- **Include context**: For specific operations ("User with ID '123' not found")

## 6. Code Review Checklist

### 6.1 Pre-Review Checklist
- [ ] All controller methods return `IActionResult`
- [ ] All responses are wrapped in `ApiResponse<T>`
- [ ] Success responses have `Success = true`
- [ ] Error responses have `Success = false`
- [ ] All methods have try-catch blocks
- [ ] Error responses include appropriate HTTP status codes
- [ ] List endpoints include pagination headers
- [ ] Messages are clear and user-friendly
- [ ] No direct returns of DTOs or models

### 6.2 Automated Checks
```bash
# Search for forbidden patterns
grep -r "return Ok([^A]" Controllers/
grep -r "return NotFound([^A]" Controllers/
grep -r "return BadRequest([^A]" Controllers/
grep -r "ActionResult<" Controllers/
```

## 7. Testing Requirements

### 7.1 Unit Test Patterns
```csharp
[Test]
public async Task GetEntity_ShouldReturnApiResponse()
{
    // Arrange
    var controller = new EntityController(mockContext, mockLogger);
    
    // Act
    var result = await controller.GetEntity("123");
    
    // Assert
    var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
    var apiResponse = okResult.Value.Should().BeOfType<ApiResponse<EntityDto>>().Subject;
    apiResponse.Success.Should().BeTrue();
    apiResponse.Data.Should().NotBeNull();
    apiResponse.Message.Should().NotBeNullOrEmpty();
}
```

### 7.2 Integration Test Patterns
```csharp
[Test]
public async Task GetEntity_ShouldReturnCorrectResponseFormat()
{
    // Act
    var response = await client.GetAsync("/api/entity/123");
    
    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var content = await response.Content.ReadAsStringAsync();
    var apiResponse = JsonSerializer.Deserialize<ApiResponse<EntityDto>>(content);
    apiResponse.Success.Should().BeTrue();
    apiResponse.Data.Should().NotBeNull();
    apiResponse.Timestamp.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
}
```

## 8. Documentation Requirements

### 8.1 API Documentation
All endpoints MUST be documented with:
- Expected request format
- Expected response format (always `ApiResponse<T>`)
- Possible error responses
- Example requests and responses

### 8.2 Example Response Documentation
```json
{
  "success": true,
  "data": {
    "id": "123",
    "name": "Example Entity",
    "createdAt": "2024-01-01T00:00:00Z"
  },
  "message": "Entity retrieved successfully",
  "errors": null,
  "timestamp": "2024-01-01T00:00:00Z",
  "requestId": null
}
```

## 9. Enforcement

### 9.1 Code Review Process
- All pull requests MUST be reviewed for compliance
- Use the checklist in section 6.1
- Automated checks should be run before merge

### 9.2 CI/CD Integration
- Add automated checks to build pipeline
- Fail builds that violate these rules
- Generate reports of compliance

### 9.3 Training
- All developers MUST read and understand these rules
- Regular reviews and updates of these standards
- Code examples and templates provided

## 10. Exceptions

### 10.1 Health Check Endpoints
Health check endpoints may return simple responses:
```csharp
[HttpGet("health")]
public IActionResult Health()
{
    return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
}
```

### 10.2 File Download Endpoints
File download endpoints may return `FileResult`:
```csharp
[HttpGet("download/{id}")]
public async Task<IActionResult> DownloadFile(string id)
{
    // Return FileResult for actual file downloads
    return File(fileBytes, "application/pdf", "document.pdf");
}
```

---

**Last Updated:** January 2024
**Version:** 1.0
**Maintainer:** Development Team 