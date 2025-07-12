# TouristSpot Image Upload Guide

## Overview

The TouristSpot API now supports image upload functionality similar to the NewsArticle system. Images are uploaded locally and stored in the `/uploads/tourist-spots/` folder, with the URL stored in the `ImageUrl` field.

## New Endpoints

### 1. Create Tourist Spot with Image Upload
**POST** `/api/touristspot/with-image`

Creates a new tourist spot with optional image upload.

**Request Body:** `multipart/form-data`
- `name` (string, required) - Tourist spot name
- `description` (string, required) - Detailed description
- `rating` (number, optional) - Rating (defaults to 5.0)
- `location` (string, required) - Location name
- `coordinates` (string, optional) - GPS coordinates
- `address` (string, required) - Full address
- `openingHours` (string, optional) - Operating hours
- `entryFee` (string, optional) - Entry fee information
- `highlights` (array of strings, optional) - List of highlights
- `travelTime` (string, optional) - Travel time information
- `isActive` (boolean, optional) - Active status (defaults to true)
- `image` (file, optional) - Image file to upload

### 2. Update Tourist Spot with Image Upload
**PUT** `/api/touristspot/{id}/with-image`

Updates an existing tourist spot with optional image upload. If an image is provided, it will replace the existing image.

**Request Body:** `multipart/form-data`
- `name` (string, required) - Tourist spot name
- `description` (string, required) - Detailed description
- `rating` (number, optional) - Rating (defaults to 5.0)
- `location` (string, required) - Location name
- `coordinates` (string, optional) - GPS coordinates
- `address` (string, required) - Full address
- `openingHours` (string, optional) - Operating hours
- `entryFee` (string, optional) - Entry fee information
- `highlights` (array of strings, optional) - List of highlights
- `travelTime` (string, optional) - Travel time information
- `isActive` (boolean, optional) - Active status (defaults to true)
- `image` (file, optional) - New image file (replaces existing)

## New DTOs

### CreateTouristSpotWithImageRequest
```csharp
public class CreateTouristSpotWithImageRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    public decimal Rating { get; set; } = 5.0m;

    [Required]
    [MaxLength(200)]
    public string Location { get; set; }

    [MaxLength(100)]
    public string? Coordinates { get; set; }

    [Required]
    [MaxLength(500)]
    public string Address { get; set; }

    [MaxLength(100)]
    public string? OpeningHours { get; set; }

    [MaxLength(100)]
    public string? EntryFee { get; set; }

    public List<string> Highlights { get; set; } = new List<string>();

    [MaxLength(100)]
    public string? TravelTime { get; set; }

    public bool IsActive { get; set; } = true;

    // Single image upload
    public IFormFile? Image { get; set; }
}
```

### UpdateTouristSpotWithImageRequest
```csharp
public class UpdateTouristSpotWithImageRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    public decimal Rating { get; set; } = 5.0m;

    [Required]
    [MaxLength(200)]
    public string Location { get; set; }

    [MaxLength(100)]
    public string? Coordinates { get; set; }

    [Required]
    [MaxLength(500)]
    public string Address { get; set; }

    [MaxLength(100)]
    public string? OpeningHours { get; set; }

    [MaxLength(100)]
    public string? EntryFee { get; set; }

    public List<string> Highlights { get; set; } = new List<string>();

    [MaxLength(100)]
    public string? TravelTime { get; set; }

    public bool IsActive { get; set; } = true;

    // Optional image upload - if provided, it will replace the existing image
    public IFormFile? Image { get; set; }
}
```

## Features

### ✅ Image Upload Support
- **Optional Upload**: Image upload is optional - tourist spots can be created without images
- **Automatic Replacement**: If a new image is uploaded during update, the old image is automatically deleted
- **Error Handling**: Tourist spot creation/update continues even if image upload fails
- **File Validation**: Only image files are accepted (.jpg, .jpeg, .png, .gif, .webp)

### ✅ Cross-Platform Compatibility
- Works on both Windows and Linux
- Uses the same file upload service as news articles
- Automatic path sanitization and folder creation

### ✅ File Management
- **Organized Storage**: Images are stored in `/uploads/tourist-spots/` folder
- **Automatic Cleanup**: Old images are deleted when replaced
- **Unique Naming**: Files are named with timestamps and GUIDs to prevent conflicts

### ✅ Error Handling
- **Graceful Degradation**: Tourist spot operations continue even if image upload fails
- **Comprehensive Logging**: Detailed error logging for debugging
- **User-Friendly Messages**: Clear success/error messages

## Usage Examples

### JavaScript/Fetch API

