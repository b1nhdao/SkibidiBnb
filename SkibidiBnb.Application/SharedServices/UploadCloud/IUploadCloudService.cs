using Microsoft.AspNetCore.Http;
using SkibidiBnb.Application.Common;

namespace SkibidiBnb.Application.SharedServices.UploadCloud
{
    public interface IUploadCloudService
    {
        Task<Result<string>> UploadFileAsync(IFormFile file);
        Task<Result<IEnumerable<string>>> UploadMultipleFilesAsync(IFormFileCollection files);
        Task<Result<bool>> DeleteFileAsync(string fileUrl);
    }
}
