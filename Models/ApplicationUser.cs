﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SIGAD.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public long CompanyID { get; set; }
        public long CandidateID { get; set; }
        public DateTime LastActiveDate { get; set; }
        public long ProfileViews { get; set; }

        public ApplicationUser()
        {
            CompanyID = 0;
            CandidateID = 0;
            LastActiveDate = DateTime.Now;
            ProfileViews = 0;
        }
    }
}
