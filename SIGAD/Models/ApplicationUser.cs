using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SIGAD.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        // Extended Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? CurrentWorkPoint { get; set; }
        public long? ClickCount { get; set; }
        public DateTime LastActiveDate { get; set; }
        public long ProfileViews { get; set; }

        public ApplicationUser()
        {
            LastActiveDate = DateTime.Now;
            ProfileViews = 0;
            ClickCount = 0;
            CurrentWorkPoint = 0;
            FirstName = "";
            LastName = "";
        }
    }
}
