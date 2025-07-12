// City service-related types for city service endpoints

import { ApiResponse, PaginatedResponse } from './common';

// ============================================================================
// RESPONSE TYPES
// ============================================================================

export interface CityServiceDto {
  id: number;
  name: string;
  description: string;
  fee: number;
  processingTime: string;
  requiredDocuments: string[];
  officeLocation: string;
  contactNumber?: string;
  operatingHours: string;
}

export interface ServiceCategoryDto {
  id: number;
  name: string;
  description: string;
  icon: string;
  isActive: boolean;
  createdAt: string; // ISO date string
  updatedAt: string; // ISO date string
}

// ============================================================================
// API RESPONSE TYPES
// ============================================================================

export type GetCityServicesResponse = ApiResponse<CityServiceDto[]>;
export type GetCityServiceResponse = ApiResponse<CityServiceDto>;
export type GetServiceCategoriesResponse = ApiResponse<ServiceCategoryDto[]>;

// ============================================================================
// QUERY PARAMETER TYPES
// ============================================================================

export interface GetCityServicesQueryParams {
  page?: number;
  pageSize?: number;
  categoryId?: number;
  isActive?: boolean;
} 