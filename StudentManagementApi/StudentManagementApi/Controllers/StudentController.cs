using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementApi.DAL;
using StudentManagementApi.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static StudentManagementApi.Models.ViewModel.ViewModels;

namespace StudentManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _iStudentRepository;
        private readonly IWebHostEnvironment _iWebHostEnvironment;

        public StudentController(IStudentRepository iStudentRepository, IWebHostEnvironment iWebHostEnvironment)
        {
            _iStudentRepository = iStudentRepository;
            _iWebHostEnvironment = iWebHostEnvironment;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var StudentList = await _iStudentRepository.GetAll();
                return Ok(StudentList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); ;
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<StudentViewModel>> GetById(int id)
        {
            try
            {
                var result = await _iStudentRepository.GetById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from database");
            }
        }
        [HttpPost("Insert")]
        public async Task<object> Insert([FromForm] StudentViewModel obj)
        {

            try
            {
                string uniqueImageName = "";

                if (obj.Photo != null)
                {
                    string uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "Images/student_Images");
                    uniqueImageName = Guid.NewGuid().ToString() + "_" + obj.Photo.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueImageName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    obj.Photo.CopyTo(fileStream);
                    fileStream.Close();
                    obj.ImagePath = uniqueImageName;
                }
                if (obj == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Data Object Missing", null));
                }
                var student = await _iStudentRepository.GetById(obj.Id);
                if (student != null)
                {
                    ModelState.AddModelError("", "Student is already Added.");
                    return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Data Object Missing", null));
                }
                var returnObj = await _iStudentRepository.Insert(obj);
                return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "Data inserted successfully", returnObj));
            }
            catch (Exception)
            {
                return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Error retrieving data from database", null));
            }
        }
        [HttpPut("Update")]
        public async Task<object> Update([FromForm] StudentViewModel obj)
        {
            try
            {
                string uniqueImageName = "";
                if (obj.Id > 0)
                {
                    if (obj.Photo != null)
                    {
                        string uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "Images/student_Images");
                        if (obj.ImagePath != null)
                        {
                            DeleteExistingImage(Path.Combine(uploadFolder, obj.ImagePath));
                        }
                        uniqueImageName = Guid.NewGuid().ToString() + "_" + obj.Photo.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueImageName);
                        FileStream fileStream = new FileStream(filePath, FileMode.Create);
                        obj.Photo.CopyTo(fileStream);
                        fileStream.Close();
                        obj.ImagePath = uniqueImageName;
                    }

                }

                var student = await _iStudentRepository.GetById(obj.Id);
                if (student == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Error retrieving data from database", null));
                }
                var returnObj = await _iStudentRepository.Update(obj);
                return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "Data updated successfully", null));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from database");
            }
        }

        private void DeleteExistingImage(string imagePath)
        {
            FileInfo fileObj = new FileInfo(imagePath);
            if (fileObj.Exists)
            {
                fileObj.Delete();
            }

        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var student = await _iStudentRepository.GetById(id);
                if (student == null)
                {
                    return NotFound();
                }
                await _iStudentRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetAllDepartmentById")]
        public async Task<ActionResult> GetAllDepartmentById(int id)
        {
            try
            {
                var departmentList = await _iStudentRepository.GetAllDepartmentById(id);
                return Ok(departmentList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); ;
            }
        }
    }
}
