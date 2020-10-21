using Microsoft.AspNetCore.Http;

namespace MyPortfolio.StorageServices
{
    public interface IStorageSrvices
    {
        string UploadFile(IFormFile file, string fileName);

        string Getpath(string fileName);

        void DeleteFile(string fileName);
    }
}