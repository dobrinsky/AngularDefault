using AutoMapper;
using RaportareOTR.Controllers.Resources;
using RaportareOTR.Models;
using RaportareOTR.Models.Estimates;
using RaportareOTR.Models.ManageViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareOTR.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {

            CreateMap<UsersToShow, UserResource>();
            CreateMap<Error, ErrorResource>();

            // Custom maps
            CreateMap<EstimateResource, Estimate>();
            CreateMap<Estimate, EstimateResource>();
        }
    }
}
