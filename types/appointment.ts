// Appointment-related types for appointment management endpoints

import { ApiResponse, PaginatedResponse, AppointmentStatus, PaymentStatus } from './common';

// ============================================================================
// REQUEST TYPES
// ============================================================================

export interface CreateAppointmentRequest {
  userId: string;
  serviceId: number;
  appointmentDate: string; // ISO date string
  appointmentTime: string;
  notes?: string;
  applicantFirstName: string;
  applicantLastName: string;
  applicantMiddleName?: string;
  applicantContactNumber: string;
  applicantEmail?: string;
  applicantAddress: string;
  serviceSpecificData?: Record<string, any>;
}

export interface UpdateAppointmentStatusRequest {
  status: AppointmentStatus;
  notes?: string;
}

// ============================================================================
// RESPONSE TYPES
// ============================================================================

export interface AppointmentDto {
  id: string;
  referenceNumber: string;
  appointmentDate: string; // ISO date string
  appointmentTime: string;
  status: string; // AppointmentStatus as string
  totalFee: number;
  paymentStatus: string; // PaymentStatus as string
  serviceName: string;
  serviceCategory: string;
  applicantFirstName: string;
  applicantLastName: string;
  applicantContactNumber: string;
  createdAt: string; // ISO date string
  completedAt?: string; // ISO date string
}

export interface AppointmentStatusDto {
  value: string;
  displayName: string;
}

export interface PaymentStatusDto {
  value: string;
  displayName: string;
}

// ============================================================================
// API RESPONSE TYPES
// ============================================================================

export type GetAppointmentsResponse = ApiResponse<AppointmentDto[]>;
export type GetAppointmentResponse = ApiResponse<AppointmentDto>;
export type CreateAppointmentResponse = ApiResponse<AppointmentDto>;
export type UpdateAppointmentResponse = ApiResponse<AppointmentDto>;
export type CancelAppointmentResponse = ApiResponse<object>;
export type UpdateAppointmentStatusResponse = ApiResponse<AppointmentDto>;
export type GetAppointmentStatusesResponse = ApiResponse<AppointmentStatusDto[]>;
export type GetPaymentStatusesResponse = ApiResponse<PaymentStatusDto[]>;

// ============================================================================
// QUERY PARAMETER TYPES
// ============================================================================

export interface GetAppointmentsQueryParams {
  page?: number;
  pageSize?: number;
  status?: string;
  paymentStatus?: string;
  userId?: string;
} 