#### Create Tourist Spot with Image
```javascript
const createTouristSpotWithImage = async (touristSpotData, imageFile) => {
  const formData = new FormData();
  
  // Add text fields
  Object.keys(touristSpotData).forEach(key => {
    if (key === 'highlights') {
      formData.append(key, JSON.stringify(touristSpotData[key]));
    } else {
      formData.append(key, touristSpotData[key]);
    }
  });
  
  // Add image file if provided
  if (imageFile) {
    formData.append('image', imageFile);
  }
  
  const response = await fetch('/api/touristspot/with-image', {
    method: 'POST',
    body: formData
  });
  return response.json();
};

// Usage
const touristSpotData = {
  name: "Beach Resort",
  description: "Beautiful beach resort with stunning ocean views",
  location: "Coastal Area",
  address: "123 Beach Road, Coastal City",
  rating: 4.8,
  isActive: true
};

const imageFile = document.getElementById('imageInput').files[0];
const result = await createTouristSpotWithImage(touristSpotData, imageFile);
```

#### Update Tourist Spot with Image
```javascript
const updateTouristSpotWithImage = async (id, touristSpotData, imageFile) => {
  const formData = new FormData();
  
  // Add text fields
  Object.keys(touristSpotData).forEach(key => {
    if (key === 'highlights') {
      formData.append(key, JSON.stringify(touristSpotData[key]));
    } else {
      formData.append(key, touristSpotData[key]);
    }
  });
  
  // Add image file if provided
  if (imageFile) {
    formData.append('image', imageFile);
  }
  
  const response = await fetch(`/api/touristspot/${id}/with-image`, {
    method: 'PUT',
    body: formData
  });
  return response.json();
};

// Usage
const touristSpotData = {
  name: "Updated Beach Resort",
  description: "Updated description with new amenities",
  location: "Updated Coastal Area",
  address: "Updated 123 Beach Road, Coastal City",
  rating: 4.9
};

const imageFile = document.getElementById('imageInput').files[0];
const result = await updateTouristSpotWithImage(1, touristSpotData, imageFile);
```

### cURL Examples

#### Create Tourist Spot with Image
```bash
curl -X POST "http://localhost:7000/api/touristspot/with-image" \
  -F "name=Beach Resort" \
  -F "description=Beautiful beach resort with stunning ocean views" \
  -F "location=Coastal Area" \
  -F "address=123 Beach Road, Coastal City" \
  -F "rating=4.8" \
  -F "isActive=true" \
  -F "image=@/path/to/beach-resort.jpg"
```

#### Update Tourist Spot with Image
```bash
curl -X PUT "http://localhost:7000/api/touristspot/1/with-image" \
  -F "name=Updated Beach Resort" \
  -F "description=Updated description with new amenities" \
  -F "location=Updated Coastal Area" \
  -F "address=Updated 123 Beach Road, Coastal City" \
  -F "rating=4.9" \
  -F "image=@/path/to/updated-beach-resort.jpg"
```

## Response Format

Both endpoints return the same response format:

```json
{
  "success": true,
  "data": {
    "id": 1,
    "name": "Beach Resort",
    "description": "Beautiful beach resort with stunning ocean views",
    "imageUrl": "/uploads/tourist-spots/20241201_143022_guid_beach-resort.jpg",
    "rating": 4.8,
    "location": "Coastal Area",
    "coordinates": null,
    "address": "123 Beach Road, Coastal City",
    "openingHours": null,
    "entryFee": null,
    "highlights": [],
    "travelTime": null,
    "isActive": true,
    "viewCount": 0,
    "createdAt": "2024-12-01T14:30:22Z",
    "updatedAt": "2024-12-01T14:30:22Z"
  },
  "message": "Tourist spot created successfully with image"
}
```

## File Storage Structure

```
uploads/
├── tourist-spots/     # Tourist spot images
│   ├── 20241201_143022_guid_beach-resort.jpg
│   ├── 20241201_143022_guid_mountain-park.png
│   └── ...
├── news/              # News article images
├── users/             # User profile images
└── reports/           # Issue report images
```

## Error Handling

### Common Error Responses

#### Tourist Spot Not Found (Update)
```json
{
  "success": false,
  "message": "Tourist spot not found"
}
```

#### Validation Error
```json
{
  "success": false,
  "message": "Validation failed",
  "errors": ["Name is required", "Description is required"]
}
```

#### Server Error
```json
{
  "success": false,
  "message": "An error occurred while creating the tourist spot",
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

- The existing `POST /api/touristspot` and `PUT /api/touristspot/{id}` endpoints remain unchanged
- New endpoints provide image upload functionality
- Image upload is completely optional - existing functionality is preserved
- Uses the same file upload service as news articles for consistency

## TypeScript Types

The TypeScript types have been updated to include the new image upload interfaces:

```typescript
export interface CreateTouristSpotWithImageRequest {
  name: string;
  description: string;
  rating?: number;
  location: string;
  coordinates?: string;
  address: string;
  openingHours?: string;
  entryFee?: string;
  highlights?: string[];
  travelTime?: string;
  isActive?: boolean;
  image?: File; // Optional image file
}

export interface UpdateTouristSpotWithImageRequest {
  name: string;
  description: string;
  rating?: number;
  location: string;
  coordinates?: string;
  address: string;
  openingHours?: string;
  entryFee?: string;
  highlights?: string[];
  travelTime?: string;
  isActive?: boolean;
  image?: File; // Optional image file
}
```

The TouristSpot image upload functionality is now fully integrated and ready to use! 