# File Upload System Guide

## Overview

The Alisto API includes a robust file upload system that works seamlessly across Windows and Linux platforms. The system automatically creates the necessary folder structure and handles file sanitization for cross-platform compatibility.

## Features

### ✅ Cross-Platform Compatibility
- Works on Windows and Linux
- Automatic path sanitization
- Case-insensitive folder naming
- Handles platform-specific invalid characters

### ✅ Automatic Folder Creation
- Creates uploads folder on application startup
- Creates default subfolders (news, users, reports, general)
- Works even without configuration settings

### ✅ Security Features
- File type validation (images only)
- File size limits
- Path traversal protection
- Unique file naming to prevent conflicts

### ✅ Default Behavior
- No configuration required for basic functionality
- Sensible defaults for all settings
- Graceful fallbacks for missing settings

## Configuration

### Basic Configuration (appsettings.json)

```json
{
  "FileUpload": {
    "BasePath": "uploads",
    "BaseUrl": "/uploads",
    "LocalPath": "uploads",
    "MaxFileSize": 10485760,
    "AllowedExtensions": [".jpg", ".jpeg", ".png", ".gif", ".webp"]
  }
}
```

### Default Values (when no config provided)

| Setting | Default Value | Description |
|---------|---------------|-------------|
| BasePath | "uploads" | Relative path to uploads folder |
| BaseUrl | "/uploads" | URL path for accessing files |
| LocalPath | "uploads" | Local storage path |
| MaxFileSize | 10485760 | Maximum file size in bytes (10MB) |
| AllowedExtensions | [".jpg", ".jpeg", ".png", ".gif", ".webp"] | Allowed image formats |

## Folder Structure

The system automatically creates this structure:

```
uploads/
├── news/          # News article images
├── users/         # User profile images
├── reports/       # Issue report images
├── general/       # General uploads (default fallback)
└── README.md      # Documentation
```

## Usage Examples

### Upload a News Image

```csharp
// In your controller
[HttpPost("news")]
public async Task<IActionResult> CreateNews([FromForm] CreateNewsRequest request)
{
    var imageUrl = await _fileUploadService.UploadFileAsync(request.Image, "news");
    // imageUrl will be: "/uploads/news/20241201_143022_guid_filename.jpg"
}
```

### Upload a User Profile Image

```csharp
var profileImageUrl = await _fileUploadService.UploadFileAsync(userImage, "users");
```

### Upload to General Folder (default)

```csharp
var imageUrl = await _fileUploadService.UploadFileAsync(file);
// Uses "general" folder by default
```

## File Naming

### Generated File Names

Files are automatically named using this format:
```
{yyyyMMdd_HHmmss}_{GUID}_{sanitized_original_filename}
```

Example: `20241201_143022_a1b2c3d4-e5f6-7890-abcd-ef1234567890_my_image.jpg`

### Sanitization Rules

The system sanitizes both file names and folder names:

1. **Invalid Characters**: Replaced with underscores
2. **Path Separators**: Converted to underscores
3. **Case**: Folder names converted to lowercase
4. **Empty Names**: Default to "general" for folders, "file" for files

### Cross-Platform Considerations

| Platform | Path Separator | Invalid Characters | Case Sensitivity |
|----------|----------------|-------------------|------------------|
| Windows | `\` | `< > : " | ? *` | Case-insensitive |
| Linux | `/` | `/` | Case-sensitive |

The system handles all these differences automatically.

## Security

### File Validation
- **Type**: Only image files allowed
- **Size**: Configurable maximum (default 10MB)
- **Extensions**: Whitelist approach

### Path Security
- **Traversal Protection**: Prevents `../` attacks
- **Sanitization**: Removes dangerous characters
- **Validation**: Ensures paths stay within uploads directory

### Access Control
- **Public Access**: Files are served as static content
- **No Authentication**: Uploaded files are publicly accessible
- **URL Structure**: Predictable but secure naming

## Docker Deployment

### Volume Mounting

For Docker deployments, mount the uploads folder as a volume:

```yaml
# docker-compose.yml
services:
  alisto-api:
    volumes:
      - ./uploads:/app/uploads
```

### Environment Variables

```bash
# .env file
FILE_UPLOAD_LOCAL_PATH=uploads
FILE_UPLOAD_BASE_URL=/uploads
FILE_UPLOAD_MAX_FILE_SIZE=10485760
```

## Troubleshooting

### Common Issues

1. **Permission Denied**
   - Ensure the application has write permissions to the uploads folder
   - On Linux: `chmod 755 uploads/`

2. **Files Not Accessible**
   - Check that static file serving is configured in Program.cs
   - Verify the BaseUrl matches the static file configuration

3. **Cross-Platform Path Issues**
   - The system handles this automatically
   - Use `Path.Combine()` for path construction

### Debugging

Enable detailed logging in appsettings.json:

```json
{
  "Logging": {
    "LogLevel": {
      "Alisto.Api.Services.LocalFileUploadService": "Debug"
    }
  }
}
```

## Best Practices

1. **Folder Organization**: Use specific folders for different content types
2. **File Naming**: Let the system handle file naming automatically
3. **Configuration**: Use environment variables for different environments
4. **Backup**: Include uploads folder in backup strategies
5. **Monitoring**: Monitor disk space usage

## Migration from wwwroot

If migrating from a wwwroot-based system:

1. Update configuration to use "uploads" instead of "wwwroot/uploads"
2. Move existing files to the new structure
3. Update any hardcoded paths in your application
4. Test file access URLs

The system will automatically create the new structure on startup. 