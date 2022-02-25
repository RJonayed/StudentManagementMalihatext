using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementApi.Models.ViewModel
{
    public class UserViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string UserName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }

        public string ImageName { get; set; }
        public bool IsChecked { get; set; }
        public IFormFile Photo { get; set; }
    }
    public class LoginViewModel
    {
        [Required]

        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool IsChecked { get; set; }

        [Required, MaxLength(80)]
        public string FirstName { get; set; }

        [Required, MaxLength(80)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime DateModified { get; set; }
        public string ImageName { get; set; }

        public string Token { get; set; }
    }
    public class UserRolesViewModel
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }
        public string JsonData { get; set; }
        public bool Selected { get; set; }
        public List<string> Users { get; set; }
    }
    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }
        public IList<UserRolesViewModel> UserRoles { get; set; }
    }
    public class UserWiseRoleViewModel
    {
        [Display(Name = "User  Id")]
        [Key]
        public int uwrId { get; set; }

        [Display(Name = "Role Id")]
        public int RoleId { get; set; }

        [Display(Name = "User Id")]
        public int UserId { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "User Image")]
        public string ImageName { get; set; }

        [Display(Name = "User Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }
    }
}
