using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolio.StorageServices
{
    public interface IStorageSrvices
    {
        void UploadFile(IFormFile file, string fileName);

        string Getpath(string fileName);

        void DeleteFile(string fileName);
    }
}