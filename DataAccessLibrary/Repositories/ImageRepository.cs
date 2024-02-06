using CloudinaryDotNet;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly Account cloudinaryAccount;

        public ImageRepository(Account cloudinaryAccount)
        {
            this.cloudinaryAccount = cloudinaryAccount;
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(cloudinaryAccount);
            var uploadFileResult =  await client.UploadAsync(
                new CloudinaryDotNet.Actions.ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    DisplayName = file.FileName
                });

            if (uploadFileResult != null && uploadFileResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadFileResult.SecureUri.ToString();
            }
            return null;
        }
    }
}
