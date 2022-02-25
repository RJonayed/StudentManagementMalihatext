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
    public class ResigstrationController : ControllerBase
    {
        private readonly IStudentRegistrationsRepository _iStudentRegistrationsRepository;


        public ResigstrationController(IStudentRegistrationsRepository iStudentRegistrationsRepository)
        {
            this._iStudentRegistrationsRepository = iStudentRegistrationsRepository;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var resigtrationList = await _iStudentRegistrationsRepository.GetAll();
                return Ok(resigtrationList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); ;
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<StudentRegistrationViewModel>> GetById(int id)
        {
            try
            {
                var result = await _iStudentRegistrationsRepository.GetById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("Insert")]
        public async Task<object> Insert([FromForm] StudentRegistrationViewModel obj)
        {

            try
            {
                
                if (obj == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Data Object Missing", null));
                }
                var resigtration = await _iStudentRegistrationsRepository.GetById(obj.RegistrationId);
                if (resigtration != null)
                {
                    ModelState.AddModelError("", "Resigtration is already Added.");
                    return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Data Object Missing", null));
                }
                var returnObj = await _iStudentRegistrationsRepository.Insert(obj);
                return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "Data inserted successfully", returnObj));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCodes.Error, ex.Message, null));
            }
        }
        [HttpPut("Update")]
        public async Task<object> Update([FromForm] StudentRegistrationViewModel obj)
        {
            try
            {
                var resigtration = await _iStudentRegistrationsRepository.GetById(obj.RegistrationId);
                if (resigtration == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Error retrieving data from database", null));
                }
                var returnObj = await _iStudentRegistrationsRepository.Update(obj);

                return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "Data updated successfully", null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var resigtration = await _iStudentRegistrationsRepository.GetById(id);
                if (resigtration == null)
                {
                    return NotFound();
                }
                await _iStudentRegistrationsRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetStudentById")]
        public async Task<ActionResult> GetStudentById(int id)
        {
            try
            {
                var result = await _iStudentRegistrationsRepository.GetAllStudentById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetAllCourseById")]
        public async Task<ActionResult> GetAllCourseById(int id)
        {
            try
            {
                var suppList = await _iStudentRegistrationsRepository.GetAllCourseById(id);
                return Ok(suppList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); ;
            }
        }
    }
}
