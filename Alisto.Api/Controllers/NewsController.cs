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
    public class NewsController : ControllerBase
    {
        private readonly AlistoDbContext _context;
        private readonly ILogger<NewsController> _logger;

        public NewsController(AlistoDbContext context, ILogger<NewsController> logger)
        {
            _context = context;
            _logger = logger;
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

                // Only show published articles
                query = query.Where(n => n.Status == ContentStatus.Published);

                var totalCount = await query.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var news = await query
                    .OrderByDescending(n => n.PublishedDate)
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
                        ViewCount = n.ViewCount
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
                    .FirstOrDefaultAsync(n => n.Id == id && n.Status == ContentStatus.Published);

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
                    ViewCount = newsArticle.ViewCount
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
        [Authorize]
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
                    ViewCount = newsArticle.ViewCount
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

        // PUT: api/news/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateNewsArticle(int id, [FromBody] CreateNewsRequest request)
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
                newsArticle.PublishedDate = request.PublishedDate;
                newsArticle.PublishedTime = request.PublishedTime;
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
                    ViewCount = newsArticle.ViewCount
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

        // DELETE: api/news/{id}
        [HttpDelete("{id}")]
        [Authorize]
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
        [Authorize]
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

        // GET: api/news/featured
        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedNews()
        {
            try
            {
                var featuredNews = await _context.NewsArticles
                    .Where(n => n.IsFeatured && n.Status == ContentStatus.Published)
                    .OrderByDescending(n => n.PublishedDate)
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
                        ViewCount = n.ViewCount
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
                    .ThenByDescending(n => n.PublishedDate)
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
                        ViewCount = n.ViewCount
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