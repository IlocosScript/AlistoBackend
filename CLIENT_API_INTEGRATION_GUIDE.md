# Alisto API Integration Guide for Client Applications

## Base URL
```
https://alisto.gregdoesdev.xyz/
```

## Overview
This guide provides documentation for integrating the Alisto backend APIs to display Tourist Spots and News articles in client applications. All endpoints are read-only and designed for public viewing.

## Authentication
- **No authentication required** for viewing Tourist Spots and News
- All endpoints are publicly accessible
- CORS is enabled for all origins

## Response Format
All API responses follow this standard format:
```json
{
  "success": boolean,
  "data": object | array,
  "message": string,
  "errors": string[] (optional)
}
```

## Pagination Headers
For paginated endpoints, the following headers are included:
- `X-Total-Count`: Total number of items
- `X-Total-Pages`: Total number of pages
- `X-Current-Page`: Current page number

---

## Tourist Spots API

### 1. Get All Tourist Spots
**Endpoint:** `GET /api/touristspot`

**Query Parameters:**
- `page` (optional): Page number (default: 1)
- `pageSize` (optional): Items per page (default: 10)
- `isActive` (optional): Filter by active status (true/false)

**Example Request:**
```
GET https://alisto.gregdoesdev.xyz/api/touristspot?page=1&pageSize=10&isActive=true
```

**Response:**
```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "name": "City Park",
      "description": "Beautiful urban park with walking trails",
      "imageUrl": "/uploads/tourist-spots/city-park.jpg",
      "rating": 4.5,
      "location": "Downtown",
      "coordinates": "14.5995,120.9842",
      "address": "123 Main Street, City Center",
      "openingHours": "6:00 AM - 10:00 PM",
      "entryFee": "Free",
      "highlights": ["Walking trails", "Playground", "Picnic areas"],
      "travelTime": "15 minutes from city center",
      "isActive": true,
      "viewCount": 1250,
      "createdAt": "2024-01-15T10:30:00Z",
      "updatedAt": "2024-01-15T10:30:00Z"
    }
  ],
  "message": "Retrieved 10 tourist spots successfully"
}
```

### 2. Get Tourist Spot by ID
**Endpoint:** `GET /api/touristspot/{id}`

**Example Request:**
```
GET https://alisto.gregdoesdev.xyz/api/touristspot/1
```

**Response:**
```json
{
  "success": true,
  "data": {
    "id": 1,
    "name": "City Park",
    "description": "Beautiful urban park with walking trails",
    "imageUrl": "/uploads/tourist-spots/city-park.jpg",
    "rating": 4.5,
    "location": "Downtown",
    "coordinates": "14.5995,120.9842",
    "address": "123 Main Street, City Center",
    "openingHours": "6:00 AM - 10:00 PM",
    "entryFee": "Free",
    "highlights": ["Walking trails", "Playground", "Picnic areas"],
    "travelTime": "15 minutes from city center",
    "isActive": true,
    "viewCount": 1251,
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:30:00Z"
  },
  "message": "Tourist spot retrieved successfully"
}
```

---

## News API

### 1. Get All News Articles
**Endpoint:** `GET /api/news`

**Query Parameters:**
- `page` (optional): Page number (default: 1)
- `pageSize` (optional): Items per page (default: 10)
- `category` (optional): Filter by news category
- `isFeatured` (optional): Filter featured articles (true/false)
- `isTrending` (optional): Filter trending articles (true/false)

**Available Categories:**
- `Local`
- `National`
- `International`
- `Sports`
- `Entertainment`
- `Technology`
- `Business`
- `Health`
- `Education`
- `Environment`

**Example Request:**
```
GET https://alisto.gregdoesdev.xyz/api/news?page=1&pageSize=10&category=Local&isFeatured=true
```

**Response:**
```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "title": "New City Development Project Announced",
      "summary": "Mayor announces major infrastructure development",
      "imageUrl": "/uploads/news/city-development.jpg",
      "publishedDate": "2024-01-15",
      "publishedTime": "10:30:00",
      "location": "City Hall",
      "expectedAttendees": "500+",
      "category": "Local",
      "author": "John Smith",
      "tags": ["development", "infrastructure", "city hall"],
      "isFeatured": true,
      "isTrending": false,
      "viewCount": 1250,
      "status": "Published"
    }
  ],
  "message": "Retrieved 10 news articles successfully"
}
```

### 2. Get News Article by ID
**Endpoint:** `GET /api/news/{id}`

**Note:** This endpoint automatically increments the view count when accessed.

**Example Request:**
```
GET https://alisto.gregdoesdev.xyz/api/news/1
```

