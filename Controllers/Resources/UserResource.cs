using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SIGAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGAD.Controllers.Resources
{
    public class UserResource:IdentityUser
    {
        public ApplicationUser User { get; set; }

        public string Role { get; set; }
    }
}
