// Auth-related types for authentication endpoints

import { ApiResponse } from './common';

// ============================================================================
// REQUEST TYPES
// ============================================================================

export interface LoginRequest {
  email: string;
  password: string;
  deviceInfo?: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  confirmPassword: string;
  firstName: string;
  lastName: string;
  middleName?: string;
  phoneNumber: string;
  address: string;
  dateOfBirth?: string; // ISO date string
  emergencyContactName?: string;
  emergencyContactNumber?: string;
  deviceInfo?: string;
}

export interface RefreshTokenRequest {
  sessionId: string;
}

// ============================================================================
// RESPONSE TYPES
// ============================================================================

export interface AuthResponse {
  userId: string;
  email: string;
  firstName: string;
  lastName: string;
  sessionId: string;
  lastLoginAt?: string; // ISO date string
}

export interface UserDto {
  id: string;
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

export type LoginResponse = ApiResponse<AuthResponse>;
export type RegisterResponse = ApiResponse<AuthResponse>;
export type LogoutResponse = ApiResponse<object>;
export type RefreshTokenResponse = ApiResponse<AuthResponse>;
export type GetCurrentUserResponse = ApiResponse<UserDto>; 