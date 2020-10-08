using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolio.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name connot exceed 50 characters")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@alayditech.com",
            ErrorMessage = "Invalid Office Email Format")]
        [Display(Name = "Office Email")]
        public string Email { get; set; }

        public Dept Department { get; set; }
    }
}