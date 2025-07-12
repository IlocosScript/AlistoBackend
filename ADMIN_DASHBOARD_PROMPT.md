# Alisto Admin Dashboard - Bolt Generation Prompt

## 🎯 **Admin Dashboard Generation Prompt for Bolt**

---

**Create a comprehensive admin dashboard for the Alisto City Management API using Next.js 14+ with Clerk authentication. The dashboard should focus on managing News Articles and Tourist Spots.**

### **📋 Project Requirements**

#### **Tech Stack**
- **Framework**: Next.js 14+ with App Router
- **Authentication**: Clerk (already configured)
- **Styling**: Tailwind CSS + shadcn/ui components
- **State Management**: React Query/TanStack Query
- **HTTP Client**: Axios or fetch API
- **Icons**: Lucide React
- **Date Handling**: date-fns
- **Form Handling**: React Hook Form + Zod validation

#### **Core Features**
1. **Authentication & Authorization**
   - Clerk authentication integration
   - Protected routes for admin access
   - User profile management

2. **News Management**
   - CRUD operations for news articles
   - Image upload functionality
   - Rich text editor for content
   - Status management (Draft/Published)
   - Featured/Trending toggles
   - Category management
   - Search and filtering

3. **Tourist Spots Management**
   - CRUD operations for tourist spots
   - Image upload functionality
   - Rating management
   - Active/Inactive status
   - Search and filtering
   - Highlights management

### **🏗️ Project Structure**

```
alisto-admin-dashboard/
├── app/
│   ├── (auth)/
│   │   ├── sign-in/[[...sign-in]]/page.tsx
│   │   └── sign-up/[[...sign-up]]/page.tsx
│   ├── (dashboard)/
│   │   ├── layout.tsx
│   │   ├── page.tsx
│   │   ├── news/
│   │   │   ├── page.tsx
│   │   │   ├── create/page.tsx
│   │   │   ├── [id]/edit/page.tsx
│   │   │   └── components/
│   │   │       ├── NewsTable.tsx
│   │   │       ├── NewsForm.tsx
│   │   │       ├── NewsFilters.tsx
│   │   │       └── ImageUpload.tsx
│   │   ├── tourist-spots/
│   │   │   ├── page.tsx
│   │   │   ├── create/page.tsx
│   │   │   ├── [id]/edit/page.tsx
│   │   │   └── components/
│   │   │       ├── TouristSpotTable.tsx
│   │   │       ├── TouristSpotForm.tsx
│   │   │       ├── TouristSpotFilters.tsx
│   │   │       └── ImageUpload.tsx
│   │   └── components/
│   │       ├── Sidebar.tsx
│   │       ├── Header.tsx
│   │       ├── DataTable.tsx
│   │       └── LoadingSpinner.tsx
│   ├── api/
│   │   └── auth/[...nextauth]/route.ts
│   ├── globals.css
│   ├── layout.tsx
│   └── page.tsx
├── lib/
│   ├── api.ts
│   ├── utils.ts
│   ├── validations.ts
│   └── constants.ts
├── hooks/
│   ├── useNews.ts
│   ├── useTouristSpots.ts
│   └── useImageUpload.ts
├── types/
│   ├── news.ts
│   ├── tourist-spot.ts
│   └── api.ts
└── components/
    ├── ui/
    └── forms/
```

### **🔧 API Integration**

#### **Base Configuration**
```typescript
// lib/api.ts
const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:7000/api';

export const api = {
  // News endpoints
  news: {
    getAll: (params?: any) => `${API_BASE_URL}/news?${new URLSearchParams(params)}`,
    getById: (id: number) => `${API_BASE_URL}/news/${id}`,
    create: () => `${API_BASE_URL}/news`,
    createWithImage: () => `${API_BASE_URL}/news/with-image`,
    update: (id: number) => `${API_BASE_URL}/news/${id}`,
    updateWithImage: (id: number) => `${API_BASE_URL}/news/${id}/with-image`,
    delete: (id: number) => `${API_BASE_URL}/news/${id}`,
    publish: (id: number) => `${API_BASE_URL}/news/${id}/publish`,
  },
  
  // Tourist spots endpoints
  touristSpots: {
    getAll: (params?: any) => `${API_BASE_URL}/touristspot?${new URLSearchParams(params)}`,
    getById: (id: number) => `${API_BASE_URL}/touristspot/${id}`,
    create: () => `${API_BASE_URL}/touristspot`,
    createWithImage: () => `${API_BASE_URL}/touristspot/with-image`,
    update: (id: number) => `${API_BASE_URL}/touristspot/${id}`,
    updateWithImage: (id: number) => `${API_BASE_URL}/touristspot/${id}/with-image`,
    delete: (id: number) => `${API_BASE_URL}/touristspot/${id}`,
    activate: (id: number) => `${API_BASE_URL}/touristspot/${id}/activate`,
    deactivate: (id: number) => `${API_BASE_URL}/touristspot/${id}/deactivate`,
  }
};
```

