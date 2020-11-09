using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.ViewModels
{
    public class CreateRoleViewModel
    {
        [Remote(action: "RoleExists", controller: "Administration")]
        [Required]
        [DisplayName("Role Name")]
        public string RoleName { get; set; }
    }
}