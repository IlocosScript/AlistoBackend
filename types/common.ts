// Common types and enums shared across all endpoints

// ============================================================================
// ENUMS
// ============================================================================

export enum AppointmentStatus {
  Pending = 'Pending',
  Confirmed = 'Confirmed',
  InProgress = 'InProgress',
  Completed = 'Completed',
  Cancelled = 'Cancelled',
  NoShow = 'NoShow'
}

export enum PaymentStatus {
  Pending = 'Pending',
  Paid = 'Paid',
  Refunded = 'Refunded',
  Failed = 'Failed'
}

export enum IssueCategory {
  Flooding = 'Flooding',
  RoadIssues = 'RoadIssues',
  FireHazard = 'FireHazard',
  PowerOutage = 'PowerOutage',
  Environmental = 'Environmental',
  PublicSafety = 'PublicSafety',
  Infrastructure = 'Infrastructure',
  Emergency = 'Emergency'
}

export enum IssueStatus {
  Submitted = 'Submitted',
  UnderReview = 'UnderReview',
  InProgress = 'InProgress',
  OnHold = 'OnHold',
  Resolved = 'Resolved',
  Closed = 'Closed',
  Rejected = 'Rejected'
}

export enum UrgencyLevel {
  Low = 'Low',
  Medium = 'Medium',
  High = 'High',
  Critical = 'Critical'
}

export enum Priority {
  Low = 'Low',
  Medium = 'Medium',
  High = 'High',
  Urgent = 'Urgent'
}

export enum NewsCategory {
  Festival = 'Festival',
  Infrastructure = 'Infrastructure',
  Health = 'Health',
  Education = 'Education',
  Environment = 'Environment',
  Culture = 'Culture',
  Technology = 'Technology',
  Sports = 'Sports',
  Government = 'Government',
  Emergency = 'Emergency'
}

export enum ContentStatus {
  Draft = 'Draft',
  Published = 'Published',
  Archived = 'Archived',
  Scheduled = 'Scheduled'
}

export enum SortDirection {
  Ascending = 'Ascending',
  Descending = 'Descending'
}

// ============================================================================
// COMMON INTERFACES
// ============================================================================

export interface ApiResponse<T> {
  success: boolean;
  data?: T;
  message?: string;
  errors?: string[];
  timestamp: string; // ISO date string
  requestId?: string;
}

export interface PaginatedResponse<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}

export interface PaginationRequest {
  pageNumber: number;
  pageSize: number;
  sortBy?: string;
  sortDirection?: SortDirection;
}

// ============================================================================
// UTILITY TYPES
// ============================================================================

export type ApiResponseWrapper<T> = ApiResponse<T>;
export type PaginatedApiResponse<T> = ApiResponse<PaginatedResponse<T>>;

// Helper type for creating new entities (without ID and timestamps)
export type CreateEntity<T> = Omit<T, 'id' | 'createdAt' | 'updatedAt'>;

// Helper type for updating entities (without ID and timestamps)
export type UpdateEntity<T> = Partial<Omit<T, 'id' | 'createdAt' | 'updatedAt'>>;

// Helper type for optional fields
export type Optional<T, K extends keyof T> = Omit<T, K> & Partial<Pick<T, K>>; 