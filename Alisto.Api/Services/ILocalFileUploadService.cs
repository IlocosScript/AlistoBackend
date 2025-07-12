using Microsoft.AspNetCore.Http;

namespace Alisto.Api.Services
{
    public interface ILocalFileUploadService
    {
        Task<string?> UploadFileAsync(IFormFile file, string folderName = "uploads");
        Task<bool> DeleteFileAsync(string filePath);
        Task<bool> FileExistsAsync(string filePath);
        Task<long> GetFileSizeAsync(string filePath);
    }
} 