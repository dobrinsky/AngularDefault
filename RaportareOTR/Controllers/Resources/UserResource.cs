using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RaportareOTR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareOTR.Controllers.Resources
{
    public class UserResource
    {
        public ApplicationUser User { get; set; }

        public string Role { get; set; }
        public string Password { get; set; }
        public string PasswordConfim { get; set; }
    }
}
