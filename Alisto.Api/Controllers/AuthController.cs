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
    public class AuthController : ControllerBase
    {
        private readonly AlistoDbContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AlistoDbContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == request.Email && u.IsActive);

                if (user == null)
                {
                    return Unauthorized(new ApiResponse<AuthResponse>
                    {
                        Success = false,
                        Message = "Invalid email or password"
                    });
                }

                // In a real application, you would verify the password hash here
                // For now, we'll just check if the user exists and is active

                // Update last login
                user.LastLoginAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                // Create session
                var session = new UserSession
                {
                    UserId = user.Id,
                    DeviceInfo = request.DeviceInfo,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    LastActivityAt = DateTime.UtcNow
                };

                _context.UserSessions.Add(session);
                await _context.SaveChangesAsync();

                var authResponse = new AuthResponse
                {
                    UserId = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    SessionId = session.Id,
                    LastLoginAt = user.LastLoginAt
                };

                return Ok(new ApiResponse<AuthResponse>
                {
                    Success = true,
                    Data = authResponse,
                    Message = "Login successful"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for email: {Email}", request.Email);
                return StatusCode(500, new ApiResponse<AuthResponse>
                {
                    Success = false,
                    Message = "An error occurred during login",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                // Check if user already exists
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == request.Email);

                if (existingUser != null)
                {
                    return BadRequest(new ApiResponse<AuthResponse>
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
                    UpdatedAt = DateTime.UtcNow,
                    LastLoginAt = DateTime.UtcNow
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Create session
                var session = new UserSession
                {
                    UserId = user.Id,
                    DeviceInfo = request.DeviceInfo,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    LastActivityAt = DateTime.UtcNow
                };

                _context.UserSessions.Add(session);
                await _context.SaveChangesAsync();

                var authResponse = new AuthResponse
                {
                    UserId = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    SessionId = session.Id,
                    LastLoginAt = user.LastLoginAt
                };

                return CreatedAtAction(nameof(Login), new ApiResponse<AuthResponse>
                {
                    Success = true,
                    Data = authResponse,
                    Message = "Registration successful"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration for email: {Email}", request.Email);
                return StatusCode(500, new ApiResponse<AuthResponse>
                {
                    Success = false,
                    Message = "An error occurred during registration",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // POST: api/auth/logout
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var session = await _context.UserSessions
                    .FirstOrDefaultAsync(s => s.Id == request.SessionId && s.IsActive);

                if (session == null)
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Invalid session"
                    });
                }

                session.IsActive = false;
                session.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Logout successful"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout for session: {SessionId}", request.SessionId);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred during logout",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // POST: api/auth/refresh
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var session = await _context.UserSessions
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.Id == request.SessionId && s.IsActive);

                if (session == null)
                {
                    return Unauthorized(new ApiResponse<AuthResponse>
                    {
                        Success = false,
                        Message = "Invalid session"
                    });
                }

                // Update session activity
                session.LastActivityAt = DateTime.UtcNow;
                session.UpdatedAt = DateTime.UtcNow;

                // Update user last login
                session.User.LastLoginAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                var authResponse = new AuthResponse
                {
                    UserId = session.User.Id,
                    Email = session.User.Email,
                    FirstName = session.User.FirstName,
                    LastName = session.User.LastName,
                    SessionId = session.Id,
                    LastLoginAt = session.User.LastLoginAt
                };

                return Ok(new ApiResponse<AuthResponse>
                {
                    Success = true,
                    Data = authResponse,
                    Message = "Token refreshed successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing token for session: {SessionId}", request.SessionId);
                return StatusCode(500, new ApiResponse<AuthResponse>
                {
                    Success = false,
                    Message = "An error occurred while refreshing token",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/auth/me
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                // In a real application, you would get the user ID from the JWT token
                // For now, we'll return a placeholder response
                return Ok(new ApiResponse<UserDto>
                {
                    Success = true,
                    Message = "Current user endpoint - implement JWT token parsing",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting current user");
                return StatusCode(500, new ApiResponse<UserDto>
                {
                    Success = false,
                    Message = "An error occurred while getting current user",
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
} 