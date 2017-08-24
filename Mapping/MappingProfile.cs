using AutoMapper;
using SIGAD.Controllers.Resources;
using SIGAD.Models;
using SIGAD.Models.ManageViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGAD.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {

            CreateMap<UsersToShow, UserResource>();
            CreateMap<Error, ErrorResource>();
        }
    }
}
