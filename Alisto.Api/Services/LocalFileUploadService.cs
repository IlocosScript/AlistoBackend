using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Alisto.Api.Services
{
    public class LocalFileUploadService : ILocalFileUploadService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LocalFileUploadService> _logger;
        private readonly string _uploadBasePath;
        private readonly string _baseUrl;

        public LocalFileUploadService(IConfiguration configuration, ILogger<LocalFileUploadService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            
            // Get the project root directory (where the .csproj file is located)
            var projectRoot = Directory.GetCurrentDirectory();
            var uploadPath = _configuration["FileUpload:LocalPath"] ?? "uploads";
            
            // Create absolute path for uploads folder in project root
            _uploadBasePath = Path.Combine(projectRoot, uploadPath);
            _baseUrl = _configuration["FileUpload:BaseUrl"] ?? "/uploads";
            
            // Ensure the upload directory and default subfolders exist
            EnsureUploadDirectoryExists();
        }

        public async Task<string?> UploadFileAsync(IFormFile file, string folderName = "uploads")
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    _logger.LogWarning("File is null or empty");
                    return null;
                }

                // Validate file type
                if (!IsValidImageFile(file))
                {
                    _logger.LogWarning("Invalid file type: {ContentType}. Only images are allowed.", file.ContentType);
                    return null;
                }

                // Create folder path - ensure it's a valid folder name
                var safeFolderName = GetSafeFolderName(folderName);
                var folderPath = Path.Combine(_uploadBasePath, safeFolderName);
                
                // Ensure the specific folder exists
                Directory.CreateDirectory(folderPath);

                // Generate unique filename
                var fileName = $"{DateTime.UtcNow:yyyyMMdd_HHmmss}_{Guid.NewGuid()}_{GetSafeFileName(file.FileName)}";
                var filePath = Path.Combine(folderPath, fileName);

                // Save file to disk
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                _logger.LogInformation("File uploaded successfully to local storage: {FilePath}", filePath);

                // Return the public URL
                var publicUrl = $"{_baseUrl}/{safeFolderName}/{fileName}";
                return publicUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file to local storage: {FileName}", file?.FileName);
                return null;
            }
        }

        public async Task<bool> DeleteFileAsync(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    return false;
                }

                // Convert URL to local path
                var localPath = ConvertUrlToLocalPath(filePath);
                if (string.IsNullOrEmpty(localPath))
                {
                    return false;
                }

                if (File.Exists(localPath))
                {
                    File.Delete(localPath);
                    _logger.LogInformation("File deleted from local storage: {FilePath}", localPath);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file from local storage: {FilePath}", filePath);
                return false;
            }
        }

        public async Task<bool> FileExistsAsync(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    return false;
                }

                var localPath = ConvertUrlToLocalPath(filePath);
                return !string.IsNullOrEmpty(localPath) && File.Exists(localPath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking file existence: {FilePath}", filePath);
                return false;
            }
        }

        public async Task<long> GetFileSizeAsync(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    return 0;
                }

                var localPath = ConvertUrlToLocalPath(filePath);
                if (string.IsNullOrEmpty(localPath) || !File.Exists(localPath))
                {
                    return 0;
                }

                var fileInfo = new FileInfo(localPath);
                return fileInfo.Length;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting file size: {FilePath}", filePath);
                return 0;
            }
        }

        private void EnsureUploadDirectoryExists()
        {
            try
            {
                // Create the main uploads directory
                if (!Directory.Exists(_uploadBasePath))
                {
                    Directory.CreateDirectory(_uploadBasePath);
                    _logger.LogInformation("Created main upload directory: {Path}", _uploadBasePath);
                }

                // Create default subfolders for better organization
                var defaultFolders = new[]
                {
                    "news",
                    "users", 
                    "reports",
                    "general"
                };

                foreach (var folder in defaultFolders)
                {
                    var folderPath = Path.Combine(_uploadBasePath, folder);
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                        _logger.LogInformation("Created default upload subfolder: {Folder}", folder);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating upload directory structure: {Path}", _uploadBasePath);
            }
        }

        private string GetSafeFolderName(string folderName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
                return "general";

            // Remove or replace invalid characters for folder names
            var invalidChars = Path.GetInvalidPathChars();
            var safeName = folderName;
            
            foreach (var invalidChar in invalidChars)
            {
                safeName = safeName.Replace(invalidChar, '_');
            }

            // Additional safety checks for common problematic characters
            safeName = safeName.Replace('/', '_')
                              .Replace('\\', '_')
                              .Replace(':', '_')
                              .Replace('*', '_')
                              .Replace('?', '_')
                              .Replace('"', '_')
                              .Replace('<', '_')
                              .Replace('>', '_')
                              .Replace('|', '_');

            // Ensure the folder name is not empty after sanitization
            if (string.IsNullOrWhiteSpace(safeName))
                return "general";

            return safeName.ToLowerInvariant();
        }

        private string GetSafeFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return "file";

            // Remove or replace invalid characters for file names
            var invalidChars = Path.GetInvalidFileNameChars();
            var safeName = fileName;
            
            foreach (var invalidChar in invalidChars)
            {
                safeName = safeName.Replace(invalidChar, '_');
            }

            // Additional safety checks for common problematic characters
            safeName = safeName.Replace('/', '_')
                              .Replace('\\', '_')
                              .Replace(':', '_')
                              .Replace('*', '_')
                              .Replace('?', '_')
                              .Replace('"', '_')
                              .Replace('<', '_')
                              .Replace('>', '_')
                              .Replace('|', '_');

            // Ensure the file name is not empty after sanitization
            if (string.IsNullOrWhiteSpace(safeName))
                return "file";

            return safeName;
        }

        private string? ConvertUrlToLocalPath(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                    return null;

                // Remove the base URL to get the relative path
                if (url.StartsWith(_baseUrl))
                {
                    var relativePath = url.Substring(_baseUrl.Length).TrimStart('/');
                    return Path.Combine(_uploadBasePath, relativePath);
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error converting URL to local path: {Url}", url);
                return null;
            }
        }

        private bool IsValidImageFile(IFormFile file)
        {
            if (file == null || string.IsNullOrEmpty(file.ContentType))
                return false;

            var allowedTypes = new[]
            {
                "image/jpeg",
                "image/jpg",
                "image/png",
                "image/gif",
                "image/webp"
            };

            return allowedTypes.Contains(file.ContentType.ToLower());
        }
    }
} 