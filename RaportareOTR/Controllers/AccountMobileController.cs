using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using RaportareOTR.Data;
using RaportareOTR.Models;
using RaportareOTR.Models.AccountViewModels;
using RaportareOTR.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using RaportareOTR.Controllers.Resources;
using Microsoft.AspNetCore.Authentication;
using RaportareOTR.Controllers;
using System;
using System.Security.Claims;
using Newtonsoft.Json;
using RaportareOTR.CommonCode.Authentication;
using System.Collections;
using System.Linq;
using ECV.Controllers.Resources;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using RaportareOTR.CommonCode;
using Microsoft.AspNetCore.Http;
using RaportareOTR.Models.ManageViewModels.Users;
using Microsoft.EntityFrameworkCore;
using SIGAD.CommonCode.Validation;
using SIGAD.Models.AccountViewModels;

namespace RaportareOTR.Controllers
{
    public class AccountMobileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        private readonly IJwtFactory _jwtFactory;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly JwtIssuerOptions _jwtOptions;

        public AccountMobileController(
            ApplicationDbContext context,
            IMapper _mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
            this._mapper = _mapper;

            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        [HttpPost("/api/login")]
        public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(credentials.UserName, credentials.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    ApplicationUser user = _context.Users.SingleOrDefault(c => c.UserName == credentials.UserName);

                    var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
                    if (identity == null)
                    {
                        return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
                    }

                    var rolesForUser = await _userManager.GetRolesAsync(user);
                    string userRole = "";

                    if (rolesForUser.Count > 0)
                        userRole = rolesForUser.ElementAt(0);
                    else
                        userRole = "";

                    var response = new
                    {
                        id = identity.Claims.Single(c => c.Type == "id").Value,
                        auth_token = await _jwtFactory.GenerateEncodedToken(credentials.UserName, identity),
                        expires_in = (int)_jwtOptions.ValidFor.TotalSeconds,
                        email = user.Email,
                        username = user.UserName,
                        role = userRole
                    };

                    var json = JsonConvert.SerializeObject(response, _serializerSettings);
                    return new OkObjectResult(json);
                }
                else
                {
                    return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
                }
            }
            catch (Exception e)
            {
                await Error.Add(e, _context);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("/api/register")]
        public async Task<IActionResult> Register([FromBody]RegisterMobileResource model)
        {
            try
            {
                List<ValidationReturn> validationReturn = await ApplicationUser.Validate(_context, model, false);

                foreach (var valid in validationReturn)
                    ModelState.AddModelError(valid.FieldName, valid.Message);

                if (ModelState.IsValid)
                { 
                    var user = new ApplicationUser { UserName = model.Username, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var result1 = _userManager.AddToRoleAsync(user, model.Userclass);

                        if (result1.Result.Succeeded)
                        {
                            _logger.LogInformation("User created a new account with password.");
                            
                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        }
                        else
                        {
                            await Error.Add(new Exception("Could not create user role!"), _context);

                            return StatusCode(StatusCodes.Status500InternalServerError);
                        }
                    }
                    else
                    {
                        AddErrors(result);

                        return BadRequest(ModelState);
                    }

                    return Ok(true);
                }

                // If we got this far, something failed, redisplay form
                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                await Error.Add(e, _context);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("/logout")]
        public async Task<IActionResult> Logout([FromBody]CredentialsViewModel credentials)
        {
            try
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation("User logged out.");
                return Ok(true);
            }
            catch (Exception e)
            {
                await Error.Add(e, _context);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("/api/isAuth")]
        public async Task<bool> IsAuth()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                await Error.Add(e, _context);

                return false;
            }

        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                {
                    // get the user to verifty
                    var userToVerify = await _userManager.FindByNameAsync(userName);

                    ApplicationUser user = _context.Users.SingleOrDefault(c => c.UserName == userName);

                    // assign the user
                    UsersToShow newUserToShow = new UsersToShow();
                    newUserToShow.User = user;

                    // one user can only have one Role. The app is biult like that so take its Role
                    var rolesForUser = await _userManager.GetRolesAsync(user);

                    if (rolesForUser.Count > 0)
                        newUserToShow.Role = rolesForUser.ElementAt(0);
                    else
                        newUserToShow.Role = "";

                    if (userToVerify != null)
                    {
                        // check the credentials  
                        if (await _userManager.CheckPasswordAsync(user, password))
                        {
                            //return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, user.Id));
                            return _jwtFactory.GenerateClaimsIdentity(userName, newUserToShow.Role, user.Id);
                        }
                    }
                }

                // Credentials are invalid, or account doesn't exist
                return await Task.FromResult<ClaimsIdentity>(null);
            }
            catch (Exception e)
            {
                await Error.Add(e, _context);

                return new ClaimsIdentity();
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }


        [HttpDelete("/api/DeleteUser/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                bool zeRezult = await ApplicationUser.Delete(_context, id);

                if (!zeRezult)
                    return NotFound();
                else
                    return Ok(zeRezult);
            }
            catch (Exception e)
            {
                await Error.Add(e, _context);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("/api/GetUserClasses")]
        public async Task<QueryResultResource<string>> GetUserClasses()
        {
            try
            {
                var result = new QueryResultResource<string>();

                var zeRezult = RoleNames.Build();

                result.TotalItems = zeRezult.Count();
                result.Items = zeRezult;

                return result;
            }
            catch (Exception e)
            {
                await Error.Add(e, _context);

                return new QueryResultResource<string>();
            }
        }

        [HttpGet("/api/GetLogeddInUserInfo")]
        public async Task<IActionResult> GetLogeddInUserInfo()
        {
            try
            {
                ApplicationUser user = _context.Users.SingleOrDefault(c => c.UserName == User.Identity.Name);

                if (user != null)
                {
                    var rolesForUser = await _userManager.GetRolesAsync(user);
                    string userRole = "";

                    if (rolesForUser.Count > 0)
                        userRole = rolesForUser.ElementAt(0);
                    else
                        userRole = "";


                    var response = new
                    {
                        email = user.Email,
                        username = user.UserName,
                        role = userRole
                    };

                    var json = JsonConvert.SerializeObject(response, _serializerSettings);
                    return new OkObjectResult(json);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                await Error.Add(e, _context);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}