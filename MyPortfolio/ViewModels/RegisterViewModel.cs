using Microsoft.AspNetCore.Mvc;
using MyPortfolio.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        [ValidEmailDomain(allowedDomain: "alayditech.com", ErrorMessage = "Email domain must be alayditech.com")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [MaxLength(30, ErrorMessage = "City connot exceed 30 characters")]
        public string City { get; set; }
    }
}