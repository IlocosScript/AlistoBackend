# Tourist Spot Management - UI Guide & API Documentation

## Overview
Complete guide for building Tourist Spot management UI with CRUD operations and image upload functionality.

## API Endpoints

### 1. CREATE TOURIST SPOT
**Endpoint:** `POST /api/tourist-spot/with-image`
**Method:** POST
**Content-Type:** `multipart/form-data`

**Required Fields:**
- `name` (text)
- `description` (textarea)
- `location` (text)
- `category` (dropdown)
- `isFeatured` (checkbox)
- `isTrending` (checkbox)

**Optional Fields:**
- `image` (file upload)
- `address` (text)
- `contactNumber` (text)
- `email` (email)
- `website` (url)
- `operatingHours` (text)
- `entranceFee` (text)
- `tags` (tag input)

### 2. UPDATE TOURIST SPOT
**Endpoint:** `PUT /api/tourist-spot/{id}/with-image`
**Method:** PUT
**Content-Type:** `multipart/form-data`

**All fields optional** - only send what you want to change

### 3. DELETE TOURIST SPOT
**Endpoint:** `DELETE /api/tourist-spot/{id}`
**Method:** DELETE

### 4. VIEW TOURIST SPOT
**Endpoint:** `GET /api/tourist-spot/{id}`
**Method:** GET

### 5. GET ALL TOURIST SPOTS
**Endpoint:** `GET /api/tourist-spot?page=1&pageSize=10&category=Nature&isFeatured=true`
**Method:** GET

## Tourist Spot Categories
```json
[
  "Historical",
  "Nature", 
  "Cultural",
  "Recreational",
  "Religious",
  "Shopping",
  "Food",
  "Adventure",
  "Educational",
  "Entertainment"
]
```

## Response Formats

### Success Response
```json
{
  "success": true,
  "data": {
    "id": 1,
    "name": "Tourist Spot Name",
    "description": "Description",
    "imageUrl": "https://example.com/image.jpg",
    "location": "Location",
    "category": "Nature",
    "isFeatured": true,
    "isTrending": false,
    "viewCount": 500,
    "status": "Published"
  },
  "message": "Success message"
}
```

### Error Response
```json
{
  "success": false,
  "message": "Error message"
}
```

## UI Components Required

### 1. List View
- **Purpose:** Display all tourist spots in a table format
- **Features:** 
  - Pagination
  - Search functionality
  - Filter by category
  - Sort by name, category, featured status
  - Action buttons (Edit, Delete, View)

### 2. Add Form
- **Purpose:** Create new tourist spot
- **Features:**
  - All required and optional fields
  - Image upload with preview
  - Form validation
  - Submit button
  - Cancel button

### 3. Edit Form
- **Purpose:** Update existing tourist spot
- **Features:**
  - Pre-populated with existing data
  - Image upload/replace functionality
  - Form validation
  - Update button
  - Cancel button

### 4. Delete Confirmation
- **Purpose:** Confirm deletion of tourist spot
- **Features:**
  - Modal dialog
  - Warning message
  - Confirm button
  - Cancel button

### 5. View Details
- **Purpose:** Display detailed tourist spot information
- **Features:**
  - Full tourist spot details
  - Image display
  - Edit button
  - Back to list button

### 6. Image Upload Component
- **Purpose:** Handle image uploads
- **Features:**
  - File input
  - Image preview
  - File type validation
  - File size validation
  - Remove image option

## Implementation Notes

### Form Fields
- **Text Inputs:** name, location, address, contactNumber, email, website, operatingHours, entranceFee
- **Textarea:** description
- **Dropdown:** category (use categories array)
- **Checkboxes:** isFeatured, isTrending
- **File Upload:** image
- **Tag Input:** tags (comma-separated or array)

### Validation Rules
- Required fields must not be empty
- Email must be valid format
- Website must be valid URL
- Image must be image file type
- Image size limit (e.g., 5MB)

### Error Handling
- Display validation errors below each field
- Show success/error messages after form submission
- Handle network errors gracefully
- Show loading states during API calls

### Responsive Design
- Mobile-friendly forms
- Responsive image upload
- Adaptive table layout
- Touch-friendly buttons

## Example Usage

### Create Tourist Spot
```javascript
const formData = new FormData();
formData.append('name', 'Mountain View Park');
formData.append('description', 'Beautiful mountain park with hiking trails');
formData.append('location', 'Mountain District');
formData.append('category', 'Nature');
formData.append('isFeatured', 'true');
formData.append('isTrending', 'false');
formData.append('image', imageFile);

const response = await fetch('/api/tourist-spot/with-image', {
  method: 'POST',
  body: formData
});
```

### Update Tourist Spot
```javascript
const formData = new FormData();
formData.append('name', 'Updated Mountain View Park');
formData.append('description', 'Updated description');
formData.append('image', newImageFile);

const response = await fetch(`/api/tourist-spot/1/with-image`, {
  method: 'PUT',
  body: formData
});
```

### Delete Tourist Spot
```javascript
const response = await fetch(`/api/tourist-spot/1`, {
  method: 'DELETE'
});
```

### Get Tourist Spot Details
```javascript
const response = await fetch(`/api/tourist-spot/1`);
const data = await response.json();
```

## File Structure Recommendation
```
src/
├── components/
│   ├── TouristSpotList.tsx
│   ├── TouristSpotForm.tsx
│   ├── TouristSpotDetails.tsx
│   ├── DeleteConfirmation.tsx
│   └── ImageUpload.tsx
├── pages/
│   ├── TouristSpots.tsx
│   ├── AddTouristSpot.tsx
│   ├── EditTouristSpot.tsx
│   └── ViewTouristSpot.tsx
└── services/
    └── touristSpotApi.ts
``` 