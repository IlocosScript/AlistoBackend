using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alisto.Api.Data;
using Alisto.Api.Models;
using Alisto.Api.DTOs;

namespace Alisto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityServiceController : ControllerBase
    {
        private readonly AlistoDbContext _context;
        private readonly ILogger<CityServiceController> _logger;

        public CityServiceController(AlistoDbContext context, ILogger<CityServiceController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/cityservice
        [HttpGet]
        public async Task<IActionResult> GetCityServices([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
            [FromQuery] int? categoryId = null, [FromQuery] bool? isActive = null)
        {
            try
            {
                var query = _context.CityServices
                    .Include(cs => cs.Category)
                    .AsQueryable();

                // Apply filters
                if (categoryId.HasValue)
                    query = query.Where(cs => cs.CategoryId == categoryId.Value);

                if (isActive.HasValue)
                    query = query.Where(cs => cs.IsActive == isActive.Value);

                var totalCount = await query.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var cityServices = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(cs => new CityServiceDto
                    {
                        Id = cs.Id,
                        Name = cs.Name,
                        Description = cs.Description,
                        CategoryId = cs.CategoryId,
                        CategoryName = cs.Category.Name,
                        Fee = cs.Fee,
                        ProcessingTime = cs.ProcessingTime,
                        RequiredDocuments = cs.RequiredDocumentsList,
                        IsActive = cs.IsActive,
                        CreatedAt = cs.CreatedAt,
                        UpdatedAt = cs.UpdatedAt
                    })
                    .ToListAsync();

                var response = new ApiResponse<List<CityServiceDto>>
                {
                    Success = true,
                    Data = cityServices,
                    Message = $"Retrieved {cityServices.Count} city services successfully"
                };

                Response.Headers["X-Total-Count"] = totalCount.ToString();
                Response.Headers["X-Total-Pages"] = totalPages.ToString();
                Response.Headers["X-Current-Page"] = page.ToString();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving city services");
                return StatusCode(500, new ApiResponse<List<CityServiceDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving city services",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/cityservice/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityService(int id)
        {
            try
            {
                var cityService = await _context.CityServices
                    .Include(cs => cs.Category)
                    .FirstOrDefaultAsync(cs => cs.Id == id);

                if (cityService == null)
                {
                    return NotFound(new ApiResponse<CityServiceDto>
                    {
                        Success = false,
                        Message = "City service not found"
                    });
                }

                var cityServiceDto = new CityServiceDto
                {
                    Id = cityService.Id,
                    Name = cityService.Name,
                    Description = cityService.Description,
                    CategoryId = cityService.CategoryId,
                    CategoryName = cityService.Category.Name,
                    Fee = cityService.Fee,
                    ProcessingTime = cityService.ProcessingTime,
                                            RequiredDocuments = cityService.RequiredDocumentsList,
                    IsActive = cityService.IsActive,
                    CreatedAt = cityService.CreatedAt,
                    UpdatedAt = cityService.UpdatedAt
                };

                return Ok(new ApiResponse<CityServiceDto>
                {
                    Success = true,
                    Data = cityServiceDto,
                    Message = "City service retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving city service with ID: {CityServiceId}", id);
                return StatusCode(500, new ApiResponse<CityServiceDto>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the city service",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/cityservice/categories
        [HttpGet("categories")]
        public async Task<IActionResult> GetServiceCategories()
        {
            try
            {
                var categories = await _context.ServiceCategories
                    .Select(sc => new ServiceCategoryDto
                    {
                        Id = sc.Id,
                        Name = sc.Name,
                        Description = sc.Description,
                        IconName = sc.IconName,
                        IsActive = sc.IsActive,
                        SortOrder = sc.SortOrder
                    })
                    .ToListAsync();

                return Ok(new ApiResponse<List<ServiceCategoryDto>>
                {
                    Success = true,
                    Data = categories,
                    Message = $"Retrieved {categories.Count} service categories successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service categories");
                return StatusCode(500, new ApiResponse<List<ServiceCategoryDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving service categories",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/cityservice/categories/{id}
        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetServiceCategory(int id)
        {
            try
            {
                var category = await _context.ServiceCategories.FindAsync(id);

                if (category == null)
                {
                    return NotFound(new ApiResponse<ServiceCategoryDto>
                    {
                        Success = false,
                        Message = "Service category not found"
                    });
                }

                var categoryDto = new ServiceCategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    IconName = category.IconName,
                    IsActive = category.IsActive,
                    SortOrder = category.SortOrder
                };

                return Ok(new ApiResponse<ServiceCategoryDto>
                {
                    Success = true,
                    Data = categoryDto,
                    Message = "Service category retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service category with ID: {CategoryId}", id);
                return StatusCode(500, new ApiResponse<ServiceCategoryDto>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the service category",
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
} 