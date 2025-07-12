# News Update with Image Upload Guide

## Overview

The Alisto API now supports updating news articles with optional image upload functionality. This includes both regular updates (JSON) and updates with image upload (multipart form data).

## New Endpoints

### 1. Regular Update (JSON)
**PUT** `/api/news/{id}`

Updates a news article using JSON data. The image URL must be provided as a string.

**Request Body:**
```json
{
  "title": "Updated News Title",
  "summary": "Updated summary",
  "fullContent": "Updated full content",
  "imageUrl": "/uploads/news/existing_image.jpg",
  "publishedDate": "2024-12-01T00:00:00Z",
  "publishedTime": "14:30",
  "location": "Updated Location",
  "expectedAttendees": "100-200 people",
  "category": "Events",
  "author": "Updated Author",
  "tags": ["tag1", "tag2"],
  "isFeatured": true,
  "isTrending": false
}
```

### 2. Update with Image Upload (Multipart Form)
**PUT** `/api/news/{id}/with-image`

Updates a news article with optional image upload. If an image is provided, it will replace the existing image.

**Request Body:** `multipart/form-data`
- `title` (string, required)
- `summary` (string, optional)
- `fullContent` (string, required)
- `publishedDate` (datetime, optional)
- `publishedTime` (string, optional)
- `location` (string, required)
- `expectedAttendees` (string, optional)
- `category` (NewsCategory enum, required)
- `author` (string, required)
- `tags` (array of strings, optional)
- `isFeatured` (boolean, required)
- `isTrending` (boolean, required)
- `image` (file, optional) - New image file

## New DTOs

### UpdateNewsRequest
```csharp
public class UpdateNewsRequest
{
    [Required]
    public string Title { get; set; }

    public string? Summary { get; set; }

    [Required]
    public string FullContent { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? PublishedDate { get; set; }

    public string? PublishedTime { get; set; }

    [Required]
    public string Location { get; set; }

    public string? ExpectedAttendees { get; set; }

    [Required]
    public NewsCategory Category { get; set; }

    [Required]
    public string Author { get; set; }

    public List<string> Tags { get; set; } = new List<string>();

    [Required]
    public bool IsFeatured { get; set; }

    [Required]
    public bool IsTrending { get; set; }
}
```

### UpdateNewsWithImageRequest
```csharp
public class UpdateNewsWithImageRequest
{
    [Required]
    public string Title { get; set; }

    public string? Summary { get; set; }

    [Required]
    public string FullContent { get; set; }

    public DateTime? PublishedDate { get; set; }

    public string? PublishedTime { get; set; }

    [Required]
    public string Location { get; set; }

    public string? ExpectedAttendees { get; set; }

    [Required]
    public NewsCategory Category { get; set; }

    [Required]
    public string Author { get; set; }

    public List<string> Tags { get; set; } = new List<string>();

    [Required]
    public bool IsFeatured { get; set; }

    [Required]
    public bool IsTrending { get; set; }

    // Optional image upload - if provided, it will replace the existing image
    public IFormFile? Image { get; set; }
}
```

## Features

### ✅ Image Upload Support
- **Optional Upload**: Image upload is optional - existing image is preserved if no new image is provided
- **Automatic Replacement**: If a new image is uploaded, the old image is automatically deleted
- **Error Handling**: Update continues even if image upload fails
- **File Validation**: Only image files are accepted (.jpg, .jpeg, .png, .gif, .webp)

### ✅ Cross-Platform Compatibility
- Works on both Windows and Linux
- Uses the same file upload service as news creation
- Automatic path sanitization and folder creation

### ✅ Error Handling
- **Graceful Degradation**: News article updates even if image upload fails
- **Comprehensive Logging**: Detailed error logging for debugging
- **User-Friendly Messages**: Clear success/error messages

### ✅ File Management
- **Automatic Cleanup**: Old images are deleted when replaced
- **Organized Storage**: Images are stored in `/uploads/news/` folder
- **Unique Naming**: Files are named with timestamps and GUIDs to prevent conflicts

