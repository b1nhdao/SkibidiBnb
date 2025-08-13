using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using SkibidiBnb.Application.Common;
using SkibidiBnb.Application.SharedServices.UploadCloud;

namespace SkibidiBnb.Infrastructure.ExternalServices
{
    public class CloudinaryUploadCloudService(Cloudinary cloudinary) : IUploadCloudService
    {
        private readonly Cloudinary _cloudinary = cloudinary;

        public Task<Result<bool>> DeleteFileAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> UploadFileAsync(IFormFile? file)
        {
            if(file == null || file.Length == 0)
            {
                return Result<string>.Failure("File is null or empty.");
            }
            using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream),
                PublicId = Path.GetFileNameWithoutExtension(file.FileName),
                Folder = "skibidi-bnb",
                Overwrite = true,
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            if (uploadResult.Error != null)
                throw new Exception(uploadResult.Error.Message);

            return Result<string>.Success(uploadResult.SecureUrl.AbsoluteUri);
        }

        public async Task<Result<IEnumerable<string>>> UploadMultipleFilesAsync(IFormFileCollection files)
        {
            if (files == null || files.Count == 0)
            {
                return Result<IEnumerable<string>>.Success([]);
            }
            var uploadTasks = files.Select(file => UploadFileAsync(file));
            var results = await Task.WhenAll(uploadTasks);
            return Result<IEnumerable<string>>.Success(results.Where(r => r.IsSuccess).Select(r => r.Value));
        }
    }
}
