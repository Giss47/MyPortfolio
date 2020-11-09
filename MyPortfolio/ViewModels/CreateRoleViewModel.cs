﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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