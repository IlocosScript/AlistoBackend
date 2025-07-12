// Issue report-related types for issue reporting endpoints

import { ApiResponse, PaginatedResponse, IssueCategory, IssueStatus, UrgencyLevel, Priority } from './common';

// ============================================================================
// REQUEST TYPES
// ============================================================================

export interface CreateIssueReportRequest {
  userId?: string; // Optional for anonymous reports
  category: IssueCategory;
  urgencyLevel: UrgencyLevel;
  title: string;
  description: string;
  location: string;
  coordinates?: string; // GPS coordinates
  contactInfo?: string;
  priority: Priority;
  publiclyVisible?: boolean;
}

export interface UpdateIssueStatusRequest {
  status: IssueStatus;
  assignedDepartment?: string;
  assignedTo?: string;
  estimatedResolution?: string; // ISO date string
  resolutionNotes?: string;
}

// ============================================================================
// RESPONSE TYPES
// ============================================================================

export interface IssuePhotoDto {
  id: string;
  photoUrl: string;
  caption?: string;
  uploadedAt: string; // ISO date string
}

export interface IssueReportDto {
  id: string;
  referenceNumber: string;
  category: string; // IssueCategory as string
  urgencyLevel: string; // UrgencyLevel as string
  title: string;
  description: string;
  location: string;
  coordinates?: string;
  status: string; // IssueStatus as string
  priority: string; // Priority as string
  assignedDepartment?: string;
  estimatedResolution?: string; // ISO date string
  createdAt: string; // ISO date string
  photos: IssuePhotoDto[];
}

export interface IssueCategoryDto {
  value: string;
  displayName: string;
}

export interface IssueStatusDto {
  value: string;
  displayName: string;
}

// ============================================================================
// API RESPONSE TYPES
// ============================================================================

export type GetIssueReportsResponse = ApiResponse<IssueReportDto[]>;
export type GetIssueReportResponse = ApiResponse<IssueReportDto>;
export type CreateIssueReportResponse = ApiResponse<IssueReportDto>;
export type UpdateIssueReportResponse = ApiResponse<IssueReportDto>;
export type DeleteIssueReportResponse = ApiResponse<object>;
export type UpdateIssueStatusResponse = ApiResponse<IssueReportDto>;
export type GetIssueCategoriesResponse = ApiResponse<IssueCategoryDto[]>;
export type GetIssueStatusesResponse = ApiResponse<IssueStatusDto[]>;

// ============================================================================
// QUERY PARAMETER TYPES
// ============================================================================

export interface GetIssueReportsQueryParams {
  page?: number;
  pageSize?: number;
  category?: string;
  status?: string;
  priority?: string;
} 