### **🎨 Dashboard Features**

#### **1. News Management Dashboard**
- **List View**: 
  - Data table with pagination
  - Search by title, author, category
  - Filter by status (Draft/Published), featured, trending
  - Sort by date, title, view count
  - Bulk actions (delete, publish, feature)

- **Create/Edit Form**:
  - Title, description, full content (rich text editor)
  - Category selection (Events, Announcements, etc.)
  - Location, expected attendees
  - Published date/time picker
  - Image upload with preview
  - Featured/Trending toggles
  - Tags management
  - Author field

- **Image Upload**:
  - Drag & drop interface
  - File validation (images only, max 10MB)
  - Preview before upload
  - Progress indicator
  - Error handling

#### **2. Tourist Spots Management Dashboard**
- **List View**:
  - Data table with pagination
  - Search by name, location, address
  - Filter by active status, rating
  - Sort by name, rating, view count
  - Bulk actions (delete, activate/deactivate)

- **Create/Edit Form**:
  - Name, description
  - Location, address, coordinates
  - Rating (1-5 stars)
  - Opening hours, entry fee
  - Travel time
  - Image upload with preview
  - Highlights management (tags)
  - Active status toggle

### **🎨 UI/UX Requirements**

#### **Design System**
- **Color Scheme**: Professional admin theme
- **Typography**: Clean, readable fonts
- **Layout**: Responsive sidebar layout
- **Components**: Consistent shadcn/ui components
- **Icons**: Lucide React icons throughout

#### **Key Pages**
1. **Dashboard Home**: Overview with stats and recent activity
2. **News List**: Table with filters and actions
3. **News Create/Edit**: Form with image upload
4. **Tourist Spots List**: Table with filters and actions
5. **Tourist Spots Create/Edit**: Form with image upload

#### **Interactive Elements**
- **Loading States**: Skeleton loaders and spinners
- **Error Handling**: Toast notifications for errors
- **Success Feedback**: Confirmation dialogs and success messages
- **Form Validation**: Real-time validation with error messages
- **Image Preview**: Thumbnail previews and full-size modals

### **🔐 Authentication & Security**

#### **Clerk Integration**
- Protected routes with middleware
- User session management
- Role-based access (admin only)
- Sign-in/Sign-up pages
- User profile management

#### **API Security**
- JWT token handling
- Request/response interceptors
- Error handling for auth failures
- Automatic token refresh

### **📱 Responsive Design**

#### **Breakpoints**
- **Desktop**: Full sidebar layout
- **Tablet**: Collapsible sidebar
- **Mobile**: Bottom navigation or hamburger menu

#### **Mobile Optimizations**
- Touch-friendly interfaces
- Swipe gestures for actions
- Optimized form layouts
- Mobile-friendly image upload

### **⚡ Performance & Optimization**

#### **Data Fetching**
- React Query for caching and state management
- Optimistic updates for better UX
- Background refetching
- Error retry logic

#### **Image Optimization**
- Lazy loading for images
- Thumbnail generation
- Progressive image loading
- WebP format support

### **🧪 Testing & Quality**

#### **Code Quality**
- TypeScript for type safety
- ESLint and Prettier configuration
- Component testing with Jest/React Testing Library
- API mocking for development

#### **Error Boundaries**
- Global error handling
- Graceful degradation
- User-friendly error messages
- Error reporting

### **📦 Dependencies**

```json
{
  "dependencies": {
    "next": "^14.0.0",
    "react": "^18.0.0",
    "react-dom": "^18.0.0",
    "@clerk/nextjs": "^4.0.0",
    "@tanstack/react-query": "^5.0.0",
    "axios": "^1.6.0",
    "react-hook-form": "^7.48.0",
    "@hookform/resolvers": "^3.3.0",
    "zod": "^3.22.0",
    "lucide-react": "^0.294.0",
    "date-fns": "^2.30.0",
    "class-variance-authority": "^0.7.0",
    "clsx": "^2.0.0",
    "tailwind-merge": "^2.0.0"
  },
  "devDependencies": {
    "@types/node": "^20.0.0",
    "@types/react": "^18.0.0",
    "@types/react-dom": "^18.0.0",
    "typescript": "^5.0.0",
    "tailwindcss": "^3.3.0",
    "autoprefixer": "^10.4.0",
    "postcss": "^8.4.0",
    "eslint": "^8.0.0",
    "eslint-config-next": "^14.0.0",
    "prettier": "^3.0.0"
  }
}
```