**Response:**
```json
{
  "success": true,
  "data": {
    "id": 1,
    "title": "New City Development Project Announced",
    "summary": "Mayor announces major infrastructure development",
    "fullContent": "The mayor today announced a comprehensive development project...",
    "imageUrl": "/uploads/news/city-development.jpg",
    "publishedDate": "2024-01-15",
    "publishedTime": "10:30:00",
    "location": "City Hall",
    "expectedAttendees": "500+",
    "category": "Local",
    "author": "John Smith",
    "tags": ["development", "infrastructure", "city hall"],
    "isFeatured": true,
    "isTrending": false,
    "viewCount": 1251,
    "status": "Published"
  },
  "message": "News article retrieved successfully"
}
```

### 3. Get Featured News Articles
**Endpoint:** `GET /api/news/featured`

**Example Request:**
```
GET https://alisto.gregdoesdev.xyz/api/news/featured
```

**Response:**
```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "title": "New City Development Project Announced",
      "summary": "Mayor announces major infrastructure development",
      "imageUrl": "/uploads/news/city-development.jpg",
      "publishedDate": "2024-01-15",
      "publishedTime": "10:30:00",
      "location": "City Hall",
      "expectedAttendees": "500+",
      "category": "Local",
      "author": "John Smith",
      "tags": ["development", "infrastructure", "city hall"],
      "isFeatured": true,
      "isTrending": false,
      "viewCount": 1250,
      "status": "Published"
    }
  ],
  "message": "Retrieved featured news articles successfully"
}
```

### 4. Get Trending News Articles
**Endpoint:** `GET /api/news/trending`

**Example Request:**
```
GET https://alisto.gregdoesdev.xyz/api/news/trending
```

**Response:**
```json
{
  "success": true,
  "data": [
    {
      "id": 2,
      "title": "Local Sports Team Wins Championship",
      "summary": "Historic victory for the city's sports team",
      "imageUrl": "/uploads/news/sports-victory.jpg",
      "publishedDate": "2024-01-14",
      "publishedTime": "20:15:00",
      "location": "Sports Arena",
      "expectedAttendees": "1000+",
      "category": "Sports",
      "author": "Jane Doe",
      "tags": ["sports", "championship", "victory"],
      "isFeatured": false,
      "isTrending": true,
      "viewCount": 2500,
      "status": "Published"
    }
  ],
  "message": "Retrieved trending news articles successfully"
}
```

---

## Image URLs

### Tourist Spot Images
- **Base URL:** `https://alisto.gregdoesdev.xyz/uploads/tourist-spots/`
- **Example:** `https://alisto.gregdoesdev.xyz/uploads/tourist-spots/city-park.jpg`

### News Images
- **Base URL:** `https://alisto.gregdoesdev.xyz/uploads/news/`
- **Example:** `https://alisto.gregdoesdev.xyz/uploads/news/city-development.jpg`

---

## Error Handling

### Common HTTP Status Codes
- `200 OK`: Request successful
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

### Error Response Format
```json
{
  "success": false,
  "message": "Error description",
  "errors": ["Detailed error message"]
}
```

### Example Error Response
```json
{
  "success": false,
  "message": "Tourist spot not found",
  "errors": []
}
```

---

## Best Practices for Client Integration

### 1. Pagination
- Always implement pagination for list endpoints
- Use the pagination headers to show navigation controls
- Consider implementing infinite scroll for better UX

### 2. Image Handling
- Always check if `imageUrl` exists before displaying
- Provide fallback images for missing photos
- Implement lazy loading for better performance

### 3. Error Handling
- Always check the `success` field in responses
- Display user-friendly error messages
- Implement retry logic for network failures

### 4. Caching
- Cache frequently accessed data (featured news, popular tourist spots)
- Implement proper cache invalidation strategies
- Use ETags if provided by the API

### 5. Performance
- Use appropriate page sizes (10-20 items per page)
- Implement search and filtering on the client side when possible
- Optimize image loading and display

### 6. User Experience
- Show loading states while fetching data
- Implement skeleton screens for better perceived performance
- Provide clear feedback for user actions

---

## TypeScript Types

### Tourist Spot Types
```typescript
interface TouristSpot {
  id: number;
  name: string;
  description: string;
  imageUrl: string;
  rating: number;
  location: string;
  coordinates?: string;
  address: string;
  openingHours?: string;
  entryFee?: string;
  highlights: string[];
  travelTime?: string;
  isActive: boolean;
  viewCount: number;
  createdAt: string;
  updatedAt: string;
}
```

### News Article Types
```typescript
interface NewsArticle {
  id: number;
  title: string;
  summary?: string;
  fullContent?: string;
  imageUrl?: string;
  publishedDate?: string;
  publishedTime?: string;
  location: string;
  expectedAttendees?: string;
  category: string;
  author: string;
  tags: string[];
  isFeatured: boolean;
  isTrending: boolean;
  viewCount: number;
  status: string;
}
```

---

## Rate Limiting
- No specific rate limits are currently enforced
- Implement reasonable request intervals in your client application
- Avoid making excessive requests in short time periods

## Support
For technical support or questions about the API integration, please contact the development team or refer to the API documentation. 