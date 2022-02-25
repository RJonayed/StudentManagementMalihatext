using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementApi.Models.ViewModel
{
    public class ViewModels
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
        public class DepartmentViewModel
        {
            [Key]
            public int Id { get; set; }
            [Required, MaxLength(30)]
            public string DeptName { get; set; }
        }
        public class StudentViewModel
        {
            [Key]
            public int Id { get; set; }
            [Required, MaxLength(30)]
            public string Name { get; set; }
            [ForeignKey("Id")]
            public int DeptId { get; set; }
            public string DeptName { get; set; }
            public string DateOfBirth { get; set; }
            public string ImagePath { get; set; }
            public IFormFile Photo { get; set; }
        }
        public class CourseViewModel
        {
            [Key]
            public int Id { get; set; }
            [Required, MaxLength(50)]
            public string Title { get; set; }
            [Required]
            public int SeatCount { get; set; }
            [Required]
            public int Fees { get; set; }
        }
        public class StudentRegistrationViewModel
        {
            [Key]
            public int RegistrationId { get; set; }
            [Required]
            public string EnrollDate { get; set; }
            public bool IsPaymentComplete { get; set; }
            public int StudentId { get; set; }
            public int CourseId { get; set; }
            public string  StudentName { get; set; }
            public string CourseTitle { get; set; }
        }
    }
}
