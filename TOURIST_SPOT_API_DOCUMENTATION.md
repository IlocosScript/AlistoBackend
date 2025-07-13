# Tourist Spot API Documentation

## Base URL
```
https://your-api-domain.com/api/touristspot
```

---

## 1. CREATE TOURIST SPOT
**Endpoint:** `POST /api/touristspot/with-image`

### Required Fields
| Field | Type | Description |
|-------|------|-------------|
| `name` | string | Tourist spot name |
| `description` | string | Description |
| `location` | string | Location |
| `isActive` | boolean | Whether spot is active |

### Optional Fields
| Field | Type | Description |
|-------|------|-------------|
| `image` | file | Tourist spot image |
| `address` | string | Address |
| `contactNumber` | string | Contact number |
| `email` | string | Email address |
| `website` | string | Website URL |
| `operatingHours` | string | Operating hours |
| `entranceFee` | string | Entrance fee |
| `tags` | array | Array of tags |

---

## 2. UPDATE TOURIST SPOT
**Endpoint:** `PUT /api/touristspot/{id}/with-image`

### Required Fields
| Field | Type | Description |
|-------|------|-------------|
| `name` | string | Tourist spot name |
| `description` | string | Description |
| `location` | string | Location |
| `isActive` | boolean | Whether spot is active |

### Optional Fields
| Field | Type | Description |
|-------|------|-------------|
| `image` | file | New image to upload |
| `address` | string | Address |
| `contactNumber` | string | Contact number |
| `email` | string | Email address |
| `website` | string | Website URL |
| `operatingHours` | string | Operating hours |
| `entranceFee` | string | Entrance fee |
| `tags` | array | Array of tags |

---

## 3. DELETE TOURIST SPOT
**Endpoint:** `DELETE /api/touristspot/{id}`

### Path Parameters
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `id` | integer | ✅ | Tourist spot ID to delete |

---

## 4. VIEW TOURIST SPOT
**Endpoint:** `GET /api/touristspot/{id}`

### Path Parameters
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `id` | integer | ✅ | Tourist spot ID to view |

---

## 5. GET ALL TOURIST SPOTS
**Endpoint:** `GET /api/touristspot?page=1&pageSize=10&isActive=true`

### Query Parameters
| Parameter | Type | Required | Description | Default |
|-----------|------|----------|-------------|---------|
| `page` | integer | ❌ | Page number | 1 |
| `pageSize` | integer | ❌ | Items per page | 10 |
| `isActive` | boolean | ❌ | Filter by active status | - |

---

## 6. ACTIVATE TOURIST SPOT
**Endpoint:** `PATCH /api/touristspot/{id}/activate`

### Path Parameters
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `id` | integer | ✅ | Tourist spot ID to activate |

---

## 7. DEACTIVATE TOURIST SPOT
**Endpoint:** `PATCH /api/touristspot/{id}/deactivate`

### Path Parameters
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `id` | integer | ✅ | Tourist spot ID to deactivate |

---

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
    "isActive": true,
    "viewCount": 500
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

---

## Important Notes

### Partial Updates
- **Supported** - Only send fields you want to change
- **Existing values preserved** - Fields not sent keep their current values
- **Image replacement** - New image upload replaces existing image

### Status Management
- **No publish/unpublish** - Tourist spots use activate/deactivate instead
- **Active status** - Controls visibility to public users
- **Boolean field** - `isActive` true/false instead of status enum

### Image Handling
- **Automatic cleanup** - Old images deleted when new image uploaded
- **File validation** - Only image files accepted
- **Unique filenames** - Timestamp + GUID prevents collisions

### No Categories
- **Tourist spots don't use categories** - Unlike news articles
- **Filtering** - Only by active status, not by category

### Pagination
- **Response headers** - X-Total-Count, X-Total-Pages, X-Current-Page
- **Default page size** - 10 items per page
- **Page-based** - Use page parameter for navigation

---

## Usage Examples

### Create Tourist Spot
- Send all required fields
- Include optional image upload
- Set initial active status

### Update Tourist Spot
- Send only fields to change
- Optional image replacement
- Maintain existing data for unchanged fields

### Delete Tourist Spot
- Requires confirmation
- Permanent deletion
- No recovery option

### Activate/Deactivate
- Toggle visibility
- No data loss
- Reversible action 