using RaportareOTR.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareOTR.Models.ManageViewModels.Users
{
    public class AddUserViewModel
    {
        public RegisterViewModel RegisterViewModel { get; set; }
        public List<string> UserClasses { get; set; }
    }
}
