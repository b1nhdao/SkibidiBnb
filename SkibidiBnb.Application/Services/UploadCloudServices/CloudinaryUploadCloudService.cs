using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace SkibidiBnb.Application.Services.UploadCloudServices
{
    public class CloudinaryUploadCloudService : IUploadCloudService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryUploadCloudService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public Task DeleteFileAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadFileAsync(IFormFile? file)
        {
            if(file == null || file.Length == 0)
            {
                return "";
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

            return uploadResult.SecureUrl.AbsoluteUri;
        }

        public Task<ICollection<string>> UploadFilesAsync(IFormFileCollection files)
        {
            if (files == null || files.Count == 0)
            {
                return Task.FromResult<ICollection<string>>(new List<string>());
            }
            var uploadTasks = files.Select(file => UploadFileAsync(file));
            return Task.WhenAll(uploadTasks).ContinueWith(t => (ICollection<string>)t.Result);
        }
    }
}
