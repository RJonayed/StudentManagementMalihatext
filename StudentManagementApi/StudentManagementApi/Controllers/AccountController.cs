using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentManagementApi.DAL.Seeds;
using StudentManagementApi.Helper;
using StudentManagementApi.Models;
using StudentManagementApi.Models.BindingModel;
using StudentManagementApi.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core.Common;

namespace StudentManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginViewModel> _logger;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IWebHostEnvironment _iWebHostEnvironment;
        private readonly JWTConfig _jWTConfig;
        public AccountController(IOptions<JWTConfig> jwtConfig, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, ILogger<LoginViewModel> logger, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment iWebHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _jWTConfig = jwtConfig.Value;
            _iWebHostEnvironment = iWebHostEnvironment;
        }
        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public async Task<object> RegisterUser([FromForm] UserViewModel objModel)
        {
            try
            {
                string uniqueImageName = "";
                if (objModel.Photo != null)
                {
                    string uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "images/user_images");
                    uniqueImageName = Guid.NewGuid().ToString() + "_" + objModel.Photo.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueImageName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    objModel.Photo.CopyTo(fileStream);
                    fileStream.Close();
                    objModel.ImageName = uniqueImageName;
                }
                var user = new ApplicationUser
                {
                    FirstName = objModel.FirstName,
                    LastName = objModel.LastName,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    UserName = objModel.UserName,
                    PhoneNumber = objModel.Phone,
                    Email = objModel.Email,
                    ImageName = objModel.ImageName,
                };
                var result = await _userManager.CreateAsync(user, objModel.Password);
                if (result.Succeeded)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "User has been Registered", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "", result.Errors.Select(x => x.Description).ToArray()));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCodes.Error, ex.Message, null));
            }
        }
        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<object> SignIn([FromBody] LoginBindingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.IsChecked, false);
                    if (result.Succeeded)
                    {
                        var appUser = await _userManager.FindByNameAsync(model.UserName);
                        var roles = (await _userManager.GetRolesAsync(appUser)).ToList();
                        var user = new LoginViewModel
                        {
                            FirstName = appUser.FirstName,
                            LastName = appUser.LastName,
                            DateCreated = DateTime.UtcNow,
                            DateModified = DateTime.UtcNow,
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            ImageName = appUser.ImageName,
                        };
                        user.Token = GenerateToken(appUser, roles);

                        return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "", user));

                    }
                }

                return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "invalid Email or password", null));

            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCodes.Error, ex.Message, null));
            }
        }
        [HttpGet("GetAllUser")]
        public async Task<object> GetAllUser()
        {
            try
            {
                List<ApplicationUser> allUserDTO = new List<ApplicationUser>();
                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    allUserDTO.Add(new ApplicationUser(
                        user.Id,
                        user.FirstName,
                        user.LastName,
                        user.UserName,
                        user.Email,
                        user.PhoneNumber,
                        user.DateCreated,
                        user.DateModified,
                        user.ImageName
                        ));
                }
                return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "", allUserDTO));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCodes.Error, ex.Message, null));
            }
        }
        [HttpDelete("DeleteUser")]
        public async Task<object> DeleteUser(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }
                if (user.UserName == "admin")
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Admin Can Not be Deleted", null));
                }             
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "Users Deleted succfully", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Something went wrong please try again later", null));
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from database");
            }
        }
        [HttpGet("GetRoles")]
        public async Task<object> GetRoles()
        {
            try
            {
                var roles = _roleManager.Roles.ToList();
                return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "", roles));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCodes.Error, ex.Message, null));
            }
        }
        [HttpPost("AddRole")]
        public async Task<object> AddRole([FromForm] UserRolesViewModel model)
        {
            try
            {
                if (model.RoleName == null || model.RoleName == "")
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Parameters are missing", null));

                }
                if (await _roleManager.RoleExistsAsync(model.RoleName))
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Role already exist", null));

                }
                var result = await _roleManager.CreateAsync(new ApplicationRole(model.RoleName.Trim()));
                if (result.Succeeded)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "Role added successfully", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "something went wrong please try again later", null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCodes.Error, ex.Message, null));
            }
        }
        [HttpPut("EditRole")]
        public async Task<object> EditRole([FromForm] UserRolesViewModel model)
        {
            try
            {               
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                if (role == null)
                {
                    return NotFound();
                }
                if (role.Name == "Admin")
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Admin Role can not be updated!", null));
                }
                if (await _roleManager.RoleExistsAsync(model.RoleName))
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Role already exist", null));

                }
                role.Name = model.RoleName;
                role.JsonData = role.JsonData;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "Role updated succfully!", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Something went wrong please try again later", null));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from database");
            }
        }
        [HttpPost("DeleteRole")]
        public async Task<object> DeleteRole([FromForm] UserRolesViewModel model)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                if (role == null)
                {
                    return NotFound();
                }

                if (role.Name == "Admin")
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Admin Role can not be deleted!", null));
                }
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "Role deleted succfully!", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Something went wrong please try again later", null));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from database");
            }
        }
        [HttpGet("UserRoles")]
        public async Task<object> UserRoles(string id)
        {
            try
            {
                var viewModel = new List<UserRolesViewModel>();
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                foreach (var role in _roleManager.Roles)
                {
                    var userRolesViewModel = new UserRolesViewModel
                    {
                        RoleName = role.Name
                    };
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        userRolesViewModel.Selected = true;
                    }
                    else
                    {
                        userRolesViewModel.Selected = false;
                    }
                    viewModel.Add(userRolesViewModel);
                }
                var model = new ManageUserRolesViewModel()
                {
                    UserId = id,
                    UserRoles = viewModel
                };
                return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "", model));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from database");
            }

        }

        [HttpPut("UpdateUserRoles")]
        public async Task<object> UpdateUserRoles(string userId, ManageUserRolesViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }
                var roles = await _userManager.GetRolesAsync(user);
                if (user == null)
                {
                    return NotFound();
                }
                var result = await _userManager.RemoveFromRolesAsync(user, roles);
                if (result.Succeeded)
                {
                    result = await _userManager.AddToRolesAsync(user, model.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
                    var currentUser = await _userManager.GetUserAsync(User);
                    await _signInManager.RefreshSignInAsync(currentUser);
                    UserRoleDataInitilizer.SeedData(_userManager, _roleManager);
                    return await Task.FromResult(new ResponseModel(ResponseCodes.OK, "Users roles updated succfully", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCodes.Error, "Something went wrong please try again later", null));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from database");
            }
        }
        private string GenerateToken(ApplicationUser user, List<string> roles)
        {
            var claims = new List<Claim>(){
               new Claim(JwtRegisteredClaimNames.NameId,user.Id),
               new Claim(JwtRegisteredClaimNames.Email,user.Email),
               new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
           };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jWTConfig.Audience,
                Issuer = _jWTConfig.Issuer
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}