### **🚀 Getting Started Instructions**

1. **Setup Next.js project** with TypeScript
2. **Install Clerk** and configure authentication
3. **Setup Tailwind CSS** and shadcn/ui
4. **Configure API integration** with the Alisto backend
5. **Create dashboard layout** with sidebar navigation
6. **Implement News management** features
7. **Implement Tourist Spots management** features
8. **Add image upload functionality** for both modules
9. **Implement responsive design** and mobile optimization
10. **Add error handling** and loading states
11. **Test all CRUD operations** and image uploads
12. **Deploy and configure** environment variables

### **🎯 Success Criteria**

The dashboard should successfully:
- ✅ Authenticate users with Clerk
- ✅ Display and manage News articles with image upload
- ✅ Display and manage Tourist Spots with image upload
- ✅ Handle all CRUD operations for both modules
- ✅ Provide responsive design for all screen sizes
- ✅ Include proper error handling and loading states
- ✅ Support image upload with preview and validation
- ✅ Include search, filtering, and pagination
- ✅ Provide intuitive navigation and user experience

---

## 🔗 **API Data Context**

### **Base API Information**

**API Base URL**: `http://localhost:7000/api` (development)
**Authentication**: JWT Bearer token (optional for public endpoints)
**CORS**: Configured to allow all origins, methods, and headers
**File Upload**: Local storage in `/uploads/` folder structure

---

### **📰 News API Endpoints & Data Models**

#### **News Endpoints**
```
GET    /api/news                    - Get all news with pagination
GET    /api/news/{id}               - Get single news article
POST   /api/news                    - Create news (JSON)
POST   /api/news/with-image         - Create news with image upload
PUT    /api/news/{id}               - Update news (JSON)
PUT    /api/news/{id}/with-image    - Update news with image upload
DELETE /api/news/{id}               - Delete news
PATCH  /api/news/{id}/publish       - Publish news article
GET    /api/news/featured           - Get featured news
GET    /api/news/trending           - Get trending news
```

#### **News Data Models**

**NewsArticle Model (Database)**
```typescript
interface NewsArticle {
  id: number;
  title: string;                    // Required, max 200 chars
  summary?: string;                 // Optional
  fullContent: string;              // Required
  imageUrl?: string;                // Optional, max 500 chars
  publishedDate?: Date;             // Optional
  publishedTime?: string;           // Optional
  location: string;                 // Required, max 200 chars
  expectedAttendees?: string;       // Optional
  category: NewsCategory;           // Required enum
  author: string;                   // Required
  tags: string[];                   // JSON array
  isFeatured: boolean;              // Required
  isTrending: boolean;              // Required
  status: ContentStatus;            // Draft/Published
  viewCount: number;                // Auto-increment
  createdAt: Date;                  // Auto-generated
  updatedAt: Date;                  // Auto-generated
}
```

**NewsCategory Enum**
```typescript
enum NewsCategory {
  Events = "Events",
  Announcements = "Announcements",
  Updates = "Updates",
  Emergency = "Emergency",
  Community = "Community"
}
```

**ContentStatus Enum**
```typescript
enum ContentStatus {
  Draft = "Draft",
  Published = "Published"
}
```

**CreateNewsRequest DTO**
```typescript
interface CreateNewsRequest {
  title: string;                    // Required
  summary?: string;                 // Optional
  fullContent: string;              // Required
  imageUrl?: string;                // Optional
  publishedDate?: Date;             // Optional
  publishedTime?: string;           // Optional
  location: string;                 // Required
  expectedAttendees?: string;       // Optional
  category: NewsCategory;           // Required
  author: string;                   // Required
  tags: string[];                   // Optional
  isFeatured: boolean;              // Required
  isTrending: boolean;              // Required
}
```

**CreateNewsWithImageRequest DTO**
```typescript
interface CreateNewsWithImageRequest {
  title: string;                    // Required
  summary?: string;                 // Optional
  fullContent: string;              // Required
  publishedDate?: Date;             // Optional
  publishedTime?: string;           // Optional
  location: string;                 // Required
  expectedAttendees?: string;       // Optional
  category: NewsCategory;           // Required
  author: string;                   // Required
  tags: string[];                   // Optional
  isFeatured: boolean;              // Required
  isTrending: boolean;              // Required
  image?: File;                     // Optional image file
}
```

