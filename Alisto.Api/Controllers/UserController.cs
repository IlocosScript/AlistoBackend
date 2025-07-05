using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alisto.Api.Data;
using Alisto.Api.Models;
using Alisto.Api.DTOs;

namespace Alisto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AlistoDbContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(AlistoDbContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/user
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var query = _context.Users.AsQueryable();
                var totalCount = await query.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var users = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(u => new UserDto
                    {
                        Id = u.Id,
                        Email = u.Email,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        MiddleName = u.MiddleName,
                        PhoneNumber = u.PhoneNumber,
                        Address = u.Address,
                        DateOfBirth = u.DateOfBirth,
                        IsActive = u.IsActive,
                        CreatedAt = u.CreatedAt,
                        LastLoginAt = u.LastLoginAt,
                        ProfileImageUrl = u.ProfileImageUrl,
                        EmergencyContactName = u.EmergencyContactName,
                        EmergencyContactNumber = u.EmergencyContactNumber
                    })
                    .ToListAsync();

                var response = new ApiResponse<List<UserDto>>
                {
                    Success = true,
                    Data = users,
                    Message = $"Retrieved {users.Count} users successfully"
                };

                Response.Headers.Add("X-Total-Count", totalCount.ToString());
                Response.Headers.Add("X-Total-Pages", totalPages.ToString());
                Response.Headers.Add("X-Current-Page", page.ToString());

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users");
                return StatusCode(500, new ApiResponse<List<UserDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving users",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUser(string id)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    return NotFound(new ApiResponse<UserDto>
                    {
                        Success = false,
                        Message = "User not found"
                    });
                }

                var userDto = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MiddleName = user.MiddleName,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    DateOfBirth = user.DateOfBirth,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    LastLoginAt = user.LastLoginAt,
                    ProfileImageUrl = user.ProfileImageUrl,
                    EmergencyContactName = user.EmergencyContactName,
                    EmergencyContactNumber = user.EmergencyContactNumber
                };

                return Ok(new ApiResponse<UserDto>
                {
                    Success = true,
                    Data = userDto,
                    Message = "User retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with ID: {UserId}", id);
                return StatusCode(500, new ApiResponse<UserDto>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the user",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] RegisterRequest request)
        {
            try
            {
                // Check if user already exists
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == request.Email);

                if (existingUser != null)
                {
                    return BadRequest(new ApiResponse<UserDto>
                    {
                        Success = false,
                        Message = "User with this email already exists"
                    });
                }

                var user = new User
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    MiddleName = request.MiddleName,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    DateOfBirth = request.DateOfBirth,
                    EmergencyContactName = request.EmergencyContactName,
                    EmergencyContactNumber = request.EmergencyContactNumber,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var userDto = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MiddleName = user.MiddleName,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    DateOfBirth = user.DateOfBirth,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    LastLoginAt = user.LastLoginAt,
                    ProfileImageUrl = user.ProfileImageUrl,
                    EmergencyContactName = user.EmergencyContactName,
                    EmergencyContactNumber = user.EmergencyContactNumber
                };

                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new ApiResponse<UserDto>
                {
                    Success = true,
                    Data = userDto,
                    Message = "User created successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                return StatusCode(500, new ApiResponse<UserDto>
                {
                    Success = false,
                    Message = "An error occurred while creating the user",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserRequest request)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    return NotFound(new ApiResponse<UserDto>
                    {
                        Success = false,
                        Message = "User not found"
                    });
                }

                // Update user properties
                user.FirstName = request.FirstName ?? user.FirstName;
                user.LastName = request.LastName ?? user.LastName;
                user.MiddleName = request.MiddleName ?? user.MiddleName;
                user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
                user.Address = request.Address ?? user.Address;
                user.DateOfBirth = request.DateOfBirth ?? user.DateOfBirth;
                user.EmergencyContactName = request.EmergencyContactName ?? user.EmergencyContactName;
                user.EmergencyContactNumber = request.EmergencyContactNumber ?? user.EmergencyContactNumber;
                user.ProfileImageUrl = request.ProfileImageUrl ?? user.ProfileImageUrl;
                user.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var userDto = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MiddleName = user.MiddleName,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    DateOfBirth = user.DateOfBirth,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    LastLoginAt = user.LastLoginAt,
                    ProfileImageUrl = user.ProfileImageUrl,
                    EmergencyContactName = user.EmergencyContactName,
                    EmergencyContactNumber = user.EmergencyContactNumber
                };

                return Ok(new ApiResponse<UserDto>
                {
                    Success = true,
                    Data = userDto,
                    Message = "User updated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with ID: {UserId}", id);
                return StatusCode(500, new ApiResponse<UserDto>
                {
                    Success = false,
                    Message = "An error occurred while updating the user",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "User not found"
                    });
                }

                // Soft delete - mark as inactive
                user.IsActive = false;
                user.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "User deleted successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with ID: {UserId}", id);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while deleting the user",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // PATCH: api/user/{id}/activate
        [HttpPatch("{id}/activate")]
        [Authorize]
        public async Task<IActionResult> ActivateUser(string id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "User not found"
                    });
                }

                user.IsActive = true;
                user.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "User activated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error activating user with ID: {UserId}", id);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while activating the user",
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
} 