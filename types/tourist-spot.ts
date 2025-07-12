// Tourist Spot Types

export interface TouristSpot {
  id: number;
  name: string;
  description: string;
  imageUrl: string;
  rating: number;
  location: string;
  coordinates?: string;
  address: string;
  openingHours?: string;
  entryFee?: string;
  highlights: string[];
  travelTime?: string;
  isActive: boolean;
  viewCount: number;
  createdAt: string;
  updatedAt: string;
}

export interface CreateTouristSpotRequest {
  name: string;
  description: string;
  imageUrl: string;
  rating?: number; // defaults to 5.0
  location: string;
  coordinates?: string;
  address: string;
  openingHours?: string;
  entryFee?: string;
  highlights?: string[];
  travelTime?: string;
  isActive?: boolean; // defaults to true
}

export interface CreateTouristSpotWithImageRequest {
  name: string;
  description: string;
  rating?: number; // defaults to 5.0
  location: string;
  coordinates?: string;
  address: string;
  openingHours?: string;
  entryFee?: string;
  highlights?: string[];
  travelTime?: string;
  isActive?: boolean; // defaults to true
  image?: File; // Optional image file
}

export interface UpdateTouristSpotRequest {
  name: string;
  description: string;
  imageUrl: string;
  rating?: number; // defaults to 5.0
  location: string;
  coordinates?: string;
  address: string;
  openingHours?: string;
  entryFee?: string;
  highlights?: string[];
  travelTime?: string;
  isActive?: boolean; // defaults to true
}

export interface UpdateTouristSpotWithImageRequest {
  name: string;
  description: string;
  rating?: number; // defaults to 5.0
  location: string;
  coordinates?: string;
  address: string;
  openingHours?: string;
  entryFee?: string;
  highlights?: string[];
  travelTime?: string;
  isActive?: boolean; // defaults to true
  image?: File; // Optional image file
}

export interface TouristSpotListResponse {
  success: boolean;
  data: TouristSpot[];
  message: string;
}

export interface TouristSpotResponse {
  success: boolean;
  data: TouristSpot;
  message: string;
}

export interface TouristSpotFilters {
  page?: number;
  pageSize?: number;
  isActive?: boolean;
} 