using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyPortfolio.Models;
using MyPortfolio.StorageServices;
using MyPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace MyPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IStorageSrvices _storageSrvices;

        public HomeController(IEmployeeRepository employeeRepository,
                               IWebHostEnvironment webHostEnvironment,
                               IConfiguration configuration,
                               IStorageSrvices storageSrvices)
        {
            _employeeRepository = employeeRepository;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _storageSrvices = storageSrvices;
        }

        public ViewResult List()
        {
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }

        public ViewResult Details(int id)
        {
            var employee = _employeeRepository.GetEmployee(id);
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Details"
            };

            ViewBag.Path = _storageSrvices.Getpath(employee.PhotoPath);
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;

                if (model.Photo != null)
                {
                    fileName = _storageSrvices.UploadFile(model.Photo, model.Photo.FileName);
                }

                var newEmployee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = fileName
                };

                _employeeRepository.Add(newEmployee);
                return RedirectToAction("Details", new { id = newEmployee.Id });
            }

            return View();
        }

        public RedirectToActionResult Delete(int id)
        {
            var fileName = _employeeRepository.GetEmployee(id).PhotoPath;
            _employeeRepository.Delete(id);
            _storageSrvices.DeleteFile(fileName);
            return RedirectToAction(nameof(List));
        }
    }
}