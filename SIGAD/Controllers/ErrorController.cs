using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGAD.Data;
using AutoMapper;
using SIGAD.Controllers.Resources;
using Microsoft.EntityFrameworkCore;
using SIGAD.Models;
using SIGAD.CommonCode;

namespace Sigad.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ErrorController(ApplicationDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("/api/error")]
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

        [HttpDelete("/api/error/{id}")]
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