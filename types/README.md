# Alisto Backend TypeScript Types

This directory contains TypeScript type definitions for the Alisto Backend API, organized by endpoint/feature for better maintainability.

## 📁 File Structure

```
types/
├── index.ts              # Main export file
├── common.ts             # Shared types and enums
├── auth.ts               # Authentication endpoints
├── user.ts               # User management endpoints
├── appointment.ts        # Appointment management endpoints
├── issue-report.ts       # Issue reporting endpoints
├── news.ts               # News management endpoints
├── dashboard.ts          # Dashboard and statistics endpoints
├── city-service.ts       # City service endpoints
├── feedback.ts           # Feedback endpoints
└── README.md             # This file
```

## 🚀 Usage

### Import All Types
```typescript
import * as AlistoTypes from './types';
```

### Import Specific Types
```typescript
import { 
  LoginRequest, 
  AppointmentDto, 
  ApiResponse 
} from './types';
```

### Import by Category
```typescript
import { LoginRequest, AuthResponse } from './types/auth';
import { AppointmentDto, CreateAppointmentRequest } from './types/appointment';
```

## 📋 Type Categories

### 🔐 Authentication (`auth.ts`)
- **Endpoints**: `/api/auth/*`
- **Types**: Login, Register, Token refresh
- **Key Types**: `LoginRequest`, `RegisterRequest`, `AuthResponse`

### 👤 User Management (`user.ts`)
- **Endpoints**: `/api/user/*`
- **Types**: User CRUD operations
- **Key Types**: `UserDto`, `UpdateUserRequest`

### 📅 Appointments (`appointment.ts`)
- **Endpoints**: `/api/appointment/*`
- **Types**: Appointment management
- **Key Types**: `AppointmentDto`, `CreateAppointmentRequest`

### 🚨 Issue Reports (`issue-report.ts`)
- **Endpoints**: `/api/issuereport/*`
- **Types**: Issue reporting and management
- **Key Types**: `IssueReportDto`, `CreateIssueReportRequest`

### 📰 News (`news.ts`)
- **Endpoints**: `/api/news/*`
- **Types**: News article management
- **Key Types**: `NewsArticleDto`, `CreateNewsRequest`

### 📊 Dashboard (`dashboard.ts`)
- **Endpoints**: Dashboard statistics
- **Types**: Statistics and analytics
- **Key Types**: `DashboardStatsDto`, `ServiceUsageDto`

### 🏛️ City Services (`city-service.ts`)
- **Endpoints**: City service information
- **Types**: Service listings and details
- **Key Types**: `CityServiceDto`, `ServiceCategoryDto`

### 💬 Feedback (`feedback.ts`)
- **Endpoints**: User feedback
- **Types**: App and service feedback
- **Key Types**: `AppFeedbackDto`, `ServiceFeedbackDto`

## 🔧 Common Types (`common.ts`)

### Enums
- `AppointmentStatus` - Pending, Confirmed, InProgress, etc.
- `PaymentStatus` - Pending, Paid, Refunded, Failed
- `IssueCategory` - Flooding, RoadIssues, FireHazard, etc.
- `IssueStatus` - Submitted, UnderReview, InProgress, etc.
- `NewsCategory` - Festival, Infrastructure, Health, etc.

### Response Wrappers
- `ApiResponse<T>` - Standard API response wrapper
- `PaginatedResponse<T>` - Paginated data wrapper
- `PaginatedApiResponse<T>` - Combined pagination and API response

### Utility Types
- `CreateEntity<T>` - Type for creating new entities
- `UpdateEntity<T>` - Type for updating entities
- `Optional<T, K>` - Make specific fields optional

## 📝 Usage Examples

### API Call with Types
```typescript
import { LoginRequest, LoginResponse } from './types';

const loginData: LoginRequest = {
  email: 'user@example.com',
  password: 'password123'
};

const response: LoginResponse = await fetch('/api/auth/login', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify(loginData)
});
```

### Component Props
```typescript
import { AppointmentDto } from './types';

interface AppointmentCardProps {
  appointment: AppointmentDto;
  onStatusChange: (status: string) => void;
}
```

### Form Validation
```typescript
import { CreateIssueReportRequest, IssueCategory, UrgencyLevel } from './types';

const validateIssueReport = (data: CreateIssueReportRequest): boolean => {
  return data.title.length > 0 && 
         data.description.length > 0 && 
         data.location.length > 0;
};
```

## 🔄 Type Mapping

### C# to TypeScript Conversions
- `DateTime` → `string` (ISO date format)
- `decimal` → `number`
- `bool` → `boolean`
- `string?` → `string | undefined`
- `ICollection<T>` → `T[]`
- `Guid` → `string`

### Enum Handling
- C# enums are converted to TypeScript string enums
- API responses return enum values as strings
- Use the enum types for type safety in requests

## 🛠️ Development

### Adding New Types
1. Create a new file in the `types/` directory
2. Export the types from the file
3. Add the export to `types/index.ts`
4. Update this README if needed

### Updating Existing Types
1. Modify the appropriate type file
2. Ensure all imports are updated
3. Test with your frontend code

## 📚 Best Practices

1. **Use specific imports** for better tree-shaking
2. **Leverage TypeScript's type checking** for API calls
3. **Use the utility types** for common patterns
4. **Keep types in sync** with backend changes
5. **Document complex types** with comments

## 🔗 Related Files

- Backend Controllers: `Alisto.Api/Controllers/`
- Backend DTOs: `Alisto.Api/DTO/`
- Backend Models: `Alisto.Api/Models/`
- Backend Enums: `Alisto.Api/Enums/` 