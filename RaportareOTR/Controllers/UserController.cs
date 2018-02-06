using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaportareOTR.Data;
using Microsoft.AspNetCore.Identity;
using RaportareOTR.Models;
using AutoMapper;
using RaportareOTR.Controllers.Resources;
using RaportareOTR.Models.ManageViewModels.Users;
using RaportareOTR.CommonCode;
using Microsoft.AspNetCore.Authorization;

namespace RaportareOTR.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper mapper;

        public UserController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.mapper = mapper;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet("/api/user")]
        public async Task<IEnumerable<UserResource>> Users()
        {
            //if (User.IsInRole(RoleNames.Admin))
            //{
            try
            {
                var users = await UsersToShow.GetUsersFromDatabase(_context, _userManager);

                return mapper.Map<IEnumerable<UsersToShow>, IEnumerable<UserResource>>(users);
            }
            catch (Exception e)
            {
                await AddError.AddErrorToDatabase(e, _context);

                return new List<UserResource>();
            }
            //}
            //else
            //{
            //    return RedirectToAction("EroareUtilizator", "Erori");
            //}
        }

        [HttpDelete("/api/user/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                    return NotFound();

                _context.Remove(user);
                await _context.SaveChangesAsync();

                return Ok(id);
            }
            catch (Exception e)
            {
                await AddError.AddErrorToDatabase(e, _context);

                return Ok(e);
            }

        }

        [AllowAnonymous]
        [HttpPost("/api/add-user/")]
        public async Task<IActionResult> AddUserAsync([FromBody]UserResource userResource)
        {
            if (ModelState.IsValid)
            { 
                var user = new ApplicationUser { UserName = userResource.User.FirstName, Email = userResource.User.Email };
                var result = await _userManager.CreateAsync(user, userResource.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

            // If we got this far, something failed, redisplay form
            return Ok(true);
        }
    }
}