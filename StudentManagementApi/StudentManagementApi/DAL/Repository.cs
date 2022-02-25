using Microsoft.EntityFrameworkCore;
using StudentManagementApi.Models;
using StudentManagementApi.Models.ContextModel;
using StudentManagementApi.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static StudentManagementApi.Models.ViewModel.ViewModels;

namespace StudentManagementApi.DAL
{

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly StudentDbContext _context;
        public DepartmentRepository(StudentDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<ViewModels.DepartmentViewModel>> GetAll()
        {
            IEnumerable<DepartmentViewModel> departmentList = await _context.Departments.Select(e => new DepartmentViewModel
            {
                Id = e.Id,
                DeptName = e.DeptName,

            }).ToListAsync();
            return departmentList;
        }

        public async Task<ViewModels.DepartmentViewModel> GetById(int id)
        {
            Department e = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (e != null)
            {
                DepartmentViewModel department = new DepartmentViewModel
                {
                    Id = e.Id,
                    DeptName = e.DeptName,
                };
                return department;
            }
            return null;
        }

        public async Task<ViewModels.DepartmentViewModel> Insert(ViewModels.DepartmentViewModel e)
        {
            DepartmentViewModel returnObj = new DepartmentViewModel();
            if (e != null)
            {
                Department obj = new Department()
                {
                    DeptName = e.DeptName,

                };
                await _context.Departments.AddAsync(obj);
                await Save();
                returnObj = await GetById(obj.Id);
            }
            return returnObj;
        }
        public async Task<ViewModels.DepartmentViewModel> Update(ViewModels.DepartmentViewModel e)
        {
            var result = await _context.Departments.FirstOrDefaultAsync(h => h.Id == e.Id);
            DepartmentViewModel returnObj = new DepartmentViewModel();
            if (result != null)
            {
                result.Id = e.Id;
                result.DeptName = e.DeptName;

            }
            await Save();
            returnObj = await GetById(result.Id);
            return returnObj;
        }
        public async Task Delete(int id)
        {
            var result = await _context.Departments.FirstOrDefaultAsync(p => p.Id == id);
            if (result != null)
            {
                _context.Departments.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
    public class StudentReposioty : IStudentRepository
    {
        private readonly StudentDbContext _context;
        public StudentReposioty(StudentDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<StudentViewModel>> GetAll()
        {
            IEnumerable<StudentViewModel> StudentList = await _context.Students.Select(e => new StudentViewModel
            {
                Id = e.Id,
                Name = e.Name,
                DeptId = e.DeptId,
                DeptName = e.Department.DeptName,
                DateOfBirth = e.DateOfBirth,
                ImagePath = e.ImagePath

            }).ToListAsync();
            return StudentList;
        }

        public async Task<StudentViewModel> GetById(int id)
        {
            Student p = await _context.Students.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (p != null)
            {
                StudentViewModel student = new StudentViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    DeptId = p.DeptId,
                    DateOfBirth = p.DateOfBirth,
                    ImagePath = p.ImagePath
                };
                return student;
            }
            return null;
        }

        public async Task<StudentViewModel> Insert(StudentViewModel e)
        {
            StudentViewModel returnObj = new StudentViewModel();
            if (e != null)
            {
                Student obj = new Student()
                {
                    Id = e.Id,
                    Name = e.Name,
                    DeptId = e.DeptId,
                    DateOfBirth = e.DateOfBirth,
                    ImagePath = e.ImagePath
                };
                await _context.Students.AddAsync(obj);
                await Save();
                returnObj = await GetById(obj.Id);
            }
            return returnObj;
        }
        public async Task<StudentViewModel> Update(StudentViewModel e)
        {
            var result = await _context.Students.FirstOrDefaultAsync(e => e.Id == e.Id);
            StudentViewModel returnObj = new StudentViewModel();
            if (result != null)
            {
                result.Id = e.Id;
                result.Name = e.Name;
                result.DeptId = e.DeptId;
                result.DateOfBirth = e.DateOfBirth;
                result.ImagePath = e.ImagePath;
            }
            await Save();
            returnObj = await GetById(result.Id);
            return returnObj;
        }
        public async Task<IEnumerable<DepartmentViewModel>> GetAllDepartmentById(int departmentId)
        {
            IEnumerable<DepartmentViewModel> catList = await _context.Departments.Where(a => a.Id == departmentId).Select(e => new DepartmentViewModel
            {
                Id = e.Id,
                DeptName = e.DeptName
            }).ToListAsync();
            return catList;
        }
        public async Task Delete(int id)
        {
            var result = await _context.Students.FirstOrDefaultAsync(p => p.Id == id);
            if (result != null)
            {
                _context.Students.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
    public class CourseRepository : ICourseRepository
    {
        private readonly StudentDbContext _context;
        public CourseRepository(StudentDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<CourseViewModel>> GetAll()
        {
            IEnumerable<CourseViewModel> CourseList = await _context.Courses.Select(e => new CourseViewModel
            {
                Id = e.Id,
                Title = e.Title,
                SeatCount = e.SeatCount,
                Fees = e.Fees
            }).ToListAsync();
            return CourseList;
        }

        public async Task<CourseViewModel> GetById(int id)
        {
            Course p = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (p != null)
            {
                CourseViewModel courses = new CourseViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    SeatCount = p.SeatCount,
                    Fees = p.Fees
                };
                return courses;
            }
            return null;
        }

        public async Task<CourseViewModel> Insert(CourseViewModel e)
        {
            CourseViewModel returnObj = new CourseViewModel();
            if (e != null)
            {
                Course obj = new Course()
                {
                    Id = e.Id,
                    Title = e.Title,
                    SeatCount = e.SeatCount,
                    Fees = e.Fees
                };
                await _context.Courses.AddAsync(obj);
                await Save();
                returnObj = await GetById(obj.Id);
            }
            return returnObj;
        }
        public async Task<CourseViewModel> Update(CourseViewModel e)
        {
            var result = await _context.Courses.FirstOrDefaultAsync(e => e.Id == e.Id);
            CourseViewModel returnObj = new CourseViewModel();
            if (result != null)
            {
                result.Id = e.Id;
                result.Title = e.Title;
                result.SeatCount = e.SeatCount;
                result.Fees = e.Fees;
            }
            await Save();
            returnObj = await GetById(result.Id);
            return returnObj;
        }
        public async Task Delete(int id)
        {
            var result = await _context.Courses.FirstOrDefaultAsync(p => p.Id == id);
            if (result != null)
            {
                _context.Courses.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
    public class StudentRegistrationReposiotry : IStudentRegistrationsRepository
    {
        private readonly StudentDbContext _context;
        public StudentRegistrationReposiotry(StudentDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<StudentRegistrationViewModel>> GetAll()
        {
            IEnumerable<StudentRegistrationViewModel> RegistrationList = await _context.StudentRegistrations.Select(e => new StudentRegistrationViewModel
            {
                RegistrationId = e.RegistrationId,
                StudentId = e.StudentId,
                CourseId=e.CourseId,
                CourseTitle = e.Course.Title,
                StudentName = e.Student.Name,
                EnrollDate = e.EnrollDate,
                IsPaymentComplete = e.IsPaymentComplete
            }).ToListAsync();
            return RegistrationList;
        }
        public async Task<StudentRegistrationViewModel> GetById(int id)
        {
            StudentRegistration p = await _context.StudentRegistrations.AsNoTracking().FirstOrDefaultAsync(p => p.RegistrationId == id);
            if (p != null)
            {
                StudentRegistrationViewModel studentResig = new StudentRegistrationViewModel
                {
                    RegistrationId = p.RegistrationId,
                    StudentId = p.StudentId,
                    CourseId=p.CourseId,
                    EnrollDate = p.EnrollDate,
                    IsPaymentComplete = p.IsPaymentComplete
                };
                return studentResig;
            }
            return null;
        }

        public async Task<StudentRegistrationViewModel> Insert(StudentRegistrationViewModel e)
        {
            StudentRegistrationViewModel returnObj = new StudentRegistrationViewModel();
            if (e != null)
            {
                StudentRegistration obj = new StudentRegistration()
                {
                    RegistrationId = e.RegistrationId,
                    StudentId = e.StudentId,
                    CourseId=e.CourseId,
                    EnrollDate = e.EnrollDate,
                    IsPaymentComplete = e.IsPaymentComplete
                };
                await _context.StudentRegistrations.AddAsync(obj);
                await Save();
                returnObj = await GetById(obj.RegistrationId);
            }
            return returnObj;
        }
        public async Task<StudentRegistrationViewModel> Update(StudentRegistrationViewModel e)
        {
            var result = await _context.StudentRegistrations.FirstOrDefaultAsync(e => e.RegistrationId == e.RegistrationId);
            StudentRegistrationViewModel returnObj = new StudentRegistrationViewModel();
            if (result != null)
            {
                result.RegistrationId = e.RegistrationId;
                result.StudentId = e.StudentId;
                result.CourseId = e.CourseId;
                result.EnrollDate = e.EnrollDate;
                result.IsPaymentComplete = e.IsPaymentComplete;
            }
            await Save();
            returnObj = await GetById(result.RegistrationId);
            return returnObj;
        }
        public async Task<IEnumerable<CourseViewModel>> GetAllCourseById(int courseId)
        {
            IEnumerable<CourseViewModel> courseList = await _context.Courses.Where(a => a.Id == courseId).Select(e => new CourseViewModel
            {
                Id = e.Id,
                Title = e.Title               
            }).ToListAsync();
            return courseList;
        }

        public async Task<IEnumerable<StudentViewModel>> GetAllStudentById(int studentId)
        {
            IEnumerable<StudentViewModel> studentList = await _context.Students.Where(a => a.Id == studentId).Select(e => new StudentViewModel
            {
                Id = e.Id,
                Name = e.Name
            }).ToListAsync();
            return studentList;
        }

        public async Task Delete(int id)
        {
            var result = await _context.StudentRegistrations.FirstOrDefaultAsync(p => p.RegistrationId == id);
            if (result != null)
            {
                _context.StudentRegistrations.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
