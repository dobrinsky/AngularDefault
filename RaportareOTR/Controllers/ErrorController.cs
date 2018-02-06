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
    public class ErrorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public ErrorController(ApplicationDbContext _context, IMapper mapper)
        {
            this.mapper = mapper;
            this._context = _context;
        }

        [HttpGet("/api/error")]
        public async Task<IEnumerable<ErrorResource>> Users()
        {
            try
            {
                var errors = await _context.Error.ToListAsync();

                return mapper.Map<IEnumerable<Error>, IEnumerable<ErrorResource>>(errors);
            }
            catch (Exception e)
            {
                await AddError.AddErrorToDatabase(e, _context);

                return new List<ErrorResource>();
            }
        }

        [HttpDelete("/api/error/{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            try
            {
                var error = await _context.Error.FindAsync(id);

                if (error == null)
                    return NotFound();

                _context.Remove(error);
                await _context.SaveChangesAsync();

                return Ok(id);
            }
            catch (Exception e)
            {
                await AddError.AddErrorToDatabase(e, _context);

                return Ok(e);
            }

        }

        [HttpDelete("/api/errorAll")]
        public async Task<IActionResult> DeleteAllErrors()
        {
            try
            {
                _context.Error.RemoveRange(_context.Error.ToList());

                await _context.SaveChangesAsync();

                return Ok(true);
            }
            catch (Exception e)
            {
                await Error.Add(e, _context);

                return Ok(e);
            }

        }
    }
}