**NewsArticleDto (Response)**
```typescript
interface NewsArticleDto {
  id: number;
  title: string;
  summary?: string;
  fullContent?: string;
  imageUrl?: string;
  publishedDate?: string;
  publishedTime?: string;
  location: string;
  expectedAttendees?: string;
  category: string;                 // Enum as string
  author: string;
  tags: string[];
  isFeatured: boolean;
  isTrending: boolean;
  viewCount: number;
  status: string;                   // Enum as string
  createdAt: string;
  updatedAt: string;
}
```

---

### **🏖️ Tourist Spots API Endpoints & Data Models**

#### **Tourist Spots Endpoints**
```
GET    /api/touristspot                    - Get all tourist spots with pagination
GET    /api/touristspot/{id}               - Get single tourist spot
POST   /api/touristspot                    - Create tourist spot (JSON)
POST   /api/touristspot/with-image         - Create tourist spot with image upload
PUT    /api/touristspot/{id}               - Update tourist spot (JSON)
PUT    /api/touristspot/{id}/with-image    - Update tourist spot with image upload
DELETE /api/touristspot/{id}               - Delete tourist spot
PATCH  /api/touristspot/{id}/activate      - Activate tourist spot
PATCH  /api/touristspot/{id}/deactivate    - Deactivate tourist spot
```

#### **Tourist Spots Data Models**

**TouristSpot Model (Database)**
```typescript
interface TouristSpot {
  id: number;
  name: string;                     // Required, max 200 chars
  description: string;              // Required
  imageUrl?: string;                // Optional, max 500 chars
  rating: number;                   // Default 5.0, decimal(3,2)
  location: string;                 // Required, max 200 chars
  coordinates?: string;             // Optional, max 100 chars
  address: string;                  // Required, max 500 chars
  openingHours?: string;            // Optional, max 100 chars
  entryFee?: string;                // Optional, max 100 chars
  highlights: string[];             // JSON array
  travelTime?: string;              // Optional, max 100 chars
  isActive: boolean;                // Default true
  viewCount: number;                // Default 0
  createdAt: Date;                  // Auto-generated
  updatedAt: Date;                  // Auto-generated
}
```

**CreateTouristSpotRequest DTO**
```typescript
interface CreateTouristSpotRequest {
  name: string;                     // Required
  description: string;              // Required
  imageUrl?: string;                // Optional
  rating?: number;                  // Optional, defaults to 5.0
  location: string;                 // Required
  coordinates?: string;             // Optional
  address: string;                  // Required
  openingHours?: string;            // Optional
  entryFee?: string;                // Optional
  highlights?: string[];            // Optional
  travelTime?: string;              // Optional
  isActive?: boolean;               // Optional, defaults to true
}
```

**CreateTouristSpotWithImageRequest DTO**
```typescript
interface CreateTouristSpotWithImageRequest {
  name: string;                     // Required
  description: string;              // Required
  rating?: number;                  // Optional, defaults to 5.0
  location: string;                 // Required
  coordinates?: string;             // Optional
  address: string;                  // Required
  openingHours?: string;            // Optional
  entryFee?: string;                // Optional
  highlights?: string[];            // Optional
  travelTime?: string;              // Optional
  isActive?: boolean;               // Optional, defaults to true
  image?: File;                     // Optional image file
}
```

