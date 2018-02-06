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
using RaportareOTR.CommonCode;
using RaportareOTR.Models;

namespace RaportareOTR.Controllers
{

    public class DatiFisiereController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public DatiFisiereController(ApplicationDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("/api/dataFisiere")]
        public async Task<DatiFisiereResource> GetOneDate(string lunaAn, string userID)
        {
            try
            {
                var dates = await context.DatiFisiere.SingleOrDefaultAsync(d => d.LunaAn == lunaAn && d.UserId == userID);

                if (dates == null)
                    return null;

                return mapper.Map<DatiFisiere, DatiFisiereResource>(dates);
            }
            catch (Exception e)
            {
                await AddError.AddErrorToDatabase(e, context);

                return null;
            }
        }

        [HttpGet("/api/datiFisiere")]
        public async Task<IEnumerable<DatiFisiereResource>> GetDates()
        {
            try
            {
                // ApplicationUser user = await context.Users.SingleOrDefaultAsync(c => c.UserName == User.Identity.Name);

                var dates = context.DatiFisiere.AsEnumerable();/*.Where(t => t.UserId == user.Id);*/

                return mapper.Map<IEnumerable<DatiFisiere>, IEnumerable<DatiFisiereResource>>(dates);
            }
            catch (Exception e)
            {
                await AddError.AddErrorToDatabase(e, context);

                return new List<DatiFisiereResource>();
            }
        }



        [HttpPost("/api/datiFisiere")]
        public async Task<IActionResult> CreateDates([FromBody]DatiFisiereResource dateResource)
        {

            try
            {
                // ApplicationUser user = await context.Users.SingleOrDefaultAsync(c => c.UserName == User.Identity.Name);


                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var date = mapper.Map<DatiFisiereResource, DatiFisiere>(dateResource);

                // date.UserId = user.Id;
                context.DatiFisiere.Add(date);
                await context.SaveChangesAsync();

                var result = mapper.Map<DatiFisiere, DatiFisiereResource>(date);
                return Ok(result);
            }
            catch(Exception e)
            {
                await AddError.AddErrorToDatabase(e, context);

                return Ok(e);
            }
        }

        [HttpPut("/api/datiFisiere/{id}")]
        public async Task<IActionResult> UpdateDates(long id, [FromBody]DatiFisiereResource dateResource)
        {

            try
            {
                // ApplicationUser user = await context.Users.SingleOrDefaultAsync(c => c.UserName == User.Identity.Name);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var dates = await context.DatiFisiere.SingleOrDefaultAsync(t => t.Id == id);



                if (dates == null)
                    return NotFound();


                mapper.Map(dateResource, dates);

                // if (dates.UserId == user.Id)
                await context.SaveChangesAsync();

                var result = mapper.Map<DatiFisiere, DatiFisiereResource>(dates);
                return Ok(result);
            }
            catch (Exception e)
            {
                await AddError.AddErrorToDatabase(e, context);

                return Ok(e);
            }
        }


        [HttpDelete("/api/datiFisiere/{id}")]
        public async Task<IActionResult> DeleteDates(long id)
        {
            try
            {
              //  ApplicationUser user = await context.Users.SingleOrDefaultAsync(c => c.UserName == User.Identity.Name);

                var dates = await context.DatiFisiere.FindAsync(id);

                if (dates == null)
                    return NotFound();

               // if (dates.UserId == user.Id)
                    context.Remove(dates);
                await context.SaveChangesAsync();

                return Ok(id);
            }
            catch (Exception e)
            {
                await AddError.AddErrorToDatabase(e, context);

                return Ok(e);
            }
        }

        //[HttpPost("/api/datiFisere")]
        //public long CreateOrModify([FromBody]DatiFisiereResource dateResource)
        //{
        //    var dataFisier = context.DatiFisiere.SingleOrDefault(c => c.LunaAn == dateResource.LunaAn && c.UserId == dateResource.UserId);

        //    if (dataFisier == null)
        //    {
        //        dataFisier = new DatiFisiere
        //        {

        //            Count = 1,
        //            LunaAn = dateResource.LunaAn,
        //            UserId = dateResource.UserId
        //        };

        //        context.DatiFisiere.Add(dataFisier);
        //    }
        //    else
        //    {
        //        dataFisier.Count += 1;
        //    }

        //    context.SaveChanges();

        //    return dataFisier.Id;
        //}


    }
}