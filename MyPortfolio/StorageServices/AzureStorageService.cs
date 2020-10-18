using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using MyPortfolio.StorageServices;
using System;

namespace MyPortfolio.Models
{
    public class AzureStorageService : IStorageSrvices
    {
        private readonly IConfiguration _configuration;

        public AzureStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void UploadFile(IFormFile file, string fileName)
        {
            var container = GetContainer();

            var newBlob = container.GetBlockBlobReference(fileName);
            using var fileStream = file.OpenReadStream();
            newBlob.UploadFromStreamAsync(fileStream).Wait();
        }

        public string Getpath(string fileName)
        {
            if (fileName != null)
            {
                var container = GetContainer();
                var blob = container.GetBlockBlobReference(fileName);
                var sasToken = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy()
                {
                    Permissions = SharedAccessBlobPermissions.Read,
                    SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1)
                });
                var blobUrl = blob.Uri.AbsoluteUri + sasToken;
                return blobUrl;
            }
            return fileName;
        }

        public void DeleteFile(string fileName)
        {
            if (fileName != null)
            {
                var container = GetContainer();
                var blob = container.GetBlockBlobReference(fileName);
                blob.DeleteAsync().Wait();
            }
        }

        private CloudBlobContainer GetContainer()
        {
            var accountName = _configuration.GetSection("Accounts")["AzureStorageAccount"];
            var accountKey = _configuration.GetConnectionString("ImagesAccessKey");
            var storageCredentials = new StorageCredentials(accountName, accountKey);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = cloudBlobClient.GetContainerReference("employeesimages");
            return container;
        }
    }
}