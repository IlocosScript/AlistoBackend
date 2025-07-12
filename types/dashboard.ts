// Dashboard-related types for dashboard and statistics endpoints

import { ApiResponse } from './common';

// ============================================================================
// RESPONSE TYPES
// ============================================================================

export interface ServiceUsageDto {
  serviceName: string;
  usageCount: number;
  satisfactionRating: number;
}

export interface DashboardStatsDto {
  totalUsers: number;
  activeAppointments: number;
  pendingIssues: number;
  completedProjects: number;
  customerSatisfactionRating: number;
  topServices: ServiceUsageDto[];
}

// ============================================================================
// API RESPONSE TYPES
// ============================================================================

export type GetDashboardStatsResponse = ApiResponse<DashboardStatsDto>; 