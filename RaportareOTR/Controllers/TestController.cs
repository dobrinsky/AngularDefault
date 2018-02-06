using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaportareOTR.Data;
using AutoMapper;
using RaportareOTR.Controllers.Resources;
using Microsoft.EntityFrameworkCore;
using RaportareOTR.Models;
using RaportareOTR.CommonCode;

namespace RaportareOTR.Controllers
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TestController(ApplicationDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("/api/test")]
        public async Task<IEnumerable<ErrorResource>> Users()
        {
            try
            {
                var errors = await context.Error.ToListAsync();

                return mapper.Map<IEnumerable<Error>, IEnumerable<ErrorResource>>(errors);
            }
            catch (Exception e)
            {
                await AddError.AddErrorToDatabase(e, context);

                return new List<ErrorResource>();
            }
        }

        [HttpDelete("/api/test/{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            try
            {
                var error = await context.Error.FindAsync(id);

                if (error == null)
                    return NotFound();

                context.Remove(error);
                await context.SaveChangesAsync();

                return Ok(id);
            }
            catch (Exception e)
            {
                await AddError.AddErrorToDatabase(e, context);

                return Ok(e);
            }

        }
    }
}