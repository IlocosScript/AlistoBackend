using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alisto.Api.Data;
using Alisto.Api.Models;
using Alisto.Api.DTOs;
using Alisto.Api.Enums;
using Alisto.Api.Services;

namespace Alisto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly AlistoDbContext _context;
        private readonly ILogger<NewsController> _logger;
        private readonly ILocalFileUploadService _fileUploadService;

        public NewsController(AlistoDbContext context, ILogger<NewsController> logger, ILocalFileUploadService fileUploadService)
        {
            _context = context;
            _logger = logger;
            _fileUploadService = fileUploadService;
        }

        // GET: api/news
        [HttpGet]
        public async Task<IActionResult> GetNews([FromQuery] int page = 1, [FromQuery] int pageSize = 10, 
            [FromQuery] string? category = null, [FromQuery] bool? isFeatured = null, [FromQuery] bool? isTrending = null)
        {
            try
            {
                var query = _context.NewsArticles.AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(category))
                {
                    if (Enum.TryParse<NewsCategory>(category, true, out var categoryEnum))
                    {
                        query = query.Where(n => n.Category == categoryEnum);
                    }
                }

                if (isFeatured.HasValue)
                {
                    query = query.Where(n => n.IsFeatured == isFeatured.Value);
                }

                if (isTrending.HasValue)
                {
                    query = query.Where(n => n.IsTrending == isTrending.Value);
                }

                // Return all news articles regardless of status

                var totalCount = await query.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var news = await query
                    .OrderByDescending(n => n.PublishedDate ?? DateTime.MinValue)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(n => new NewsArticleDto
                    {
                        Id = n.Id,
                        Title = n.Title,
                        Summary = n.Summary,
                        ImageUrl = n.ImageUrl,
                        PublishedDate = n.PublishedDate,
                        PublishedTime = n.PublishedTime,
                        Location = n.Location,
                        ExpectedAttendees = n.ExpectedAttendees,
                        Category = n.Category.ToString(),
                        Author = n.Author,
                        Tags = n.TagsList,
                        IsFeatured = n.IsFeatured,
                        IsTrending = n.IsTrending,
                        ViewCount = n.ViewCount,
                        Status = n.Status.ToString()
                    })
                    .ToListAsync();

                var response = new ApiResponse<List<NewsArticleDto>>
                {
                    Success = true,
                    Data = news,
                    Message = $"Retrieved {news.Count} news articles successfully"
                };

                Response.Headers["X-Total-Count"] = totalCount.ToString();
                Response.Headers["X-Total-Pages"] = totalPages.ToString();
                Response.Headers["X-Current-Page"] = page.ToString();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving news articles");
                return StatusCode(500, new ApiResponse<List<NewsArticleDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving news articles",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/news/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsArticle(int id)
        {
            try
            {
                var newsArticle = await _context.NewsArticles
                    .FirstOrDefaultAsync(n => n.Id == id);

                if (newsArticle == null)
                {
                    return NotFound(new ApiResponse<NewsArticleDto>
                    {
                        Success = false,
                        Message = "News article not found"
                    });
                }

                // Increment view count
                newsArticle.ViewCount++;
                await _context.SaveChangesAsync();

                var newsDto = new NewsArticleDto
                {
                    Id = newsArticle.Id,
                    Title = newsArticle.Title,
                    Summary = newsArticle.Summary,
                    FullContent = newsArticle.FullContent,
                    ImageUrl = newsArticle.ImageUrl,
                    PublishedDate = newsArticle.PublishedDate,
                    PublishedTime = newsArticle.PublishedTime,
                    Location = newsArticle.Location,
                    ExpectedAttendees = newsArticle.ExpectedAttendees,
                    Category = newsArticle.Category.ToString(),
                    Author = newsArticle.Author,
                    Tags = newsArticle.TagsList,
                    IsFeatured = newsArticle.IsFeatured,
                    IsTrending = newsArticle.IsTrending,
                    ViewCount = newsArticle.ViewCount,
                    Status = newsArticle.Status.ToString()
                };

                return Ok(new ApiResponse<NewsArticleDto>
                {
                    Success = true,
                    Data = newsDto,
                    Message = "News article retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving news article with ID: {NewsId}", id);
                return StatusCode(500, new ApiResponse<NewsArticleDto>
                {
                    Success = false,
                    Message = "An error occurred while retrieving the news article",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // POST: api/news
        [HttpPost]
        public async Task<IActionResult> CreateNewsArticle([FromBody] CreateNewsRequest request)
        {
            try
            {
                var newsArticle = new NewsArticle
                {
                    Title = request.Title,
                    Summary = request.Summary,
                    FullContent = request.FullContent,
                    ImageUrl = request.ImageUrl,
                    PublishedDate = request.PublishedDate,
                    PublishedTime = request.PublishedTime,
                    Location = request.Location,
                    ExpectedAttendees = request.ExpectedAttendees,
                    Category = request.Category,
                    Author = request.Author,
                    Tags = request.Tags != null ? System.Text.Json.JsonSerializer.Serialize(request.Tags) : "[]",
                    IsFeatured = request.IsFeatured,
                    IsTrending = request.IsTrending,
                    Status = ContentStatus.Draft,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.NewsArticles.Add(newsArticle);
                await _context.SaveChangesAsync();

                var newsDto = new NewsArticleDto
                {
                    Id = newsArticle.Id,
                    Title = newsArticle.Title,
                    Summary = newsArticle.Summary,
                    FullContent = newsArticle.FullContent,
                    ImageUrl = newsArticle.ImageUrl,
                    PublishedDate = newsArticle.PublishedDate,
                    PublishedTime = newsArticle.PublishedTime,
                    Location = newsArticle.Location,
                    ExpectedAttendees = newsArticle.ExpectedAttendees,
                    Category = newsArticle.Category.ToString(),
                    Author = newsArticle.Author,
                    Tags = newsArticle.TagsList,
                    IsFeatured = newsArticle.IsFeatured,
                    IsTrending = newsArticle.IsTrending,
                    ViewCount = newsArticle.ViewCount,
                    Status = newsArticle.Status.ToString()
                };

                return CreatedAtAction(nameof(GetNewsArticle), new { id = newsArticle.Id }, new ApiResponse<NewsArticleDto>
                {
                    Success = true,
                    Data = newsDto,
                    Message = "News article created successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating news article");
                return StatusCode(500, new ApiResponse<NewsArticleDto>
                {
                    Success = false,
                    Message = "An error occurred while creating the news article",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // POST: api/news/with-image
        [HttpPost("with-image")]
        public async Task<IActionResult> CreateNewsArticleWithImage([FromForm] CreateNewsWithImageRequest request)
        {
            try
            {
                string? imageUrl = null;

                // Upload image to local directory if provided
                if (request.Image != null)
                {
                    try
                    {
                        // Get local file upload service from DI
                        var localFileService = HttpContext.RequestServices.GetService<Alisto.Api.Services.ILocalFileUploadService>();
                        
                        if (localFileService != null)
                        {
                            imageUrl = await localFileService.UploadFileAsync(request.Image, "news-images");
                            
                            if (!string.IsNullOrEmpty(imageUrl))
                            {
                                _logger.LogInformation("Image uploaded successfully to local storage. URL: {Url}", imageUrl);
                            }
                            else
                            {
                                _logger.LogWarning("Failed to upload image to local storage for news article: {Title}", request.Title);
                            }
                        }
                        else
                        {
                            _logger.LogWarning("Local file upload service not available.");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error uploading image to local storage for news article: {Title}. Continuing without image.", request.Title);
                        // Continue without image - don't fail the entire request
                    }
                }

                // Create news article (with or without image)
                var newsArticle = new NewsArticle
                {
                    Title = request.Title,
                    Summary = request.Summary,
                    FullContent = request.FullContent,
                    ImageUrl = imageUrl, // Will be null if upload failed
                    PublishedDate = request.PublishedDate,
                    PublishedTime = request.PublishedTime,
                    Location = request.Location,
                    ExpectedAttendees = request.ExpectedAttendees,
                    Category = request.Category,
                    Author = request.Author,
                    Tags = request.Tags != null ? System.Text.Json.JsonSerializer.Serialize(request.Tags) : "[]",
                    IsFeatured = request.IsFeatured,
                    IsTrending = request.IsTrending,
                    Status = ContentStatus.Draft,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.NewsArticles.Add(newsArticle);
                await _context.SaveChangesAsync();

                var newsDto = new NewsArticleDto
                {
                    Id = newsArticle.Id,
                    Title = newsArticle.Title,
                    Summary = newsArticle.Summary,
                    FullContent = newsArticle.FullContent,
                    ImageUrl = newsArticle.ImageUrl,
                    PublishedDate = newsArticle.PublishedDate,
                    PublishedTime = newsArticle.PublishedTime,
                    Location = newsArticle.Location,
                    ExpectedAttendees = newsArticle.ExpectedAttendees,
                    Category = newsArticle.Category.ToString(),
                    Author = newsArticle.Author,
                    Tags = newsArticle.TagsList,
                    IsFeatured = newsArticle.IsFeatured,
                    IsTrending = newsArticle.IsTrending,
                    ViewCount = newsArticle.ViewCount,
                    Status = newsArticle.Status.ToString()
                };

                var message = imageUrl != null 
                    ? "News article with image created successfully (uploaded to local storage)" 
                    : "News article created successfully (image upload failed or not provided)";

                return CreatedAtAction(nameof(GetNewsArticle), new { id = newsArticle.Id }, new ApiResponse<NewsArticleDto>
                {
                    Success = true,
                    Data = newsDto,
                    Message = message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating news article with image");
                return StatusCode(500, new ApiResponse<NewsArticleDto>
                {
                    Success = false,
                    Message = "An error occurred while creating the news article",
                    Errors = new List<string> { ex.Message }
                });
            }
        }



        // PUT: api/news/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNewsArticle(int id, [FromBody] UpdateNewsRequest request)
        {
            try
            {
                var newsArticle = await _context.NewsArticles.FindAsync(id);

                if (newsArticle == null)
                {
                    return NotFound(new ApiResponse<NewsArticleDto>
                    {
                        Success = false,
                        Message = "News article not found"
                    });
                }

                // Update news article properties
                newsArticle.Title = request.Title;
                newsArticle.Summary = request.Summary;
                newsArticle.FullContent = request.FullContent;
                newsArticle.ImageUrl = request.ImageUrl;
                newsArticle.Location = request.Location;
                newsArticle.ExpectedAttendees = request.ExpectedAttendees;
                newsArticle.Category = request.Category;
                newsArticle.Author = request.Author;
                newsArticle.Tags = request.Tags != null ? System.Text.Json.JsonSerializer.Serialize(request.Tags) : "[]";
                newsArticle.IsFeatured = request.IsFeatured;
                newsArticle.IsTrending = request.IsTrending;
                newsArticle.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var newsDto = new NewsArticleDto
                {
                    Id = newsArticle.Id,
                    Title = newsArticle.Title,
                    Summary = newsArticle.Summary,
                    FullContent = newsArticle.FullContent,
                    ImageUrl = newsArticle.ImageUrl,
                    PublishedDate = newsArticle.PublishedDate,
                    PublishedTime = newsArticle.PublishedTime,
                    Location = newsArticle.Location,
                    ExpectedAttendees = newsArticle.ExpectedAttendees,
                    Category = newsArticle.Category.ToString(),
                    Author = newsArticle.Author,
                    Tags = newsArticle.TagsList,
                    IsFeatured = newsArticle.IsFeatured,
                    IsTrending = newsArticle.IsTrending,
                    ViewCount = newsArticle.ViewCount,
                    Status = newsArticle.Status.ToString()
                };

                return Ok(new ApiResponse<NewsArticleDto>
                {
                    Success = true,
                    Data = newsDto,
                    Message = "News article updated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating news article with ID: {NewsId}", id);
                return StatusCode(500, new ApiResponse<NewsArticleDto>
                {
                    Success = false,
                    Message = "An error occurred while updating the news article",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // PUT: api/news/{id}/with-image
        [HttpPut("{id}/with-image")]
        public async Task<IActionResult> UpdateNewsArticleWithImage(int id, [FromForm] UpdateNewsWithImageRequest request)
        {
            try
            {
                var newsArticle = await _context.NewsArticles.FindAsync(id);

                if (newsArticle == null)
                {
                    return NotFound(new ApiResponse<NewsArticleDto>
                    {
                        Success = false,
                        Message = "News article not found"
                    });
                }

                string? imageUrl = null;

                // Upload new image if provided
                if (request.Image != null)
                {
                    try
                    {
                        imageUrl = await _fileUploadService.UploadFileAsync(request.Image, "news");
                        
                        if (imageUrl == null)
                        {
                            _logger.LogWarning("Failed to upload image for news article {NewsId}", id);
                            // Continue with update even if image upload fails
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error uploading image for news article {NewsId}", id);
                        // Continue with update even if image upload fails
                    }
                }

                // Update news article properties
                newsArticle.Title = request.Title;
                newsArticle.Summary = request.Summary;
                newsArticle.FullContent = request.FullContent;
                
                // Update image URL only if new image was uploaded successfully
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    // Delete old image if it exists
                    if (!string.IsNullOrEmpty(newsArticle.ImageUrl))
                    {
                        try
                        {
                            await _fileUploadService.DeleteFileAsync(newsArticle.ImageUrl);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "Failed to delete old image for news article {NewsId}", id);
                        }
                    }
                    newsArticle.ImageUrl = imageUrl;
                }
                
                newsArticle.Location = request.Location;
                newsArticle.ExpectedAttendees = request.ExpectedAttendees;
                newsArticle.Category = request.Category;
                newsArticle.Author = request.Author;
                newsArticle.Tags = request.Tags != null ? System.Text.Json.JsonSerializer.Serialize(request.Tags) : "[]";
                newsArticle.IsFeatured = request.IsFeatured;
                newsArticle.IsTrending = request.IsTrending;
                newsArticle.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var newsDto = new NewsArticleDto
                {
                    Id = newsArticle.Id,
                    Title = newsArticle.Title,
                    Summary = newsArticle.Summary,
                    FullContent = newsArticle.FullContent,
                    ImageUrl = newsArticle.ImageUrl,
                    PublishedDate = newsArticle.PublishedDate,
                    PublishedTime = newsArticle.PublishedTime,
                    Location = newsArticle.Location,
                    ExpectedAttendees = newsArticle.ExpectedAttendees,
                    Category = newsArticle.Category.ToString(),
                    Author = newsArticle.Author,
                    Tags = newsArticle.TagsList,
                    IsFeatured = newsArticle.IsFeatured,
                    IsTrending = newsArticle.IsTrending,
                    ViewCount = newsArticle.ViewCount,
                    Status = newsArticle.Status.ToString()
                };

                return Ok(new ApiResponse<NewsArticleDto>
                {
                    Success = true,
                    Data = newsDto,
                    Message = "News article updated successfully" + (imageUrl != null ? " with new image" : "")
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating news article with ID: {NewsId}", id);
                return StatusCode(500, new ApiResponse<NewsArticleDto>
                {
                    Success = false,
                    Message = "An error occurred while updating the news article",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // DELETE: api/news/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsArticle(int id)
        {
            try
            {
                var newsArticle = await _context.NewsArticles.FindAsync(id);

                if (newsArticle == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "News article not found"
                    });
                }

                _context.NewsArticles.Remove(newsArticle);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "News article deleted successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting news article with ID: {NewsId}", id);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while deleting the news article",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // PATCH: api/news/{id}/publish
        [HttpPatch("{id}/publish")]
        public async Task<IActionResult> PublishNewsArticle(int id)
        {
            try
            {
                var newsArticle = await _context.NewsArticles.FindAsync(id);

                if (newsArticle == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "News article not found"
                    });
                }

                newsArticle.Status = ContentStatus.Published;
                newsArticle.UpdatedAt = DateTime.UtcNow;
                newsArticle.PublishedDate = DateTime.UtcNow;
                newsArticle.PublishedTime = DateTime.UtcNow.ToString("HH:mm");
                

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "News article published successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing news article with ID: {NewsId}", id);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while publishing the news article",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // PATCH: api/news/{id}/unpublish
        [HttpPatch("{id}/unpublish")]
        public async Task<IActionResult> UnpublishNewsArticle(int id)
        {
            try
            {
                var newsArticle = await _context.NewsArticles.FindAsync(id);

                if (newsArticle == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "News article not found"
                    });
                }

                newsArticle.Status = ContentStatus.Draft;
                newsArticle.UpdatedAt = DateTime.UtcNow;
                newsArticle.PublishedDate = null;
                newsArticle.PublishedTime = null;

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "News article unpublished successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error unpublishing news article with ID: {NewsId}", id);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while unpublishing the news article",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/news/featured
        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedNews()
        {
            try
            {
                var featuredNews = await _context.NewsArticles
                    .Where(n => n.IsFeatured && n.Status == ContentStatus.Published)
                    .OrderByDescending(n => n.PublishedDate ?? DateTime.MinValue)
                    .Take(5)
                    .Select(n => new NewsArticleDto
                    {
                        Id = n.Id,
                        Title = n.Title,
                        Summary = n.Summary,
                        ImageUrl = n.ImageUrl,
                        PublishedDate = n.PublishedDate,
                        PublishedTime = n.PublishedTime,
                        Location = n.Location,
                        ExpectedAttendees = n.ExpectedAttendees,
                        Category = n.Category.ToString(),
                        Author = n.Author,
                        Tags = n.TagsList,
                        IsFeatured = n.IsFeatured,
                        IsTrending = n.IsTrending,
                        ViewCount = n.ViewCount,
                        Status = n.Status.ToString()
                    })
                    .ToListAsync();

                return Ok(new ApiResponse<List<NewsArticleDto>>
                {
                    Success = true,
                    Data = featuredNews,
                    Message = $"Retrieved {featuredNews.Count} featured news articles"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving featured news articles");
                return StatusCode(500, new ApiResponse<List<NewsArticleDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving featured news articles",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // GET: api/news/trending
        [HttpGet("trending")]
        public async Task<IActionResult> GetTrendingNews()
        {
            try
            {
                var trendingNews = await _context.NewsArticles
                    .Where(n => n.IsTrending && n.Status == ContentStatus.Published)
                    .OrderByDescending(n => n.ViewCount)
                    .ThenByDescending(n => n.PublishedDate ?? DateTime.MinValue)
                    .Take(10)
                    .Select(n => new NewsArticleDto
                    {
                        Id = n.Id,
                        Title = n.Title,
                        Summary = n.Summary,
                        ImageUrl = n.ImageUrl,
                        PublishedDate = n.PublishedDate,
                        PublishedTime = n.PublishedTime,
                        Location = n.Location,
                        ExpectedAttendees = n.ExpectedAttendees,
                        Category = n.Category.ToString(),
                        Author = n.Author,
                        Tags = n.TagsList,
                        IsFeatured = n.IsFeatured,
                        IsTrending = n.IsTrending,
                        ViewCount = n.ViewCount,
                        Status = n.Status.ToString()
                    })
                    .ToListAsync();

                return Ok(new ApiResponse<List<NewsArticleDto>>
                {
                    Success = true,
                    Data = trendingNews,
                    Message = $"Retrieved {trendingNews.Count} trending news articles"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving trending news articles");
                return StatusCode(500, new ApiResponse<List<NewsArticleDto>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving trending news articles",
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
} 