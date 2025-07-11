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
    public class IssueReportController : ControllerBase
    {
        private readonly AlistoDbContext _context;
        private readonly ILogger<IssueReportController> _logger;

        public IssueReportController(AlistoDbContext context, ILogger<IssueReportController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/issuereport
        [HttpGet]
        public async Task<IActionResult> GetIssueReports([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
            [FromQuery] string? category = null, [FromQuery] string? status = null, [FromQuery] string? priority = null)
        {
            try
            {
                var query = _context.IssueReports
                    .Include(i => i.Photos)
                    .AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(category))
                {
                    if (Enum.TryParse<IssueCategory>(category, true, out var categoryEnum))
                    {
                        query = query.Where(i => i.Category == categoryEnum);
                    }
                }

                if (!string.IsNullOrEmpty(status))
                {
                    if (Enum.TryParse<IssueStatus>(status, true, out var statusEnum))
                    {
                        query = query.Where(i => i.Status == statusEnum);
                    }
                }

                if (!string.IsNullOrEmpty(priority))
                {
                    if (Enum.TryParse<Priority>(priority, true, out var priorityEnum))
                    {
                        query = query.Where(i => i.Priority == priorityEnum);
                    }
                }

                // Only show publicly visible reports
                query = query.Where(i => i.PubliclyVisible);

                var totalCount = await query.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var issueReports = await query
                    .OrderByDescending(i => i.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(i => new IssueReportDto
                    {
                        Id = i.Id,
                        ReferenceNumber = i.ReferenceNumber,
                        Category = i.Category.ToString(),
                        UrgencyLevel = i.UrgencyLevel.ToString(),
                        Title = i.Title,
                        Description = i.Description,
                        Location = i.Location,
                        Coordinates = i.Coordinates,
                        Status = i.Status.ToString(),
                        Priority = i.Priority.ToString(),
                        AssignedDepartment = i.AssignedDepartment,
                        EstimatedResolution = i.EstimatedResolution,
                        CreatedAt = i.CreatedAt,
                        Photos = i.Photos.Select(p => new IssuePhotoDto
                        {
                            Id = p.Id,
                            PhotoUrl = p.PhotoUrl,
                            Caption = p.Caption,
                            UploadedAt = p.UploadedAt
                        }).ToList()
                    })
                    .ToListAsync();

                var response = new ApiResponse<List<IssueReportDto>>
                {
                    Success = true,
                    Data = issueReports,
                    Message = $"Retrieved {issueReports.Count} issue reports successfully"
                };

                Response.Headers["X-Total-Count"] = totalCount.ToString();
                Response.Headers["X-Total-Pages"] = totalPages.ToString();
                Response.Headers["X-Current-Page"] = page.ToString();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving issue reports");
                return StatusCode(500, new ApiResponse<List<IssueReportDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving issue reports",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/issuereport/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIssueReport(string id)
        {
            try
            {
                var issueReport = await _context.IssueReports
                    .Include(i => i.Photos)
                    .Include(i => i.Updates)
                    .FirstOrDefaultAsync(i => i.Id == id && i.PubliclyVisible);

                if (issueReport == null)
                {
                    return NotFound(new ApiResponse<IssueReportDto>
                    {
                        Success = false,
                        Message = "Issue report not found"
                    });
                }

                var issueDto = new IssueReportDto
                {
                    Id = issueReport.Id,
                    ReferenceNumber = issueReport.ReferenceNumber,
                    Category = issueReport.Category.ToString(),
                    UrgencyLevel = issueReport.UrgencyLevel.ToString(),
                    Title = issueReport.Title,
                    Description = issueReport.Description,
                    Location = issueReport.Location,
                    Coordinates = issueReport.Coordinates,
                    Status = issueReport.Status.ToString(),
                    Priority = issueReport.Priority.ToString(),
                    AssignedDepartment = issueReport.AssignedDepartment,
                    EstimatedResolution = issueReport.EstimatedResolution,
                    CreatedAt = issueReport.CreatedAt,
                    Photos = issueReport.Photos.Select(p => new IssuePhotoDto
                    {
                        Id = p.Id,
                        PhotoUrl = p.PhotoUrl,
                        Caption = p.Caption,
                        UploadedAt = p.UploadedAt
                    }).ToList()
                };

                return Ok(new ApiResponse<IssueReportDto>
                {
                    Success = true,
                    Data = issueDto,
                    Message = "Issue report retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving issue report with ID: {IssueId}", id);
                return StatusCode(500, new ApiResponse<IssueReportDto>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the issue report",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // POST: api/issuereport
        [HttpPost]
        public async Task<IActionResult> CreateIssueReport([FromBody] CreateIssueReportRequest request)
        {
            try
            {
                // Generate reference number
                var referenceNumber = $"IR-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

                var issueReport = new IssueReport
                {
                    UserId = request.UserId, // Can be null for anonymous reports
                    ReferenceNumber = referenceNumber,
                    Category = request.Category,
                    UrgencyLevel = request.UrgencyLevel,
                    Title = request.Title,
                    Description = request.Description,
                    Location = request.Location,
                    Coordinates = request.Coordinates,
                    ContactInfo = request.ContactInfo,
                    Status = IssueStatus.Submitted,
                    Priority = request.Priority,
                    PubliclyVisible = request.PubliclyVisible,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.IssueReports.Add(issueReport);
                await _context.SaveChangesAsync();

                var issueDto = new IssueReportDto
                {
                    Id = issueReport.Id,
                    ReferenceNumber = issueReport.ReferenceNumber,
                    Category = issueReport.Category.ToString(),
                    UrgencyLevel = issueReport.UrgencyLevel.ToString(),
                    Title = issueReport.Title,
                    Description = issueReport.Description,
                    Location = issueReport.Location,
                    Coordinates = issueReport.Coordinates,
                    Status = issueReport.Status.ToString(),
                    Priority = issueReport.Priority.ToString(),
                    AssignedDepartment = issueReport.AssignedDepartment,
                    EstimatedResolution = issueReport.EstimatedResolution,
                    CreatedAt = issueReport.CreatedAt,
                    Photos = new List<IssuePhotoDto>()
                };

                return CreatedAtAction(nameof(GetIssueReport), new { id = issueReport.Id }, new ApiResponse<IssueReportDto>
                {
                    Success = true,
                    Data = issueDto,
                    Message = "Issue report created successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating issue report");
                return StatusCode(500, new ApiResponse<IssueReportDto>
                {
                    Success = false,
                    Message = "An error occurred while creating the issue report",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // PUT: api/issuereport/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIssueReport(string id, [FromBody] CreateIssueReportRequest request)
        {
            try
            {
                var issueReport = await _context.IssueReports.FindAsync(id);

                if (issueReport == null)
                {
                    return NotFound(new ApiResponse<IssueReportDto>
                    {
                        Success = false,
                        Message = "Issue report not found"
                    });
                }

                // Update issue report properties
                issueReport.Category = request.Category;
                issueReport.UrgencyLevel = request.UrgencyLevel;
                issueReport.Title = request.Title;
                issueReport.Description = request.Description;
                issueReport.Location = request.Location;
                issueReport.Coordinates = request.Coordinates;
                issueReport.ContactInfo = request.ContactInfo;
                issueReport.Priority = request.Priority;
                issueReport.PubliclyVisible = request.PubliclyVisible;
                issueReport.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var issueDto = new IssueReportDto
                {
                    Id = issueReport.Id,
                    ReferenceNumber = issueReport.ReferenceNumber,
                    Category = issueReport.Category.ToString(),
                    UrgencyLevel = issueReport.UrgencyLevel.ToString(),
                    Title = issueReport.Title,
                    Description = issueReport.Description,
                    Location = issueReport.Location,
                    Coordinates = issueReport.Coordinates,
                    Status = issueReport.Status.ToString(),
                    Priority = issueReport.Priority.ToString(),
                    AssignedDepartment = issueReport.AssignedDepartment,
                    EstimatedResolution = issueReport.EstimatedResolution,
                    CreatedAt = issueReport.CreatedAt,
                    Photos = new List<IssuePhotoDto>()
                };

                return Ok(new ApiResponse<IssueReportDto>
                {
                    Success = true,
                    Data = issueDto,
                    Message = "Issue report updated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating issue report with ID: {IssueId}", id);
                return StatusCode(500, new ApiResponse<IssueReportDto>
                {
                    Success = false,
                    Message = "An error occurred while updating the issue report",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // DELETE: api/issuereport/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssueReport(string id)
        {
            try
            {
                var issueReport = await _context.IssueReports.FindAsync(id);

                if (issueReport == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Issue report not found"
                    });
                }

                _context.IssueReports.Remove(issueReport);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Issue report deleted successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting issue report with ID: {IssueId}", id);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while deleting the issue report",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // PATCH: api/issuereport/{id}/status
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateIssueStatus(string id, [FromBody] UpdateIssueStatusRequest request)
        {
            try
            {
                var issueReport = await _context.IssueReports.FindAsync(id);

                if (issueReport == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Issue report not found"
                    });
                }

                issueReport.Status = request.Status;
                issueReport.AssignedDepartment = request.AssignedDepartment;
                issueReport.AssignedTo = request.AssignedTo;
                issueReport.EstimatedResolution = request.EstimatedResolution;
                issueReport.UpdatedAt = DateTime.UtcNow;

                // If status is resolved, set actual resolution date
                if (request.Status == IssueStatus.Resolved)
                {
                    issueReport.ActualResolution = DateTime.UtcNow;
                    issueReport.ResolutionNotes = request.ResolutionNotes;
                }

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Issue status updated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating issue status with ID: {IssueId}", id);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while updating the issue status",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/issuereport/categories
        [HttpGet("categories")]
        public IActionResult GetIssueCategories()
        {
            try
            {
                var categories = Enum.GetValues<IssueCategory>()
                    .Select(c => new { Value = c.ToString(), Label = c.ToString().Replace("_", " ") })
                    .ToList();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Data = categories,
                    Message = "Issue categories retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving issue categories");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving issue categories",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/issuereport/statuses
        [HttpGet("statuses")]
        public IActionResult GetIssueStatuses()
        {
            try
            {
                var statuses = Enum.GetValues<IssueStatus>()
                    .Select(s => new { Value = s.ToString(), Label = s.ToString().Replace("_", " ") })
                    .ToList();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Data = statuses,
                    Message = "Issue statuses retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving issue statuses");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving issue statuses",
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
} 