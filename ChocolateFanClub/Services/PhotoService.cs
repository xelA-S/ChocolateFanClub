using ChocolateFanClub.Interfaces;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using ChocolateFanClub.Helpers;

namespace ChocolateFanClub.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        // this brings in the cloudinary settings
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            // this creates a new account
            var acc = new Account(
                // this is based on the values set in the cloudinary settings
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            // this creates a cloudinary account based on the values above
            _cloudinary = new Cloudinary(acc);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            // checks if file exists
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    // dimensions and transformations of uploaded image can be specified here
                    Transformation = new Transformation().Height(500).Width(500)

                };
                // this uploads the image
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}
