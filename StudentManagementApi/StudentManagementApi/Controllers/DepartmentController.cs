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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _iDepartmentRepository;
        private readonly IWebHostEnvironment _iWebHostEnvironment;

        public DepartmentController(IDepartmentRepository iDepartmentRepository, IWebHostEnvironment iWebHostEnvironment)
        {
            _iDepartmentRepository = iDepartmentRepository;
            _iWebHostEnvironment = iWebHostEnvironment;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var catagoryList = await _iDepartmentRepository.GetAll();
                return Ok(catagoryList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); ;
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DepartmentViewModel>> GetById(int id)
        {
            try
            {
                var result = await _iDepartmentRepository.GetById(id);
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
        public async Task<object> Insert([FromBody] DepartmentViewModel obj)
        {

            try
            {
                if (obj == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "Data Object Missing", null));
                }
                var department = await _iDepartmentRepository.GetById(obj.Id);
                if (department != null)
                {
                    ModelState.AddModelError("", "Department is already Added.");
                    return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "Data Object Missing", null));
                }
                var returnObj = await _iDepartmentRepository.Insert(obj);
                return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "Data inserted successfully", returnObj));
            }
            catch (Exception)
            {
                return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Error retrieving data from database", null));
            }
        }
        [HttpPut("Update")]
        public async Task<object> Update([FromBody] DepartmentViewModel obj)
        {
            try
            {

                var department = await _iDepartmentRepository.GetById(obj.Id);
                if (department == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Error retrieving data from database", null));
                }
                var returnObj = await _iDepartmentRepository.Update(obj);
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
                var department = await _iDepartmentRepository.GetById(id);
                if (department == null)
                {
                    return NotFound();
                }
                await _iDepartmentRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
