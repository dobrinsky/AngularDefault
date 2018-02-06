using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaportareOTR.Data;
using AutoMapper;
using RaportareOTR.Controllers.Resources;
using RaportareOTR.Models.Estimates;
using RaportareOTR.Models;
using RaportareOTR.CommonCode;

namespace RaportareOTR.Controllers
{
    [Produces("application/json")]
    [Route("api/Estimate")]
    public class EstimateController : Controller
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EstimateController(ApplicationDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpPost("/api/Estimate/Add")]
        public async Task<IActionResult> AddEstimate([FromBody]EstimateResource estimateResource)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var estimate = mapper.Map<EstimateResource, Estimate>(estimateResource);
                estimate.UserId = await ApplicationUser.GetUserIdByName(context, User.Identity.Name);

                if (String.IsNullOrEmpty(estimate.UserId))
                {
                    return NotFound();
                }

                await context.Estimate.AddAsync(estimate);
                await context.SaveChangesAsync();
              
                return Ok(true);
            }
            catch (Exception e)
            {
                await AddError.AddErrorToDatabase(e, context);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}