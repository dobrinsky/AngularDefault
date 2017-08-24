using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGAD.Data;
using Microsoft.AspNetCore.Identity;
using SIGAD.Models;
using AutoMapper;
using SIGAD.Controllers.Resources;
using SIGAD.Models.ManageViewModels.Users;
using SIGAD.CommonCode;

namespace SIGAD.Controllers
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
    }
}