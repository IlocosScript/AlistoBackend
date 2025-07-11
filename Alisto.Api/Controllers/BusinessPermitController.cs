using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alisto.Api.Data;
using Alisto.Api.Models;
using Alisto.Api.DTOs;
using Alisto.Api.Enums;

namespace Alisto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessPermitController : ControllerBase
    {
        private readonly AlistoDbContext _context;
        private readonly ILogger<BusinessPermitController> _logger;

        public BusinessPermitController(AlistoDbContext context, ILogger<BusinessPermitController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/businesspermit
        [HttpGet]
        public async Task<IActionResult> GetBusinessPermits([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
            [FromQuery] string? userId = null, [FromQuery] BusinessServiceType? serviceType = null,
            [FromQuery] AppointmentStatus? status = null)
        {
            try
            {
                var query = _context.BusinessPermitRequests
                    .Include(bpr => bpr.Appointment)
                    .ThenInclude(a => a.User)
                    .AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(userId))
                    query = query.Where(bpr => bpr.Appointment.UserId == userId);

                if (serviceType.HasValue)
                    query = query.Where(bpr => bpr.ServiceType == serviceType.Value);

                if (status.HasValue)
                    query = query.Where(bpr => bpr.Appointment.Status == status.Value);

                var totalCount = await query.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var businessPermits = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(bpr => new BusinessPermitRequestDto
                    {
                        AppointmentId = bpr.AppointmentId,
                        UserId = bpr.Appointment.UserId,
                        UserName = $"{bpr.Appointment.User.FirstName} {bpr.Appointment.User.LastName}",
                        BusinessName = bpr.BusinessName,
                        BusinessType = bpr.BusinessType,
                        ServiceType = bpr.ServiceType,
                        BusinessAddress = bpr.BusinessAddress,
                        OwnerName = bpr.OwnerName,
                        TinNumber = bpr.TinNumber,
                        CapitalInvestment = bpr.CapitalInvestment,
                        Status = bpr.Appointment.Status,
                        CreatedAt = bpr.Appointment.CreatedAt,
                        UpdatedAt = bpr.Appointment.UpdatedAt
                    })
                    .ToListAsync();

                var response = new ApiResponse<List<BusinessPermitRequestDto>>
                {
                    Success = true,
                    Data = businessPermits,
                    Message = $"Retrieved {businessPermits.Count} business permit requests successfully"
                };

                Response.Headers["X-Total-Count"] = totalCount.ToString();
                Response.Headers["X-Total-Pages"] = totalPages.ToString();
                Response.Headers["X-Current-Page"] = page.ToString();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving business permit requests");
                return StatusCode(500, new ApiResponse<List<BusinessPermitRequestDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving business permit requests",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/businesspermit/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusinessPermit(string id)
        {
            try
            {
                var businessPermit = await _context.BusinessPermitRequests
                    .Include(bpr => bpr.Appointment)
                    .ThenInclude(a => a.User)
                    .FirstOrDefaultAsync(bpr => bpr.AppointmentId == id);

                if (businessPermit == null)
                {
                    return NotFound(new ApiResponse<BusinessPermitRequestDto>
                    {
                        Success = false,
                        Message = "Business permit request not found"
                    });
                }

                var businessPermitDto = new BusinessPermitRequestDto
                {
                    AppointmentId = businessPermit.AppointmentId,
                    UserId = businessPermit.Appointment.UserId,
                    UserName = $"{businessPermit.Appointment.User.FirstName} {businessPermit.Appointment.User.LastName}",
                    BusinessName = businessPermit.BusinessName,
                    BusinessType = businessPermit.BusinessType,
                    ServiceType = businessPermit.ServiceType,
                    BusinessAddress = businessPermit.BusinessAddress,
                    OwnerName = businessPermit.OwnerName,
                    TinNumber = businessPermit.TinNumber,
                    CapitalInvestment = businessPermit.CapitalInvestment,
                    Status = businessPermit.Appointment.Status,
                    CreatedAt = businessPermit.Appointment.CreatedAt,
                    UpdatedAt = businessPermit.Appointment.UpdatedAt
                };

                return Ok(new ApiResponse<BusinessPermitRequestDto>
                {
                    Success = true,
                    Data = businessPermitDto,
                    Message = "Business permit request retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving business permit request with ID: {BusinessPermitId}", id);
                return StatusCode(500, new ApiResponse<BusinessPermitRequestDto>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the business permit request",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // POST: api/businesspermit
        [HttpPost]
        public async Task<IActionResult> CreateBusinessPermit([FromBody] CreateBusinessPermitRequest request)
        {
            try
            {
                var businessPermit = new BusinessPermitRequest
                {
                    AppointmentId = request.AppointmentId,
                    BusinessName = request.BusinessName,
                    BusinessType = request.BusinessType,
                    ServiceType = request.ServiceType,
                    BusinessAddress = request.BusinessAddress,
                    OwnerName = request.OwnerName,
                    TinNumber = request.TinNumber,
                    CapitalInvestment = request.CapitalInvestment
                };

                _context.BusinessPermitRequests.Add(businessPermit);
                await _context.SaveChangesAsync();

                var businessPermitDto = new BusinessPermitRequestDto
                {
                    AppointmentId = businessPermit.AppointmentId,
                    BusinessName = businessPermit.BusinessName,
                    BusinessType = businessPermit.BusinessType,
                    ServiceType = businessPermit.ServiceType,
                    BusinessAddress = businessPermit.BusinessAddress,
                    OwnerName = businessPermit.OwnerName,
                    TinNumber = businessPermit.TinNumber,
                    CapitalInvestment = businessPermit.CapitalInvestment
                };

                return CreatedAtAction(nameof(GetBusinessPermit), new { id = businessPermit.AppointmentId }, new ApiResponse<BusinessPermitRequestDto>
                {
                    Success = true,
                    Data = businessPermitDto,
                    Message = "Business permit request created successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating business permit request");
                return StatusCode(500, new ApiResponse<BusinessPermitRequestDto>
                {
                    Success = false,
                    Message = "An error occurred while creating the business permit request",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // PUT: api/businesspermit/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBusinessPermit(string id, [FromBody] CreateBusinessPermitRequest request)
        {
            try
            {
                var businessPermit = await _context.BusinessPermitRequests.FindAsync(id);

                if (businessPermit == null)
                {
                    return NotFound(new ApiResponse<BusinessPermitRequestDto>
                    {
                        Success = false,
                        Message = "Business permit request not found"
                    });
                }

                // Update business permit properties
                businessPermit.BusinessName = request.BusinessName;
                businessPermit.BusinessType = request.BusinessType;
                businessPermit.ServiceType = request.ServiceType;
                businessPermit.BusinessAddress = request.BusinessAddress;
                businessPermit.OwnerName = request.OwnerName;
                businessPermit.TinNumber = request.TinNumber;
                businessPermit.CapitalInvestment = request.CapitalInvestment;

                await _context.SaveChangesAsync();

                var businessPermitDto = new BusinessPermitRequestDto
                {
                    AppointmentId = businessPermit.AppointmentId,
                    BusinessName = businessPermit.BusinessName,
                    BusinessType = businessPermit.BusinessType,
                    ServiceType = businessPermit.ServiceType,
                    BusinessAddress = businessPermit.BusinessAddress,
                    OwnerName = businessPermit.OwnerName,
                    TinNumber = businessPermit.TinNumber,
                    CapitalInvestment = businessPermit.CapitalInvestment
                };

                return Ok(new ApiResponse<BusinessPermitRequestDto>
                {
                    Success = true,
                    Data = businessPermitDto,
                    Message = "Business permit request updated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating business permit request with ID: {BusinessPermitId}", id);
                return StatusCode(500, new ApiResponse<BusinessPermitRequestDto>
                {
                    Success = false,
                    Message = "An error occurred while updating the business permit request",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // DELETE: api/businesspermit/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusinessPermit(string id)
        {
            try
            {
                var businessPermit = await _context.BusinessPermitRequests.FindAsync(id);

                if (businessPermit == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Business permit request not found"
                    });
                }

                _context.BusinessPermitRequests.Remove(businessPermit);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Business permit request deleted successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting business permit request with ID: {BusinessPermitId}", id);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while deleting the business permit request",
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
} 