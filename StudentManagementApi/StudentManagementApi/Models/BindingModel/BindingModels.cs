using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementApi.Models.BindingModel
{
    public class LoginBindingModel
    {

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsChecked { get; set; }
    }

    public class UserRolesBindingModel
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }
        public string JsonData { get; set; }
    }
}
