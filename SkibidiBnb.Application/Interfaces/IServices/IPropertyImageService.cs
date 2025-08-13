using Microsoft.AspNetCore.Http;
using SkibidiBnb.Application.Common;

namespace SkibidiBnb.Application.Interfaces.IServices
{
    public interface IPropertyImageService
    {
        Task<Result<IEnumerable<string>>> UploadMultipleAsync(IFormFileCollection files);
        Task<Result<string>> UploadSingleAsync(IFormFile file);
        Task<Result<bool>> DeleteAsync(string imageUrl);
    }
}
