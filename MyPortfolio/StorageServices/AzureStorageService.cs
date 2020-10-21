using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MyPortfolio.StorageServices;
using Azure.Storage.Blobs;
using System;
using Azure.Storage.Sas;
using Azure.Storage;
using Azure.Storage.Blobs.Specialized;
using System.Drawing;

namespace MyPortfolio.Models
{
    public class AzureStorageService : IStorageSrvices
    {
        private readonly IConfiguration _configuration;

        public AzureStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string UploadFile(IFormFile file, string fileName)
        {
            var container = GetContainer();
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
            var newBlob = container.GetBlobClient(uniqueFileName);
            using var fileStream = file.OpenReadStream();
            newBlob.UploadAsync(fileStream).Wait();
            return uniqueFileName;
        }

        public string Getpath(string fileName)
        {
            if (fileName != null)
            {
                var imagesAccesKey = _configuration.GetConnectionString("ImagesAccessKey");
                var storageName = _configuration.GetConnectionString("StorageName");
                StorageSharedKeyCredential key = new StorageSharedKeyCredential(storageName, imagesAccesKey);
                var container = GetContainer();
                var blob = container.GetBlobClient(fileName);
                BlobSasBuilder sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = container.Name,
                    BlobName = blob.Name,
                    Resource = "b",
                };
                sasBuilder.StartsOn = DateTimeOffset.UtcNow;
                sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(1);
                sasBuilder.SetPermissions(BlobContainerSasPermissions.Read);
                string sasToken = sasBuilder.ToSasQueryParameters(key).ToString();
                return $"{container.GetBlockBlobClient(blob.Name).Uri}?{sasToken}";
            }

            return fileName;
        }

        public void DeleteFile(string fileName)
        {
            if (fileName != null)
            {
                var container = GetContainer();
                var blob = container.GetBlobClient(fileName);
                blob.DeleteAsync().Wait();
            }
        }

        private BlobContainerClient GetContainer()
        {
            var connectionString = _configuration.GetConnectionString("AzureBlobStorage");
            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient("employeesimages");
            return containerClient;
        }
    }
}