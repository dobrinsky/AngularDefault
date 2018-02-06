using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RaportareOTR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareOTR.Models.ManageViewModels.Users
{
    public class UsersToShow
    {
        public ApplicationUser User { get; set; }
        public string Role { get; set; }
        
        /// <summary>
        /// Builds a list with all the users and their userClass from the database.
        /// </summary>
        /// <returns>List<UsersToShow> that are to be shwon in tha View</returns>
        public static async Task<List<UsersToShow>> GetUsersFromDatabase(ApplicationDbContext _context, UserManager<ApplicationUser> _userManager)
        {
            // a custom list with the Role (or UserClass) also
            List<UsersToShow> usersToShow = new List<UsersToShow>();
            
            List<ApplicationUser> usersInDb = _context.Users.ToList();

            foreach (ApplicationUser user in usersInDb)
            {
                // assign the user
                UsersToShow newUserToShow = new UsersToShow();
                newUserToShow.User = user;

                // one user can only have one Role. The app is biult like that so take its Role
                var rolesForUser = await _userManager.GetRolesAsync(user);

                if (rolesForUser.Count > 0)
                    newUserToShow.Role = rolesForUser.ElementAt(0);
                else
                    newUserToShow.Role = "Not assigned";

                usersToShow.Add(newUserToShow);
            }

            return usersToShow;
        }
    }
}