**TouristSpotDto (Response)**
```typescript
interface TouristSpotDto {
  id: number;
  name: string;
  description: string;
  imageUrl?: string;
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

---

### **📁 File Upload System**

#### **Upload Configuration**
```typescript
interface FileUploadConfig {
  basePath: string;                 // "uploads"
  baseUrl: string;                  // "/uploads"
  maxFileSize: number;              // 10485760 (10MB)
  allowedExtensions: string[];      // [".jpg", ".jpeg", ".png", ".gif", ".webp"]
}
```

#### **File Storage Structure**
```
uploads/
├── news/                    # News article images
│   ├── 20241201_143022_guid_filename.jpg
│   └── ...
├── tourist-spots/           # Tourist spot images
│   ├── 20241201_143022_guid_filename.jpg
│   └── ...
├── users/                   # User profile images
├── reports/                 # Issue report images
└── general/                 # General uploads
```

#### **Image Upload Response**
```typescript
// Successful upload returns URL like:
"/uploads/news/20241201_143022_a1b2c3d4-e5f6-7890-abcd-ef1234567890_image.jpg"
```

---

### **📊 Pagination & Filtering**

#### **Pagination Parameters**
```typescript
interface PaginationParams {
  page?: number;             // Default: 1
  pageSize?: number;         // Default: 10
}
```

#### **News Filtering Parameters**
```typescript
interface NewsFilters extends PaginationParams {
  category?: string;         // NewsCategory enum
  isFeatured?: boolean;
  isTrending?: boolean;
  status?: string;           // ContentStatus enum
}
```

#### **Tourist Spots Filtering Parameters**
```typescript
interface TouristSpotFilters extends PaginationParams {
  isActive?: boolean;
  location?: string;
}
```

#### **Pagination Response Headers**
```
X-Total-Count: 150
X-Total-Pages: 15
X-Current-Page: 1
```

---

### **🔐 Authentication & Headers**

#### **JWT Configuration**
```typescript
interface JwtSettings {
  secretKey: string;
  issuer: string;
  audience: string;
  expirationMinutes: number;
}
```

#### **Request Headers**
```typescript
// For authenticated requests
{
  "Authorization": "Bearer <jwt_token>",
  "Content-Type": "application/json"
}

// For file uploads
{
  "Content-Type": "multipart/form-data"
}
```

---

### **📝 Sample API Responses**

#### **News List Response**
```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "title": "City Festival 2024",
      "summary": "Annual city celebration",
      "fullContent": "Join us for the biggest city festival...",
      "imageUrl": "/uploads/news/20241201_143022_guid_festival.jpg",
      "publishedDate": "2024-12-01T00:00:00Z",
      "publishedTime": "14:30",
      "location": "City Center",
      "expectedAttendees": "10,000+ people",
      "category": "Events",
      "author": "City Admin",
      "tags": ["festival", "celebration", "community"],
      "isFeatured": true,
      "isTrending": false,
      "viewCount": 1250,
      "status": "Published",
      "createdAt": "2024-12-01T14:30:22Z",
      "updatedAt": "2024-12-01T14:30:22Z"
    }
  ],
  "message": "Retrieved 1 news articles successfully"
}
```

#### **Tourist Spot Response**
```json
{
  "success": true,
  "data": {
    "id": 1,
    "name": "Beach Resort",
    "description": "Beautiful beach resort with stunning ocean views",
    "imageUrl": "/uploads/tourist-spots/20241201_143022_guid_beach.jpg",
    "rating": 4.8,
    "location": "Coastal Area",
    "coordinates": "40.7128,-74.0060",
    "address": "123 Beach Road, Coastal City",
    "openingHours": "6:00 AM - 8:00 PM",
    "entryFee": "Free",
    "highlights": ["Ocean Views", "Beach Access", "Restaurants"],
    "travelTime": "30 minutes from city center",
    "isActive": true,
    "viewCount": 850,
    "createdAt": "2024-12-01T14:30:22Z",
    "updatedAt": "2024-12-01T14:30:22Z"
  },
  "message": "Tourist spot created successfully with image"
}
```

#### **Error Response**
```json
{
  "success": false,
  "message": "Validation failed",
  "errors": ["Title is required", "Description is required"]
}
```

---

### **🔑 Key Implementation Notes**

1. **Image Upload**: All image uploads use `multipart/form-data` and store files locally
2. **File Naming**: Images are automatically named with timestamp and GUID
3. **CORS**: Configured to allow all origins, methods, and headers
4. **Validation**: Server-side validation with detailed error messages
5. **Pagination**: Built-in pagination with header metadata
6. **Status Management**: News has Draft/Published status, Tourist Spots have Active/Inactive
7. **Rating System**: Tourist Spots use decimal rating (1.0-5.0)
8. **Categories**: News has predefined categories (Events, Announcements, etc.)
9. **Tags**: News supports custom tags, Tourist Spots use highlights
10. **Timestamps**: All entities have createdAt and updatedAt timestamps

---

## 🚀 **Usage Instructions for Bolt**

1. **Copy this entire file** and use it as input for Bolt
2. **Bolt will generate** a complete Next.js admin dashboard
3. **The generated code** will include all the features and API integration
4. **Follow the setup instructions** provided by Bolt
5. **Configure environment variables** for API URL and Clerk
6. **Test all functionality** with the provided API endpoints

---

**This comprehensive prompt provides Bolt with complete context about your API structure, data models, and requirements, ensuring the generated admin dashboard will integrate seamlessly with your Alisto City Management API.** 