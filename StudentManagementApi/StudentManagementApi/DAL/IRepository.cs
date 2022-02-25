using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static StudentManagementApi.Models.ViewModel.ViewModels;

namespace StudentManagementApi.DAL
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentViewModel>> GetAll();
        Task<DepartmentViewModel> GetById(int id);
        Task<DepartmentViewModel> Insert(DepartmentViewModel e);
        Task<DepartmentViewModel> Update(DepartmentViewModel e);
        Task Delete(int id);
        Task Save();
    }
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentViewModel>> GetAll();
        Task<StudentViewModel> GetById(int id);
        Task<IEnumerable<DepartmentViewModel>> GetAllDepartmentById(int departmentId);
        Task<StudentViewModel> Insert(StudentViewModel e);
        Task<StudentViewModel> Update(StudentViewModel e);
        Task Delete(int id);
        Task Save();
    }
    public interface ICourseRepository
    {
        Task<IEnumerable<CourseViewModel>> GetAll();
        Task<CourseViewModel> GetById(int id);
        Task<CourseViewModel> Insert(CourseViewModel e);
        Task<CourseViewModel> Update(CourseViewModel e);
        Task Delete(int id);
        Task Save();
    }
    public interface IStudentRegistrationsRepository
    {
        Task<IEnumerable<StudentRegistrationViewModel>> GetAll();
        Task<StudentRegistrationViewModel> GetById(int id);
        Task<IEnumerable<CourseViewModel>> GetAllCourseById(int courseId);
        Task<IEnumerable<StudentViewModel>> GetAllStudentById(int studentId);
        Task<StudentRegistrationViewModel> Insert(StudentRegistrationViewModel e);
        Task<StudentRegistrationViewModel> Update(StudentRegistrationViewModel e);
        Task Delete(int id);
        Task Save();
    }
}
