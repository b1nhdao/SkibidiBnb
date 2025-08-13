using Microsoft.AspNetCore.Http;

namespace SkibidiBnb.Application.Services.UploadCloudServices
{
    public interface IUploadCloudService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<ICollection<string>> UploadFilesAsync(IFormFileCollection files);
        Task DeleteFileAsync(string filePath);
    }
}
