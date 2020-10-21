using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyPortfolio.Models;
using MyPortfolio.StorageServices;
using MyPortfolio.ViewModels;

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

            // I use this techniqe because Of SAS key Added to blob when using AzureStorage Service
            // Which varied everytime
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

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel model = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };

            ViewBag.Path = _storageSrvices.Getpath(employee.PhotoPath);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                string fileName = null;

                if (model.Photo != null)
                {
                    fileName = _storageSrvices.UploadFile(model.Photo, model.Photo.FileName);
                    employee.PhotoPath = fileName;
                    _storageSrvices.DeleteFile(model.ExistingPhotoPath);
                }

                _employeeRepository.Update(employee);

                return RedirectToAction("Details", new { id = employee.Id });
            }

            return View();
        }
    }
}