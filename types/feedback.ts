// Feedback-related types for feedback endpoints

import { ApiResponse } from './common';

// ============================================================================
// REQUEST TYPES
// ============================================================================

export interface CreateAppFeedbackRequest {
  rating: number;
  comment?: string;
}

export interface CreateServiceFeedbackRequest {
  appointmentId: string;
  rating: number;
  comment?: string;
}

// ============================================================================
// RESPONSE TYPES
// ============================================================================

export interface AppFeedbackDto {
  id: string;
  userId: string;
  rating: number;
  comment?: string;
  status: string; // FeedbackStatus as string
  createdAt: string; // ISO date string
  updatedAt: string; // ISO date string
}

export interface ServiceFeedbackDto {
  id: string;
  appointmentId: string;
  rating: number;
  comment?: string;
  status: string; // FeedbackStatus as string
  createdAt: string; // ISO date string
  updatedAt: string; // ISO date string
}

// ============================================================================
// API RESPONSE TYPES
// ============================================================================

export type CreateAppFeedbackResponse = ApiResponse<AppFeedbackDto>;
export type CreateServiceFeedbackResponse = ApiResponse<ServiceFeedbackDto>;
export type GetAppFeedbacksResponse = ApiResponse<AppFeedbackDto[]>;
export type GetServiceFeedbacksResponse = ApiResponse<ServiceFeedbackDto[]>; 