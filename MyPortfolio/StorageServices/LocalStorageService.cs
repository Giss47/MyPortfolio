using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace MyPortfolio.StorageServices
{
    public class LocalStorageService : IStorageSrvices
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string UploadFile(IFormFile file, string fileName)
        {
            string uploadsfodler = Path.Combine(_webHostEnvironment.WebRootPath, "images/employees");
            var uniquefilename = Guid.NewGuid().ToString() + "_" + fileName;
            string filePath = Path.Combine(uploadsfodler, uniquefilename);
            using FileStream filestream = new FileStream(filePath, FileMode.CreateNew);
            file.CopyTo(filestream);
            return uniquefilename;
        }

        public string Getpath(string fileName)
        {
            if (fileName != null)
            {
                string uploadsfodler = Path.Combine(_webHostEnvironment.WebRootPath, "/images/employees");
                string filePath = Path.Combine(uploadsfodler, fileName);
                return filePath;
            }
            return fileName;
        }

        public void DeleteFile(string fileName)
        {
            if (fileName != null)
            {
                string uploadsfodler = Path.Combine(_webHostEnvironment.WebRootPath, "images/employees");
                string filePath = Path.Combine(uploadsfodler, fileName);
                File.Delete(filePath);
            }
        }
    }
}