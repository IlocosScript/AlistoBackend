// News-related types for news management endpoints

import { ApiResponse, PaginatedResponse, NewsCategory, ContentStatus } from './common';

// ============================================================================
// REQUEST TYPES
// ============================================================================

export interface CreateNewsRequest {
  title: string;
  summary: string;
  fullContent: string;
  imageUrl: string;
  publishedDate: string; // ISO date string
  publishedTime: string;
  location: string;
  expectedAttendees?: string;
  category: NewsCategory;
  author: string;
  tags?: string[];
  isFeatured?: boolean;
  isTrending?: boolean;
}

// ============================================================================
// RESPONSE TYPES
// ============================================================================

export interface NewsArticleDto {
  id: number;
  title: string;
  summary: string;
  fullContent?: string; // Only included in detailed view
  imageUrl: string;
  publishedDate: string; // ISO date string
  publishedTime: string;
  location: string;
  expectedAttendees?: string;
  category: string; // NewsCategory as string
  author: string;
  tags: string[];
  isFeatured: boolean;
  isTrending: boolean;
  viewCount: number;
}

// ============================================================================
// API RESPONSE TYPES
// ============================================================================

export type GetNewsResponse = ApiResponse<NewsArticleDto[]>;
export type GetNewsArticleResponse = ApiResponse<NewsArticleDto>;
export type CreateNewsArticleResponse = ApiResponse<NewsArticleDto>;
export type UpdateNewsArticleResponse = ApiResponse<NewsArticleDto>;
export type DeleteNewsArticleResponse = ApiResponse<object>;
export type PublishNewsArticleResponse = ApiResponse<NewsArticleDto>;
export type GetFeaturedNewsResponse = ApiResponse<NewsArticleDto[]>;
export type GetTrendingNewsResponse = ApiResponse<NewsArticleDto[]>;

// ============================================================================
// QUERY PARAMETER TYPES
// ============================================================================

export interface GetNewsQueryParams {
  page?: number;
  pageSize?: number;
  category?: string;
  isFeatured?: boolean;
  isTrending?: boolean;
} 