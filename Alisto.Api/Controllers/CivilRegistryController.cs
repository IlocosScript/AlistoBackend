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
    public class CivilRegistryController : ControllerBase
    {
        private readonly AlistoDbContext _context;
        private readonly ILogger<CivilRegistryController> _logger;

        public CivilRegistryController(AlistoDbContext context, ILogger<CivilRegistryController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/civilregistry
        [HttpGet]
        public async Task<IActionResult> GetCivilRegistryRequests([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
            [FromQuery] string? userId = null, [FromQuery] CivilDocumentType? documentType = null,
            [FromQuery] AppointmentStatus? status = null)
        {
            try
            {
                var query = _context.CivilRegistryRequests
                    .Include(crr => crr.Appointment)
                    .ThenInclude(a => a.User)
                    .AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(userId))
                    query = query.Where(crr => crr.Appointment.UserId == userId);

                if (documentType.HasValue)
                    query = query.Where(crr => crr.DocumentType == documentType.Value);

                if (status.HasValue)
                    query = query.Where(crr => crr.Appointment.Status == status.Value);

                var totalCount = await query.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var civilRegistryRequests = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(crr => new CivilRegistryRequestDto
                    {
                        AppointmentId = crr.AppointmentId,
                        UserId = crr.Appointment.UserId,
                        UserName = $"{crr.Appointment.User.FirstName} {crr.Appointment.User.LastName}",
                        DocumentType = crr.DocumentType,
                        Purpose = crr.Purpose,
                        NumberOfCopies = crr.NumberOfCopies,
                        RegistryNumber = crr.RegistryNumber,
                        RegistryDate = crr.RegistryDate,
                        RegistryPlace = crr.RegistryPlace,
                        Status = crr.Appointment.Status,
                        CreatedAt = crr.Appointment.CreatedAt,
                        UpdatedAt = crr.Appointment.UpdatedAt
                    })
                    .ToListAsync();

                var response = new ApiResponse<List<CivilRegistryRequestDto>>
                {
                    Success = true,
                    Data = civilRegistryRequests,
                    Message = $"Retrieved {civilRegistryRequests.Count} civil registry requests successfully"
                };

                Response.Headers["X-Total-Count"] = totalCount.ToString();
                Response.Headers["X-Total-Pages"] = totalPages.ToString();
                Response.Headers["X-Current-Page"] = page.ToString();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving civil registry requests");
                return StatusCode(500, new ApiResponse<List<CivilRegistryRequestDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving civil registry requests",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/civilregistry/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCivilRegistryRequest(string id)
        {
            try
            {
                var civilRegistryRequest = await _context.CivilRegistryRequests
                    .Include(crr => crr.Appointment)
                    .ThenInclude(a => a.User)
                    .FirstOrDefaultAsync(crr => crr.AppointmentId == id);

                if (civilRegistryRequest == null)
                {
                    return NotFound(new ApiResponse<CivilRegistryRequestDto>
                    {
                        Success = false,
                        Message = "Civil registry request not found"
                    });
                }

                var civilRegistryRequestDto = new CivilRegistryRequestDto
                {
                    AppointmentId = civilRegistryRequest.AppointmentId,
                    UserId = civilRegistryRequest.Appointment.UserId,
                    UserName = $"{civilRegistryRequest.Appointment.User.FirstName} {civilRegistryRequest.Appointment.User.LastName}",
                    DocumentType = civilRegistryRequest.DocumentType,
                    Purpose = civilRegistryRequest.Purpose,
                    NumberOfCopies = civilRegistryRequest.NumberOfCopies,
                    RegistryNumber = civilRegistryRequest.RegistryNumber,
                    RegistryDate = civilRegistryRequest.RegistryDate,
                    RegistryPlace = civilRegistryRequest.RegistryPlace,
                    Status = civilRegistryRequest.Appointment.Status,
                    CreatedAt = civilRegistryRequest.Appointment.CreatedAt,
                    UpdatedAt = civilRegistryRequest.Appointment.UpdatedAt
                };

                return Ok(new ApiResponse<CivilRegistryRequestDto>
                {
                    Success = true,
                    Data = civilRegistryRequestDto,
                    Message = "Civil registry request retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving civil registry request with ID: {CivilRegistryRequestId}", id);
                return StatusCode(500, new ApiResponse<CivilRegistryRequestDto>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the civil registry request",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // POST: api/civilregistry
        [HttpPost]
        public async Task<IActionResult> CreateCivilRegistryRequest([FromBody] CreateCivilRegistryRequest request)
        {
            try
            {
                var civilRegistryRequest = new CivilRegistryRequest
                {
                    AppointmentId = request.AppointmentId,
                    DocumentType = request.DocumentType,
                    Purpose = request.Purpose,
                    NumberOfCopies = request.NumberOfCopies,
                    RegistryNumber = request.RegistryNumber,
                    RegistryDate = request.RegistryDate,
                    RegistryPlace = request.RegistryPlace
                };

                _context.CivilRegistryRequests.Add(civilRegistryRequest);
                await _context.SaveChangesAsync();

                var civilRegistryRequestDto = new CivilRegistryRequestDto
                {
                    AppointmentId = civilRegistryRequest.AppointmentId,
                    DocumentType = civilRegistryRequest.DocumentType,
                    Purpose = civilRegistryRequest.Purpose,
                    NumberOfCopies = civilRegistryRequest.NumberOfCopies,
                    RegistryNumber = civilRegistryRequest.RegistryNumber,
                    RegistryDate = civilRegistryRequest.RegistryDate,
                    RegistryPlace = civilRegistryRequest.RegistryPlace
                };

                return CreatedAtAction(nameof(GetCivilRegistryRequest), new { id = civilRegistryRequest.AppointmentId }, new ApiResponse<CivilRegistryRequestDto>
                {
                    Success = true,
                    Data = civilRegistryRequestDto,
                    Message = "Civil registry request created successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating civil registry request");
                return StatusCode(500, new ApiResponse<CivilRegistryRequestDto>
                {
                    Success = false,
                    Message = "An error occurred while creating the civil registry request",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // PUT: api/civilregistry/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCivilRegistryRequest(string id, [FromBody] CreateCivilRegistryRequest request)
        {
            try
            {
                var civilRegistryRequest = await _context.CivilRegistryRequests.FindAsync(id);

                if (civilRegistryRequest == null)
                {
                    return NotFound(new ApiResponse<CivilRegistryRequestDto>
                    {
                        Success = false,
                        Message = "Civil registry request not found"
                    });
                }

                // Update civil registry request properties
                civilRegistryRequest.DocumentType = request.DocumentType;
                civilRegistryRequest.Purpose = request.Purpose;
                civilRegistryRequest.NumberOfCopies = request.NumberOfCopies;
                civilRegistryRequest.RegistryNumber = request.RegistryNumber;
                civilRegistryRequest.RegistryDate = request.RegistryDate;
                civilRegistryRequest.RegistryPlace = request.RegistryPlace;

                await _context.SaveChangesAsync();

                var civilRegistryRequestDto = new CivilRegistryRequestDto
                {
                    AppointmentId = civilRegistryRequest.AppointmentId,
                    DocumentType = civilRegistryRequest.DocumentType,
                    Purpose = civilRegistryRequest.Purpose,
                    NumberOfCopies = civilRegistryRequest.NumberOfCopies,
                    RegistryNumber = civilRegistryRequest.RegistryNumber,
                    RegistryDate = civilRegistryRequest.RegistryDate,
                    RegistryPlace = civilRegistryRequest.RegistryPlace
                };

                return Ok(new ApiResponse<CivilRegistryRequestDto>
                {
                    Success = true,
                    Data = civilRegistryRequestDto,
                    Message = "Civil registry request updated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating civil registry request with ID: {CivilRegistryRequestId}", id);
                return StatusCode(500, new ApiResponse<CivilRegistryRequestDto>
                {
                    Success = false,
                    Message = "An error occurred while updating the civil registry request",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // DELETE: api/civilregistry/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCivilRegistryRequest(string id)
        {
            try
            {
                var civilRegistryRequest = await _context.CivilRegistryRequests.FindAsync(id);

                if (civilRegistryRequest == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Civil registry request not found"
                    });
                }

                _context.CivilRegistryRequests.Remove(civilRegistryRequest);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Civil registry request deleted successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting civil registry request with ID: {CivilRegistryRequestId}", id);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while deleting the civil registry request",
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
} 