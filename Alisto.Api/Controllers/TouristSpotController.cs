using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alisto.Api.Data;
using Alisto.Api.Models;
using Alisto.Api.DTOs;
using Alisto.Api.Services;

namespace Alisto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TouristSpotController : ControllerBase
    {
        private readonly AlistoDbContext _context;
        private readonly ILogger<TouristSpotController> _logger;
        private readonly ILocalFileUploadService _fileUploadService;

        public TouristSpotController(AlistoDbContext context, ILogger<TouristSpotController> logger, ILocalFileUploadService fileUploadService)
        {
            _context = context;
            _logger = logger;
            _fileUploadService = fileUploadService;
        }

        // GET: api/touristspot
        [HttpGet]
        public async Task<IActionResult> GetTouristSpots([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
            [FromQuery] bool? isActive = null, [FromQuery] string? category = null)
        {
            try
            {
                var query = _context.TouristSpots.AsQueryable();

                // Apply filters
                if (isActive.HasValue)
                    query = query.Where(t => t.IsActive == isActive.Value);

                // Note: TouristSpot model doesn't have Category property, removing this filter

                var totalCount = await query.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var touristSpots = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(t => new TouristSpotDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Description = t.Description,
                        Address = t.Address,
                        Coordinates = t.Coordinates,
                        OpeningHours = t.OpeningHours,
                        EntryFee = t.EntryFee,
                        Rating = t.Rating,
                        ImageUrl = t.ImageUrl,
                        Location = t.Location,
                        TravelTime = t.TravelTime,
                        ViewCount = t.ViewCount,
                        Highlights = t.HighlightsList
                    })
                    .ToListAsync();

                var response = new ApiResponse<List<TouristSpotDto>>
                {
                    Success = true,
                    Data = touristSpots,
                    Message = $"Retrieved {touristSpots.Count} tourist spots successfully"
                };

                Response.Headers["X-Total-Count"] = totalCount.ToString();
                Response.Headers["X-Total-Pages"] = totalPages.ToString();
                Response.Headers["X-Current-Page"] = page.ToString();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tourist spots");
                return StatusCode(500, new ApiResponse<List<TouristSpotDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving tourist spots",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/touristspot/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTouristSpot(string id)
        {
            try
            {
                var touristSpot = await _context.TouristSpots.FindAsync(id);

                if (touristSpot == null)
                {
                    return NotFound(new ApiResponse<TouristSpotDto>
                    {
                        Success = false,
                        Message = "Tourist spot not found"
                    });
                }

                var touristSpotDto = new TouristSpotDto
                {
                    Id = touristSpot.Id,
                    Name = touristSpot.Name,
                    Description = touristSpot.Description,
                    Address = touristSpot.Address,
                    Coordinates = touristSpot.Coordinates,
                    OpeningHours = touristSpot.OpeningHours,
                    EntryFee = touristSpot.EntryFee,
                    Rating = touristSpot.Rating,
                    ImageUrl = touristSpot.ImageUrl,
                    Location = touristSpot.Location,
                    TravelTime = touristSpot.TravelTime,
                    ViewCount = touristSpot.ViewCount,
                    Highlights = touristSpot.HighlightsList
                };

                return Ok(new ApiResponse<TouristSpotDto>
                {
                    Success = true,
                    Data = touristSpotDto,
                    Message = "Tourist spot retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tourist spot with ID: {TouristSpotId}", id);
                return StatusCode(500, new ApiResponse<TouristSpotDto>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the tourist spot",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // POST: api/touristspot
        [HttpPost]
        public async Task<IActionResult> CreateTouristSpot([FromBody] CreateTouristSpotRequest request)
        {
            try
            {
                var touristSpot = new TouristSpot
                {
                    Name = request.Name,
                    Description = request.Description,
                    ImageUrl = request.ImageUrl,
                    Rating = request.Rating,
                    Location = request.Location,
                    Coordinates = request.Coordinates,
                    Address = request.Address,
                    OpeningHours = request.OpeningHours,
                    EntryFee = request.EntryFee,
                    Highlights = System.Text.Json.JsonSerializer.Serialize(request.Highlights),
                    TravelTime = request.TravelTime,
                    IsActive = request.IsActive,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.TouristSpots.Add(touristSpot);
                await _context.SaveChangesAsync();

                var touristSpotDto = new TouristSpotDto
                {
                    Id = touristSpot.Id,
                    Name = touristSpot.Name,
                    Description = touristSpot.Description,
                    ImageUrl = touristSpot.ImageUrl,
                    Rating = touristSpot.Rating,
                    Location = touristSpot.Location,
                    Coordinates = touristSpot.Coordinates,
                    Address = touristSpot.Address,
                    OpeningHours = touristSpot.OpeningHours,
                    EntryFee = touristSpot.EntryFee,
                    Highlights = touristSpot.HighlightsList,
                    TravelTime = touristSpot.TravelTime,
                    IsActive = touristSpot.IsActive,
                    ViewCount = touristSpot.ViewCount,
                    CreatedAt = touristSpot.CreatedAt,
                    UpdatedAt = touristSpot.UpdatedAt
                };

                return CreatedAtAction(nameof(GetTouristSpot), new { id = touristSpot.Id }, new ApiResponse<TouristSpotDto>
                {
                    Success = true,
                    Data = touristSpotDto,
                    Message = "Tourist spot created successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating tourist spot");
                return StatusCode(500, new ApiResponse<TouristSpotDto>
                {
                    Success = false,
                    Message = "An error occurred while creating the tourist spot",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // POST: api/touristspot/with-image
        [HttpPost("with-image")]
        public async Task<IActionResult> CreateTouristSpotWithImage([FromForm] CreateTouristSpotWithImageRequest request)
        {
            try
            {
                string? imageUrl = null;

                // Upload image to local directory if provided
                if (request.Image != null)
                {
                    try
                    {
                        imageUrl = await _fileUploadService.UploadFileAsync(request.Image, "tourist-spots");
                        
                        if (imageUrl == null)
                        {
                            _logger.LogWarning("Failed to upload image for tourist spot");
                            // Continue with creation even if image upload fails
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error uploading image for tourist spot");
                        // Continue with creation even if image upload fails
                    }
                }

                var touristSpot = new TouristSpot
                {
                    Name = request.Name,
                    Description = request.Description,
                    ImageUrl = imageUrl ?? string.Empty, // Use uploaded image URL or empty string
                    Rating = request.Rating,
                    Location = request.Location,
                    Coordinates = request.Coordinates,
                    Address = request.Address,
                    OpeningHours = request.OpeningHours,
                    EntryFee = request.EntryFee,
                    Highlights = System.Text.Json.JsonSerializer.Serialize(request.Highlights),
                    TravelTime = request.TravelTime,
                    IsActive = request.IsActive,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.TouristSpots.Add(touristSpot);
                await _context.SaveChangesAsync();

                var touristSpotDto = new TouristSpotDto
                {
                    Id = touristSpot.Id,
                    Name = touristSpot.Name,
                    Description = touristSpot.Description,
                    ImageUrl = touristSpot.ImageUrl,
                    Rating = touristSpot.Rating,
                    Location = touristSpot.Location,
                    Coordinates = touristSpot.Coordinates,
                    Address = touristSpot.Address,
                    OpeningHours = touristSpot.OpeningHours,
                    EntryFee = touristSpot.EntryFee,
                    Highlights = touristSpot.HighlightsList,
                    TravelTime = touristSpot.TravelTime,
                    IsActive = touristSpot.IsActive,
                    ViewCount = touristSpot.ViewCount,
                    CreatedAt = touristSpot.CreatedAt,
                    UpdatedAt = touristSpot.UpdatedAt
                };

                return CreatedAtAction(nameof(GetTouristSpot), new { id = touristSpot.Id }, new ApiResponse<TouristSpotDto>
                {
                    Success = true,
                    Data = touristSpotDto,
                    Message = "Tourist spot created successfully" + (imageUrl != null ? " with image" : "")
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating tourist spot with image");
                return StatusCode(500, new ApiResponse<TouristSpotDto>
                {
                    Success = false,
                    Message = "An error occurred while creating the tourist spot",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // PUT: api/touristspot/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTouristSpot(int id, [FromBody] UpdateTouristSpotRequest request)
        {
            try
            {
                var touristSpot = await _context.TouristSpots.FindAsync(id);

                if (touristSpot == null)
                {
                    return NotFound(new ApiResponse<TouristSpotDto>
                    {
                        Success = false,
                        Message = "Tourist spot not found"
                    });
                }

                // Update tourist spot properties
                touristSpot.Name = request.Name;
                touristSpot.Description = request.Description;
                touristSpot.ImageUrl = request.ImageUrl;
                touristSpot.Rating = request.Rating;
                touristSpot.Location = request.Location;
                touristSpot.Coordinates = request.Coordinates;
                touristSpot.Address = request.Address;
                touristSpot.OpeningHours = request.OpeningHours;
                touristSpot.EntryFee = request.EntryFee;
                touristSpot.Highlights = System.Text.Json.JsonSerializer.Serialize(request.Highlights);
                touristSpot.TravelTime = request.TravelTime;
                touristSpot.IsActive = request.IsActive;
                touristSpot.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var touristSpotDto = new TouristSpotDto
                {
                    Id = touristSpot.Id,
                    Name = touristSpot.Name,
                    Description = touristSpot.Description,
                    ImageUrl = touristSpot.ImageUrl,
                    Rating = touristSpot.Rating,
                    Location = touristSpot.Location,
                    Coordinates = touristSpot.Coordinates,
                    Address = touristSpot.Address,
                    OpeningHours = touristSpot.OpeningHours,
                    EntryFee = touristSpot.EntryFee,
                    Highlights = touristSpot.HighlightsList,
                    TravelTime = touristSpot.TravelTime,
                    IsActive = touristSpot.IsActive,
                    ViewCount = touristSpot.ViewCount,
                    CreatedAt = touristSpot.CreatedAt,
                    UpdatedAt = touristSpot.UpdatedAt
                };

                return Ok(new ApiResponse<TouristSpotDto>
                {
                    Success = true,
                    Data = touristSpotDto,
                    Message = "Tourist spot updated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating tourist spot with ID: {TouristSpotId}", id);
                return StatusCode(500, new ApiResponse<TouristSpotDto>
                {
                    Success = false,
                    Message = "An error occurred while updating the tourist spot",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // PUT: api/touristspot/{id}/with-image
        [HttpPut("{id}/with-image")]
        public async Task<IActionResult> UpdateTouristSpotWithImage(int id, [FromForm] UpdateTouristSpotWithImageRequest request)
        {
            try
            {
                var touristSpot = await _context.TouristSpots.FindAsync(id);

                if (touristSpot == null)
                {
                    return NotFound(new ApiResponse<TouristSpotDto>
                    {
                        Success = false,
                        Message = "Tourist spot not found"
                    });
                }

                string? imageUrl = null;

                // Upload new image if provided
                if (request.Image != null)
                {
                    try
                    {
                        imageUrl = await _fileUploadService.UploadFileAsync(request.Image, "tourist-spots");
                        
                        if (imageUrl == null)
                        {
                            _logger.LogWarning("Failed to upload image for tourist spot {TouristSpotId}", id);
                            // Continue with update even if image upload fails
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error uploading image for tourist spot {TouristSpotId}", id);
                        // Continue with update even if image upload fails
                    }
                }

                // Update tourist spot properties
                touristSpot.Name = request.Name;
                touristSpot.Description = request.Description;
                
                // Update image URL only if new image was uploaded successfully
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    // Delete old image if it exists
                    if (!string.IsNullOrEmpty(touristSpot.ImageUrl))
                    {
                        try
                        {
                            await _fileUploadService.DeleteFileAsync(touristSpot.ImageUrl);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "Failed to delete old image for tourist spot {TouristSpotId}", id);
                        }
                    }
                    touristSpot.ImageUrl = imageUrl;
                }
                
                touristSpot.Rating = request.Rating;
                touristSpot.Location = request.Location;
                touristSpot.Coordinates = request.Coordinates;
                touristSpot.Address = request.Address;
                touristSpot.OpeningHours = request.OpeningHours;
                touristSpot.EntryFee = request.EntryFee;
                touristSpot.Highlights = System.Text.Json.JsonSerializer.Serialize(request.Highlights);
                touristSpot.TravelTime = request.TravelTime;
                touristSpot.IsActive = request.IsActive;
                touristSpot.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var touristSpotDto = new TouristSpotDto
                {
                    Id = touristSpot.Id,
                    Name = touristSpot.Name,
                    Description = touristSpot.Description,
                    ImageUrl = touristSpot.ImageUrl,
                    Rating = touristSpot.Rating,
                    Location = touristSpot.Location,
                    Coordinates = touristSpot.Coordinates,
                    Address = touristSpot.Address,
                    OpeningHours = touristSpot.OpeningHours,
                    EntryFee = touristSpot.EntryFee,
                    Highlights = touristSpot.HighlightsList,
                    TravelTime = touristSpot.TravelTime,
                    IsActive = touristSpot.IsActive,
                    ViewCount = touristSpot.ViewCount,
                    CreatedAt = touristSpot.CreatedAt,
                    UpdatedAt = touristSpot.UpdatedAt
                };

                return Ok(new ApiResponse<TouristSpotDto>
                {
                    Success = true,
                    Data = touristSpotDto,
                    Message = "Tourist spot updated successfully" + (imageUrl != null ? " with new image" : "")
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating tourist spot with ID: {TouristSpotId}", id);
                return StatusCode(500, new ApiResponse<TouristSpotDto>
                {
                    Success = false,
                    Message = "An error occurred while updating the tourist spot",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // DELETE: api/touristspot/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTouristSpot(int id)
        {
            try
            {
                var touristSpot = await _context.TouristSpots.FindAsync(id);

                if (touristSpot == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Tourist spot not found"
                    });
                }

                _context.TouristSpots.Remove(touristSpot);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Tourist spot deleted successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting tourist spot with ID: {TouristSpotId}", id);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while deleting the tourist spot",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // PATCH: api/touristspot/{id}/activate
        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> ActivateTouristSpot(int id)
        {
            try
            {
                var touristSpot = await _context.TouristSpots.FindAsync(id);

                if (touristSpot == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Tourist spot not found"
                    });
                }

                touristSpot.IsActive = true;
                touristSpot.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Tourist spot activated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error activating tourist spot with ID: {TouristSpotId}", id);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while activating the tourist spot",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // PATCH: api/touristspot/{id}/deactivate
        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> DeactivateTouristSpot(int id)
        {
            try
            {
                var touristSpot = await _context.TouristSpots.FindAsync(id);

                if (touristSpot == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Tourist spot not found"
                    });
                }

                touristSpot.IsActive = false;
                touristSpot.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Tourist spot deactivated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deactivating tourist spot with ID: {TouristSpotId}", id);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while deactivating the tourist spot",
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
} 