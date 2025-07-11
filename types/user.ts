// User-related types for user management endpoints

import { ApiResponse, PaginatedResponse, PaginationRequest } from './common';
import { UserDto } from './auth';

// ============================================================================
// REQUEST TYPES
// ============================================================================

export interface UpdateUserRequest {
  firstName?: string;
  lastName?: string;
  middleName?: string;
  phoneNumber?: string;
  address?: string;
  dateOfBirth?: string; // ISO date string
  emergencyContactName?: string;
  emergencyContactNumber?: string;
  profileImageUrl?: string;
}

// ============================================================================
// RESPONSE TYPES
// ============================================================================

// UserDto is already defined in auth.ts and imported above

// ============================================================================
// API RESPONSE TYPES
// ============================================================================

export type GetUsersResponse = ApiResponse<UserDto[]>;
export type GetUserResponse = ApiResponse<UserDto>;
export type CreateUserResponse = ApiResponse<UserDto>;
export type UpdateUserResponse = ApiResponse<UserDto>;
export type DeleteUserResponse = ApiResponse<object>;
export type ActivateUserResponse = ApiResponse<object>;

// ============================================================================
// QUERY PARAMETER TYPES
// ============================================================================

export interface GetUsersQueryParams extends PaginationRequest {
  // Additional query parameters can be added here if needed
} 