## Usage Examples

### JavaScript/Fetch API

#### Regular Update (JSON)
```javascript
const updateNews = async (id, newsData) => {
  const response = await fetch(`/api/news/${id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(newsData)
  });
  return response.json();
};

// Usage
const newsData = {
  title: "Updated Title",
  fullContent: "Updated content",
  location: "Updated Location",
  category: "Events",
  author: "Updated Author",
  isFeatured: true,
  isTrending: false
};

const result = await updateNews(1, newsData);
```

#### Update with Image Upload
```javascript
const updateNewsWithImage = async (id, newsData, imageFile) => {
  const formData = new FormData();
  
  // Add text fields
  Object.keys(newsData).forEach(key => {
    if (key === 'tags') {
      formData.append(key, JSON.stringify(newsData[key]));
    } else {
      formData.append(key, newsData[key]);
    }
  });
  
  // Add image file if provided
  if (imageFile) {
    formData.append('image', imageFile);
  }
  
  const response = await fetch(`/api/news/${id}/with-image`, {
    method: 'PUT',
    body: formData
  });
  return response.json();
};

// Usage
const newsData = {
  title: "Updated Title",
  fullContent: "Updated content",
  location: "Updated Location",
  category: "Events",
  author: "Updated Author",
  isFeatured: true,
  isTrending: false
};

const imageFile = document.getElementById('imageInput').files[0];
const result = await updateNewsWithImage(1, newsData, imageFile);
```

### cURL Examples

#### Regular Update
```bash
curl -X PUT "http://localhost:7000/api/news/1" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Updated Title",
    "fullContent": "Updated content",
    "location": "Updated Location",
    "category": "Events",
    "author": "Updated Author",
    "isFeatured": true,
    "isTrending": false
  }'
```

#### Update with Image
```bash
curl -X PUT "http://localhost:7000/api/news/1/with-image" \
  -F "title=Updated Title" \
  -F "fullContent=Updated content" \
  -F "location=Updated Location" \
  -F "category=Events" \
  -F "author=Updated Author" \
  -F "isFeatured=true" \
  -F "isTrending=false" \
  -F "image=@/path/to/new-image.jpg"
```

## Response Format

Both endpoints return the same response format:

```json
{
  "success": true,
  "data": {
    "id": 1,
    "title": "Updated Title",
    "summary": "Updated summary",
    "fullContent": "Updated content",
    "imageUrl": "/uploads/news/20241201_143022_guid_filename.jpg",
    "publishedDate": "2024-12-01T00:00:00Z",
    "publishedTime": "14:30",
    "location": "Updated Location",
    "expectedAttendees": "100-200 people",
    "category": "Events",
    "author": "Updated Author",
    "tags": ["tag1", "tag2"],
    "isFeatured": true,
    "isTrending": false,
    "viewCount": 0,
    "status": "Draft"
  },
  "message": "News article updated successfully with new image"
}
```

## Error Handling

### Common Error Responses

#### News Article Not Found
```json
{
  "success": false,
  "message": "News article not found"
}
```

#### Validation Error
```json
{
  "success": false,
  "message": "Validation failed",
  "errors": ["Title is required", "FullContent is required"]
}
```

#### Server Error
```json
{
  "success": false,
  "message": "An error occurred while updating the news article",
  "errors": ["Database connection failed"]
}
```

## Best Practices

1. **Image Optimization**: Optimize images before upload for better performance
2. **File Size**: Keep images under 10MB (configurable)
3. **Error Handling**: Always handle potential upload failures gracefully
4. **User Feedback**: Provide clear feedback about upload progress and results
5. **Backup**: Consider backing up images before replacement

## Migration Notes

- The existing `PUT /api/news/{id}` endpoint now uses `UpdateNewsRequest` instead of `CreateNewsRequest`
- New `PUT /api/news/{id}/with-image` endpoint for image uploads
- Both endpoints maintain backward compatibility with existing functionality
- Image upload is completely optional - existing behavior is preserved 