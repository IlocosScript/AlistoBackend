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
    public class PublicProjectController : ControllerBase
    {
        private readonly AlistoDbContext _context;
        private readonly ILogger<PublicProjectController> _logger;

        public PublicProjectController(AlistoDbContext context, ILogger<PublicProjectController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/publicproject
        [HttpGet]
        public async Task<IActionResult> GetPublicProjects([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
            [FromQuery] ProjectStatus? status = null, [FromQuery] bool? isPublic = null)
        {
            try
            {
                var query = _context.PublicProjects.AsQueryable();

                // Apply filters
                if (status.HasValue)
                    query = query.Where(pp => pp.Status == status.Value);

                if (isPublic.HasValue)
                    query = query.Where(pp => pp.IsPublic == isPublic.Value);

                var totalCount = await query.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var publicProjects = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(pp => new PublicProjectDto
                    {
                        Id = pp.Id,
                        Title = pp.Title,
                        Description = pp.Description,
                        Cost = pp.Cost,
                        Contractor = pp.Contractor,
                        Status = pp.Status,
                        Progress = pp.Progress,
                        StartDate = pp.StartDate,
                        ExpectedEndDate = pp.ExpectedEndDate,
                        ActualEndDate = pp.ActualEndDate,
                        Location = pp.Location,
                        ProjectType = pp.ProjectType,
                        FundingSource = pp.FundingSource,
                        IsPublic = pp.IsPublic,
                        CreatedAt = pp.CreatedAt,
                        UpdatedAt = pp.UpdatedAt
                    })
                    .ToListAsync();

                var response = new ApiResponse<List<PublicProjectDto>>
                {
                    Success = true,
                    Data = publicProjects,
                    Message = $"Retrieved {publicProjects.Count} public projects successfully"
                };

                Response.Headers["X-Total-Count"] = totalCount.ToString();
                Response.Headers["X-Total-Pages"] = totalPages.ToString();
                Response.Headers["X-Current-Page"] = page.ToString();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving public projects");
                return StatusCode(500, new ApiResponse<List<PublicProjectDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving public projects",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/publicproject/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublicProject(int id)
        {
            try
            {
                var publicProject = await _context.PublicProjects.FindAsync(id);

                if (publicProject == null)
                {
                    return NotFound(new ApiResponse<PublicProjectDto>
                    {
                        Success = false,
                        Message = "Public project not found"
                    });
                }

                var publicProjectDto = new PublicProjectDto
                {
                    Id = publicProject.Id,
                    Title = publicProject.Title,
                    Description = publicProject.Description,
                    Cost = publicProject.Cost,
                    Contractor = publicProject.Contractor,
                    Status = publicProject.Status,
                    Progress = publicProject.Progress,
                    StartDate = publicProject.StartDate,
                    ExpectedEndDate = publicProject.ExpectedEndDate,
                    ActualEndDate = publicProject.ActualEndDate,
                    Location = publicProject.Location,
                    ProjectType = publicProject.ProjectType,
                    FundingSource = publicProject.FundingSource,
                    IsPublic = publicProject.IsPublic,
                    CreatedAt = publicProject.CreatedAt,
                    UpdatedAt = publicProject.UpdatedAt
                };

                return Ok(new ApiResponse<PublicProjectDto>
                {
                    Success = true,
                    Data = publicProjectDto,
                    Message = "Public project retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving public project with ID: {PublicProjectId}", id);
                return StatusCode(500, new ApiResponse<PublicProjectDto>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the public project",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // POST: api/publicproject
        [HttpPost]
        public async Task<IActionResult> CreatePublicProject([FromBody] CreatePublicProjectRequest request)
        {
            try
            {
                var publicProject = new PublicProject
                {
                    Title = request.Title,
                    Description = request.Description,
                    Cost = request.Cost,
                    Contractor = request.Contractor,
                    Status = request.Status,
                    Progress = request.Progress,
                    StartDate = request.StartDate,
                    ExpectedEndDate = request.ExpectedEndDate,
                    Location = request.Location,
                    ProjectType = request.ProjectType,
                    FundingSource = request.FundingSource,
                    IsPublic = request.IsPublic,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.PublicProjects.Add(publicProject);
                await _context.SaveChangesAsync();

                var publicProjectDto = new PublicProjectDto
                {
                    Id = publicProject.Id,
                    Title = publicProject.Title,
                    Description = publicProject.Description,
                    Cost = publicProject.Cost,
                    Contractor = publicProject.Contractor,
                    Status = publicProject.Status,
                    Progress = publicProject.Progress,
                    StartDate = publicProject.StartDate,
                    ExpectedEndDate = publicProject.ExpectedEndDate,
                    ActualEndDate = publicProject.ActualEndDate,
                    Location = publicProject.Location,
                    ProjectType = publicProject.ProjectType,
                    FundingSource = publicProject.FundingSource,
                    IsPublic = publicProject.IsPublic,
                    CreatedAt = publicProject.CreatedAt,
                    UpdatedAt = publicProject.UpdatedAt
                };

                return CreatedAtAction(nameof(GetPublicProject), new { id = publicProject.Id }, new ApiResponse<PublicProjectDto>
                {
                    Success = true,
                    Data = publicProjectDto,
                    Message = "Public project created successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating public project");
                return StatusCode(500, new ApiResponse<PublicProjectDto>
                {
                    Success = false,
                    Message = "An error occurred while creating the public project",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // PUT: api/publicproject/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublicProject(int id, [FromBody] CreatePublicProjectRequest request)
        {
            try
            {
                var publicProject = await _context.PublicProjects.FindAsync(id);

                if (publicProject == null)
                {
                    return NotFound(new ApiResponse<PublicProjectDto>
                    {
                        Success = false,
                        Message = "Public project not found"
                    });
                }

                // Update public project properties
                publicProject.Title = request.Title;
                publicProject.Description = request.Description;
                publicProject.Cost = request.Cost;
                publicProject.Contractor = request.Contractor;
                publicProject.Status = request.Status;
                publicProject.Progress = request.Progress;
                publicProject.StartDate = request.StartDate;
                publicProject.ExpectedEndDate = request.ExpectedEndDate;
                publicProject.Location = request.Location;
                publicProject.ProjectType = request.ProjectType;
                publicProject.FundingSource = request.FundingSource;
                publicProject.IsPublic = request.IsPublic;
                publicProject.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var publicProjectDto = new PublicProjectDto
                {
                    Id = publicProject.Id,
                    Title = publicProject.Title,
                    Description = publicProject.Description,
                    Cost = publicProject.Cost,
                    Contractor = publicProject.Contractor,
                    Status = publicProject.Status,
                    Progress = publicProject.Progress,
                    StartDate = publicProject.StartDate,
                    ExpectedEndDate = publicProject.ExpectedEndDate,
                    ActualEndDate = publicProject.ActualEndDate,
                    Location = publicProject.Location,
                    ProjectType = publicProject.ProjectType,
                    FundingSource = publicProject.FundingSource,
                    IsPublic = publicProject.IsPublic,
                    CreatedAt = publicProject.CreatedAt,
                    UpdatedAt = publicProject.UpdatedAt
                };

                return Ok(new ApiResponse<PublicProjectDto>
                {
                    Success = true,
                    Data = publicProjectDto,
                    Message = "Public project updated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating public project with ID: {PublicProjectId}", id);
                return StatusCode(500, new ApiResponse<PublicProjectDto>
                {
                    Success = false,
                    Message = "An error occurred while updating the public project",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // PATCH: api/publicproject/{id}/status
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdatePublicProjectStatus(int id, [FromBody] UpdatePublicProjectStatusRequest request)
        {
            try
            {
                var publicProject = await _context.PublicProjects.FindAsync(id);

                if (publicProject == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Public project not found"
                    });
                }

                publicProject.Status = request.Status;
                publicProject.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Public project status updated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating public project status with ID: {PublicProjectId}", id);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while updating the public project status",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // DELETE: api/publicproject/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublicProject(int id)
        {
            try
            {
                var publicProject = await _context.PublicProjects.FindAsync(id);

                if (publicProject == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Public project not found"
                    });
                }

                _context.PublicProjects.Remove(publicProject);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Public project deleted successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting public project with ID: {PublicProjectId}", id);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while deleting the public project",
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
} 