using Microsoft.AspNetCore.Authorization;
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
    public class AppointmentController : ControllerBase
    {
        private readonly AlistoDbContext _context;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(AlistoDbContext context, ILogger<AppointmentController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/appointment
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAppointments([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
            [FromQuery] string? status = null, [FromQuery] string? paymentStatus = null, [FromQuery] string? userId = null)
        {
            try
            {
                var query = _context.Appointments
                    .Include(a => a.Service)
                    .Include(a => a.Service.Category)
                    .AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(status))
                {
                    if (Enum.TryParse<AppointmentStatus>(status, true, out var statusEnum))
                    {
                        query = query.Where(a => a.Status == statusEnum);
                    }
                }

                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    if (Enum.TryParse<PaymentStatus>(paymentStatus, true, out var paymentStatusEnum))
                    {
                        query = query.Where(a => a.PaymentStatus == paymentStatusEnum);
                    }
                }

                if (!string.IsNullOrEmpty(userId))
                {
                    query = query.Where(a => a.UserId == userId);
                }

                var totalCount = await query.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var appointments = await query
                    .OrderByDescending(a => a.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(a => new AppointmentDto
                    {
                        Id = a.Id,
                        ReferenceNumber = a.ReferenceNumber,
                        AppointmentDate = a.AppointmentDate,
                        AppointmentTime = a.AppointmentTime,
                        Status = a.Status.ToString(),
                        TotalFee = a.TotalFee,
                        PaymentStatus = a.PaymentStatus.ToString(),
                        ServiceName = a.Service.Name,
                        ServiceCategory = a.Service.Category.Name,
                        ApplicantFirstName = a.ApplicantFirstName,
                        ApplicantLastName = a.ApplicantLastName,
                        ApplicantContactNumber = a.ApplicantContactNumber,
                        CreatedAt = a.CreatedAt,
                        CompletedAt = a.CompletedAt
                    })
                    .ToListAsync();

                var response = new ApiResponse<List<AppointmentDto>>
                {
                    Success = true,
                    Data = appointments,
                    Message = $"Retrieved {appointments.Count} appointments successfully"
                };

                Response.Headers["X-Total-Count"] = totalCount.ToString();
                Response.Headers["X-Total-Pages"] = totalPages.ToString();
                Response.Headers["X-Current-Page"] = page.ToString();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving appointments");
                return StatusCode(500, new ApiResponse<List<AppointmentDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving appointments",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/appointment/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAppointment(string id)
        {
            try
            {
                var appointment = await _context.Appointments
                    .Include(a => a.Service)
                    .Include(a => a.Service.Category)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (appointment == null)
                {
                    return NotFound(new ApiResponse<AppointmentDto>
                    {
                        Success = false,
                        Message = "Appointment not found"
                    });
                }

                var appointmentDto = new AppointmentDto
                {
                    Id = appointment.Id,
                    ReferenceNumber = appointment.ReferenceNumber,
                    AppointmentDate = appointment.AppointmentDate,
                    AppointmentTime = appointment.AppointmentTime,
                    Status = appointment.Status.ToString(),
                    TotalFee = appointment.TotalFee,
                    PaymentStatus = appointment.PaymentStatus.ToString(),
                    ServiceName = appointment.Service.Name,
                    ServiceCategory = appointment.Service.Category.Name,
                    ApplicantFirstName = appointment.ApplicantFirstName,
                    ApplicantLastName = appointment.ApplicantLastName,
                    ApplicantContactNumber = appointment.ApplicantContactNumber,
                    CreatedAt = appointment.CreatedAt,
                    CompletedAt = appointment.CompletedAt
                };

                return Ok(new ApiResponse<AppointmentDto>
                {
                    Success = true,
                    Data = appointmentDto,
                    Message = "Appointment retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving appointment with ID: {AppointmentId}", id);
                return StatusCode(500, new ApiResponse<AppointmentDto>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the appointment",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // POST: api/appointment
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequest request)
        {
            try
            {
                // Validate service exists
                var service = await _context.CityServices.FindAsync(request.ServiceId);
                if (service == null)
                {
                    return BadRequest(new ApiResponse<AppointmentDto>
                    {
                        Success = false,
                        Message = "Service not found"
                    });
                }

                // Generate reference number
                var referenceNumber = $"APT-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

                var appointment = new Appointment
                {
                    UserId = request.UserId,
                    ServiceId = request.ServiceId,
                    ReferenceNumber = referenceNumber,
                    AppointmentDate = request.AppointmentDate,
                    AppointmentTime = request.AppointmentTime,
                    Status = AppointmentStatus.Pending,
                    TotalFee = service.Fee,
                    PaymentStatus = PaymentStatus.Pending,
                    Notes = request.Notes,
                    ApplicantFirstName = request.ApplicantFirstName,
                    ApplicantLastName = request.ApplicantLastName,
                    ApplicantMiddleName = request.ApplicantMiddleName,
                    ApplicantContactNumber = request.ApplicantContactNumber,
                    ApplicantEmail = request.ApplicantEmail,
                    ApplicantAddress = request.ApplicantAddress,
                    ServiceSpecificData = request.ServiceSpecificData ?? "{}",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();

                var appointmentDto = new AppointmentDto
                {
                    Id = appointment.Id,
                    ReferenceNumber = appointment.ReferenceNumber,
                    AppointmentDate = appointment.AppointmentDate,
                    AppointmentTime = appointment.AppointmentTime,
                    Status = appointment.Status.ToString(),
                    TotalFee = appointment.TotalFee,
                    PaymentStatus = appointment.PaymentStatus.ToString(),
                    ServiceName = service.Name,
                    ServiceCategory = service.Category.Name,
                    ApplicantFirstName = appointment.ApplicantFirstName,
                    ApplicantLastName = appointment.ApplicantLastName,
                    ApplicantContactNumber = appointment.ApplicantContactNumber,
                    CreatedAt = appointment.CreatedAt,
                    CompletedAt = appointment.CompletedAt
                };

                return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, new ApiResponse<AppointmentDto>
                {
                    Success = true,
                    Data = appointmentDto,
                    Message = "Appointment created successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating appointment");
                return StatusCode(500, new ApiResponse<AppointmentDto>
                {
                    Success = false,
                    Message = "An error occurred while creating the appointment",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // PUT: api/appointment/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAppointment(string id, [FromBody] CreateAppointmentRequest request)
        {
            try
            {
                var appointment = await _context.Appointments.FindAsync(id);

                if (appointment == null)
                {
                    return NotFound(new ApiResponse<AppointmentDto>
                    {
                        Success = false,
                        Message = "Appointment not found"
                    });
                }

                // Only allow updates if appointment is still pending
                if (appointment.Status != AppointmentStatus.Pending)
                {
                    return BadRequest(new ApiResponse<AppointmentDto>
                    {
                        Success = false,
                        Message = "Cannot update appointment that is not in pending status"
                    });
                }

                // Update appointment properties
                appointment.AppointmentDate = request.AppointmentDate;
                appointment.AppointmentTime = request.AppointmentTime;
                appointment.Notes = request.Notes;
                appointment.ApplicantFirstName = request.ApplicantFirstName;
                appointment.ApplicantLastName = request.ApplicantLastName;
                appointment.ApplicantMiddleName = request.ApplicantMiddleName;
                appointment.ApplicantContactNumber = request.ApplicantContactNumber;
                appointment.ApplicantEmail = request.ApplicantEmail;
                appointment.ApplicantAddress = request.ApplicantAddress;
                appointment.ServiceSpecificData = request.ServiceSpecificData ?? "{}";
                appointment.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var service = await _context.CityServices.FindAsync(appointment.ServiceId);
                var appointmentDto = new AppointmentDto
                {
                    Id = appointment.Id,
                    ReferenceNumber = appointment.ReferenceNumber,
                    AppointmentDate = appointment.AppointmentDate,
                    AppointmentTime = appointment.AppointmentTime,
                    Status = appointment.Status.ToString(),
                    TotalFee = appointment.TotalFee,
                    PaymentStatus = appointment.PaymentStatus.ToString(),
                    ServiceName = service?.Name ?? "Unknown",
                    ServiceCategory = service?.Category?.Name ?? "Unknown",
                    ApplicantFirstName = appointment.ApplicantFirstName,
                    ApplicantLastName = appointment.ApplicantLastName,
                    ApplicantContactNumber = appointment.ApplicantContactNumber,
                    CreatedAt = appointment.CreatedAt,
                    CompletedAt = appointment.CompletedAt
                };

                return Ok(new ApiResponse<AppointmentDto>
                {
                    Success = true,
                    Data = appointmentDto,
                    Message = "Appointment updated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating appointment with ID: {AppointmentId}", id);
                return StatusCode(500, new ApiResponse<AppointmentDto>
                {
                    Success = false,
                    Message = "An error occurred while updating the appointment",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // DELETE: api/appointment/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> CancelAppointment(string id)
        {
            try
            {
                var appointment = await _context.Appointments.FindAsync(id);

                if (appointment == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Appointment not found"
                    });
                }

                // Only allow cancellation if appointment is pending or confirmed
                if (appointment.Status != AppointmentStatus.Pending && appointment.Status != AppointmentStatus.Confirmed)
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Cannot cancel appointment that is not in pending or confirmed status"
                    });
                }

                appointment.Status = AppointmentStatus.Cancelled;
                appointment.CancelledAt = DateTime.UtcNow;
                appointment.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Appointment cancelled successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling appointment with ID: {AppointmentId}", id);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while cancelling the appointment",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // PATCH: api/appointment/{id}/status
        [HttpPatch("{id}/status")]
        [Authorize]
        public async Task<IActionResult> UpdateAppointmentStatus(string id, [FromBody] UpdateAppointmentStatusRequest request)
        {
            try
            {
                var appointment = await _context.Appointments.FindAsync(id);

                if (appointment == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Appointment not found"
                    });
                }

                appointment.Status = request.Status;
                appointment.UpdatedAt = DateTime.UtcNow;

                // Set completion date if status is completed
                if (request.Status == AppointmentStatus.Completed)
                {
                    appointment.CompletedAt = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Appointment status updated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating appointment status with ID: {AppointmentId}", id);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while updating the appointment status",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/appointment/statuses
        [HttpGet("statuses")]
        public async Task<IActionResult> GetAppointmentStatuses()
        {
            try
            {
                var statuses = Enum.GetValues<AppointmentStatus>()
                    .Select(s => new { Value = s.ToString(), Label = s.ToString().Replace("_", " ") })
                    .ToList();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Data = statuses,
                    Message = "Appointment statuses retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving appointment statuses");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving appointment statuses",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/appointment/payment-statuses
        [HttpGet("payment-statuses")]
        public async Task<IActionResult> GetPaymentStatuses()
        {
            try
            {
                var paymentStatuses = Enum.GetValues<PaymentStatus>()
                    .Select(p => new { Value = p.ToString(), Label = p.ToString().Replace("_", " ") })
                    .ToList();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Data = paymentStatuses,
                    Message = "Payment statuses retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payment statuses");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving payment statuses",
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
} 