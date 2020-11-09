using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }

        public string Id { get; set; }

        [Required]
        [DisplayName("Role Name")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}