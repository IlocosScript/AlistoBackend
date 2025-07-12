// User-related types for user management endpoints

import { ApiResponse, PaginatedResponse, PaginationRequest } from './common';

// ============================================================================
// REQUEST TYPES
// ============================================================================

export interface CreateUserFromAuthRequest {
  externalId: string;
  authProvider: string;
  email: string;
  firstName: string;
  lastName: string;
  middleName?: string;
  phoneNumber?: string;
  address?: string;
  dateOfBirth?: string; // ISO date string
  profileImageUrl?: string;
  emergencyContactName?: string;
  emergencyContactNumber?: string;
}

export interface SyncUserFromAuthRequest {
  externalId: string;
  authProvider: string;
  email: string;
  firstName: string;
  lastName: string;
  middleName?: string;
  phoneNumber?: string;
  address?: string;
  dateOfBirth?: string; // ISO date string
  profileImageUrl?: string;
  emergencyContactName?: string;
  emergencyContactNumber?: string;
}

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

export interface UserDto {
  id: string;
  externalId?: string; // ID from third-party auth provider
  authProvider?: string; // e.g., "Google", "Microsoft", "Custom"
  email: string;
  firstName: string;
  lastName: string;
  middleName?: string;
  phoneNumber: string;
  address: string;
  dateOfBirth?: string; // ISO date string
  isActive: boolean;
  createdAt: string; // ISO date string
  lastLoginAt?: string; // ISO date string
  profileImageUrl?: string;
  emergencyContactName?: string;
  emergencyContactNumber?: string;
}

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