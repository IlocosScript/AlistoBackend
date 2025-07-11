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
                        ExternalId = u.ExternalId,
                        AuthProvider = u.AuthProvider,
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

                Response.Headers["X-Total-Count"] = totalCount.ToString();
                Response.Headers["X-Total-Pages"] = totalPages.ToString();
                Response.Headers["X-Current-Page"] = page.ToString();

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
                var user = await _context.Users.FindAsync(id);

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
                    ExternalId = user.ExternalId,
                    AuthProvider = user.AuthProvider,
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

        // GET: api/user/external/{externalId}/{authProvider}
        [HttpGet("external/{externalId}/{authProvider}")]
        public async Task<IActionResult> GetUserByExternalId(string externalId, string authProvider)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.ExternalId == externalId && u.AuthProvider == authProvider);

                if (user == null)
                {
                    return NotFound(new ApiResponse<UserDto>
                    {
                        Success = false,
                        Message = "User not found with provided external ID and auth provider"
                    });
                }

                var userDto = new UserDto
                {
                    Id = user.Id,
                    ExternalId = user.ExternalId,
                    AuthProvider = user.AuthProvider,
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
                _logger.LogError(ex, "Error retrieving user with external ID: {ExternalId} and provider: {AuthProvider}", externalId, authProvider);
                return StatusCode(500, new ApiResponse<UserDto>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the user",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // POST: api/user/from-auth
        [HttpPost("from-auth")]
        public async Task<IActionResult> CreateUserFromAuth([FromBody] CreateUserFromAuthRequest request)
        {
            try
            {
                // Check if user already exists by external ID
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.ExternalId == request.ExternalId && u.AuthProvider == request.AuthProvider);

                if (existingUser != null)
                {
                    // Update last login
                    existingUser.LastLoginAt = DateTime.UtcNow;
                    await _context.SaveChangesAsync();

                    var userDto = new UserDto
                    {
                        Id = existingUser.Id,
                        ExternalId = existingUser.ExternalId,
                        AuthProvider = existingUser.AuthProvider,
                        Email = existingUser.Email,
                        FirstName = existingUser.FirstName,
                        LastName = existingUser.LastName,
                        MiddleName = existingUser.MiddleName,
                        PhoneNumber = existingUser.PhoneNumber,
                        Address = existingUser.Address,
                        DateOfBirth = existingUser.DateOfBirth,
                        IsActive = existingUser.IsActive,
                        CreatedAt = existingUser.CreatedAt,
                        LastLoginAt = existingUser.LastLoginAt,
                        ProfileImageUrl = existingUser.ProfileImageUrl,
                        EmergencyContactName = existingUser.EmergencyContactName,
                        EmergencyContactNumber = existingUser.EmergencyContactNumber
                    };

                    return Ok(new ApiResponse<UserDto>
                    {
                        Success = true,
                        Data = userDto,
                        Message = "User found and login updated"
                    });
                }

                // Check if user exists by email
                var userByEmail = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == request.Email);

                if (userByEmail != null)
                {
                    // Link existing user to external auth
                    userByEmail.ExternalId = request.ExternalId;
                    userByEmail.AuthProvider = request.AuthProvider;
                    userByEmail.LastLoginAt = DateTime.UtcNow;
                    userByEmail.UpdatedAt = DateTime.UtcNow;

                    // Update profile data if provided
                    if (!string.IsNullOrEmpty(request.FirstName)) userByEmail.FirstName = request.FirstName;
                    if (!string.IsNullOrEmpty(request.LastName)) userByEmail.LastName = request.LastName;
                    if (!string.IsNullOrEmpty(request.MiddleName)) userByEmail.MiddleName = request.MiddleName;
                    if (!string.IsNullOrEmpty(request.PhoneNumber)) userByEmail.PhoneNumber = request.PhoneNumber;
                    if (!string.IsNullOrEmpty(request.Address)) userByEmail.Address = request.Address;
                    if (request.DateOfBirth.HasValue) userByEmail.DateOfBirth = request.DateOfBirth;
                    if (!string.IsNullOrEmpty(request.ProfileImageUrl)) userByEmail.ProfileImageUrl = request.ProfileImageUrl;
                    if (!string.IsNullOrEmpty(request.EmergencyContactName)) userByEmail.EmergencyContactName = request.EmergencyContactName;
                    if (!string.IsNullOrEmpty(request.EmergencyContactNumber)) userByEmail.EmergencyContactNumber = request.EmergencyContactNumber;

                    await _context.SaveChangesAsync();

                    var userDto = new UserDto
                    {
                        Id = userByEmail.Id,
                        ExternalId = userByEmail.ExternalId,
                        AuthProvider = userByEmail.AuthProvider,
                        Email = userByEmail.Email,
                        FirstName = userByEmail.FirstName,
                        LastName = userByEmail.LastName,
                        MiddleName = userByEmail.MiddleName,
                        PhoneNumber = userByEmail.PhoneNumber,
                        Address = userByEmail.Address,
                        DateOfBirth = userByEmail.DateOfBirth,
                        IsActive = userByEmail.IsActive,
                        CreatedAt = userByEmail.CreatedAt,
                        LastLoginAt = userByEmail.LastLoginAt,
                        ProfileImageUrl = userByEmail.ProfileImageUrl,
                        EmergencyContactName = userByEmail.EmergencyContactName,
                        EmergencyContactNumber = userByEmail.EmergencyContactNumber
                    };

                    return Ok(new ApiResponse<UserDto>
                    {
                        Success = true,
                        Data = userDto,
                        Message = "Existing user linked to external auth"
                    });
                }

                // Create new user
                var user = new User
                {
                    ExternalId = request.ExternalId,
                    AuthProvider = request.AuthProvider,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    MiddleName = request.MiddleName,
                    PhoneNumber = request.PhoneNumber ?? string.Empty,
                    Address = request.Address ?? string.Empty,
                    DateOfBirth = request.DateOfBirth,
                    ProfileImageUrl = request.ProfileImageUrl,
                    EmergencyContactName = request.EmergencyContactName,
                    EmergencyContactNumber = request.EmergencyContactNumber,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    LastLoginAt = DateTime.UtcNow
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var newUserDto = new UserDto
                {
                    Id = user.Id,
                    ExternalId = user.ExternalId,
                    AuthProvider = user.AuthProvider,
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
                    Data = newUserDto,
                    Message = "User created successfully from external auth"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user from external auth");
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
                    ExternalId = user.ExternalId,
                    AuthProvider = user.AuthProvider,
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

        // PUT: api/user/sync-from-auth
        [HttpPut("sync-from-auth")]
        public async Task<IActionResult> SyncUserFromAuth([FromBody] SyncUserFromAuthRequest request)
        {
            try
            {
                // Find user by external ID
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.ExternalId == request.ExternalId && u.AuthProvider == request.AuthProvider);

                if (user == null)
                {
                    return NotFound(new ApiResponse<UserDto>
                    {
                        Success = false,
                        Message = "User not found with provided external ID and auth provider"
                    });
                }

                // Update user data from auth provider
                user.Email = request.Email;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.MiddleName = request.MiddleName;
                user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
                user.Address = request.Address ?? user.Address;
                user.DateOfBirth = request.DateOfBirth ?? user.DateOfBirth;
                user.ProfileImageUrl = request.ProfileImageUrl ?? user.ProfileImageUrl;
                user.EmergencyContactName = request.EmergencyContactName ?? user.EmergencyContactName;
                user.EmergencyContactNumber = request.EmergencyContactNumber ?? user.EmergencyContactNumber;
                user.LastLoginAt = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var userDto = new UserDto
                {
                    Id = user.Id,
                    ExternalId = user.ExternalId,
                    AuthProvider = user.AuthProvider,
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
                    Message = "User data synced successfully from external auth"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error syncing user from external auth");
                return StatusCode(500, new ApiResponse<UserDto>
                {
                    Success = false,
                    Message = "An error occurred while syncing the user",
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