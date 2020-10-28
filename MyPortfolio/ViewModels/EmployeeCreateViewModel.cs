using Microsoft.AspNetCore.Http;
using MyPortfolio.CustomValidationAttributes;
using MyPortfolio.Models;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name connot exceed 50 characters")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@alayditech.com",
            ErrorMessage = "Email domain must be alayditech.com")]
        [Display(Name = "Office Email")]
        public string Email { get; set; }

        [Required]
        public Dept? Department { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [MaxFileSize(1000000)]
        public IFormFile Photo { get; set; }
    }
}