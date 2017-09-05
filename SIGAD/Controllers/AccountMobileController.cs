using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using SIGAD.Data;
using SIGAD.Models;
using SIGAD.Models.AccountViewModels;
using SIGAD.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using SIGAD.Controllers.Resources;
using Microsoft.AspNetCore.Authentication;
using SIGAD.Controllers;
using System;
using System.Security.Claims;
using Newtonsoft.Json;
using SIGAD.CommonCode.Authentication;
using System.Collections;
using System.Linq;
using ECV.Controllers.Resources;
using Microsoft.Extensions.Options;

namespace SIGAD.Controllers
{
    public class AccountMobileController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        private readonly IJwtFactory _jwtFactory;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly JwtIssuerOptions _jwtOptions;

        public AccountMobileController(
            ApplicationDbContext context,
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

            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        //[HttpPost("/api/login")]
        //public async Task<IActionResult> Login([FromBody]LoginMobileResource model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var identity = await GetClaimsIdentity(model.Email, model.Password);
        //    if (identity == null)
        //    {
        //        return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
        //    }

        //    // Serialize and return the response
        //    var response = new
        //    {
        //        id = identity.Claims.Single(c => c.Type == "id").Value,
        //        auth_token = await _jwtFactory.GenerateEncodedToken(model.Email, identity),
        //        expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
        //    };

        //    var json = JsonConvert.SerializeObject(response, _serializerSettings);
        //    return new OkObjectResult(json);
        //}

        [HttpPost("/api/login")]
        public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials)
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
                //ClaimsIdentity claim = new ClaimsIdentity
                //{
                //    Actor = ""
                //};

                // Serialize and return the response

                ApplicationUser user = _context.Users.SingleOrDefault(c => c.UserName == credentials.UserName);

                var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
                if (identity == null)
                {
                    return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
                }

                var response = new
                {
                    id = identity.Claims.Single(c => c.Type == "id").Value,
                    auth_token = await _jwtFactory.GenerateEncodedToken(credentials.UserName, identity),
                    expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
                };

                var json = JsonConvert.SerializeObject(response, _serializerSettings);
                return new OkObjectResult(json);
            }
            else
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }
        }

        [HttpPost("/api/register")]
        public async Task<IActionResult> Register([FromBody]RegisterMobileResource model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return Ok();
        }

        [HttpGet("/api/isAuth")]
        public bool IsAuth()
        {
            if (User.Identity.IsAuthenticated)
                return true;
            else
                return false;

        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                // get the user to verifty
                var userToVerify = await _userManager.FindByNameAsync(userName);

                ApplicationUser user = _context.Users.SingleOrDefault(c => c.UserName == userName);

                if (userToVerify != null)
                {
                    // check the credentials  
                    if (await _userManager.CheckPasswordAsync(user, password))
                    {
                        //return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, user.Id));
                        return _jwtFactory.GenerateClaimsIdentity(userName, user.Id);
                    }
                }
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}