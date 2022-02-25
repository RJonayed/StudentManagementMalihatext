using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementApi.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName) : base(roleName) { }

        public ApplicationRole(string roleName, string jsonData) : base(roleName)
        {
            base.Name = roleName;

            this.JsonData = jsonData;
        }

        [Display(Name = "JsonData")]
        public string JsonData { get; set; }
    }
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() { }

        public ApplicationUser(string userName) : base(userName) { }
        public ApplicationUser(string userId, string firstName, string lastName, string userName, string Email, string PhoneNumber, DateTime dateCreated, DateTime dateModified, string imageName) : base(userName)
        {
            this.Id = userId;
            this.FirstName = firstName;
            base.UserName = userName;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.DateCreated = dateCreated;
            this.DateModified = dateModified;
            this.ImageName = imageName;
        }
        [Required, MaxLength(80)]
        public string FirstName { get; set; }

        [Required, MaxLength(80)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime DateModified { get; set; }
        public string ImageName { get; set; }
    }
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string DeptName { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        public int DeptId { get; set; }
        public string DateOfBirth { get; set; }
        public string ImagePath { get; set; }
        [ForeignKey("DeptId")]
        public virtual Department Department { get; set; }
        public virtual ICollection<StudentRegistration>  StudentRegistrations { get; set; }
    }
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public int SeatCount { get; set; }
        [Required]
        public int Fees { get; set; }
        public virtual ICollection<StudentRegistration> StudentRegistrations { get; set; }
    }
    public class StudentRegistration
    {
        [Key]
        public int RegistrationId { get; set; }
        public int StudentId { get; set; }     
        public int CourseId { get; set; }
        [Required]
        public string EnrollDate { get; set; }
        public bool IsPaymentComplete { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
}
