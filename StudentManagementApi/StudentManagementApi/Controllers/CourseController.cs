using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementApi.DAL;
using StudentManagementApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ubiety.Dns.Core.Common;
using static StudentManagementApi.Models.ViewModel.ViewModels;

namespace StudentManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _iCourseRepository;
        private readonly IWebHostEnvironment _iWebHostEnvironment;

        public CourseController(ICourseRepository iCourseRepository, IWebHostEnvironment iWebHostEnvironment)
        {
            _iCourseRepository = iCourseRepository;
            _iWebHostEnvironment = iWebHostEnvironment;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var catagoryList = await _iCourseRepository.GetAll();
                return Ok(catagoryList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); ;
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CourseViewModel>> GetById(int id)
        {
            try
            {
                var result = await _iCourseRepository.GetById(id);
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
        public async Task<object> Insert([FromForm] CourseViewModel obj)
        {

            try
            {
                if (obj == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Data Object Missing", null));
                }
                var course = await _iCourseRepository.GetById(obj.Id);
                if (course != null)
                {
                    ModelState.AddModelError("", "Course is already Added.");
                    return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Data Object Missing", null));
                }
                var returnObj = await _iCourseRepository.Insert(obj);
                return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "Data inserted successfully", returnObj));
            }
            catch (Exception)
            {
                return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Error retrieving data from database", null));
            }
        }
        [HttpPut("Update")]
        public async Task<object> Update([FromForm] CourseViewModel obj)
        {
            try
            {

                var course = await _iCourseRepository.GetById(obj.Id);
                if (course == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Error retrieving data from database", null));
                }
                var returnObj = await _iCourseRepository.Update(obj);
                return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "Data updated successfully", null));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from database");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var course = await _iCourseRepository.GetById(id);
                if (course == null)
                {
                    return NotFound();
                }
                await _iCourseRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
