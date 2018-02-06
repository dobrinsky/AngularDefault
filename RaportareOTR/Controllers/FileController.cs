using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using RaportareOTR.Data;
using AutoMapper;
using RaportareOTR.Controllers.Resources;
using RaportareOTR.Models;

namespace RaportareOTR.Controllers
{
    public class FileController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public FileController(IHostingEnvironment host, ApplicationDbContext context, IMapper mapper )
        {
            this.host = host;
            this.context = context;
            this.mapper = mapper;
        }

       

        [HttpPost("/api/datiFisiere/{id}/file")]
        public async Task<IActionResult> Upload(long id, IFormFile file)
        {
            var dataFisisere = await DatiFisiere.GetDate(id,context);
            if (dataFisisere == null)
                return NotFound();

            byte[] content = null;
            BinaryReader reader = new BinaryReader(file.OpenReadStream());
            content = reader.ReadBytes((int)file.Length);


            //var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
            //if (!Directory.Exists(uploadsFolderPath))
            //    Directory.CreateDirectory(uploadsFolderPath);

            //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            //var filePath = Path.Combine(uploadsFolderPath, fileName);

            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    await file.CopyToAsync(stream);
            //}
           
            var newFile = new Models.File { FileName = file.FileName, FileType= Path.GetExtension(file.FileName), Content=content };
            dataFisisere.Files.Add(newFile);
            await context.SaveChangesAsync();

            return Ok(mapper.Map<Models.File, FileResource>(newFile));
        }
    }
}

//System.Text.Encoding.Default.GetString(result);