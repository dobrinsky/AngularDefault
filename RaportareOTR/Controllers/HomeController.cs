using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RaportareOTR.Controllers.Resources;

using System.Net.Mail;
using System.Text;

namespace RaportareOTR.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        [HttpPost("/api/sendEmail")]
        public IActionResult SendEmail([FromBody]SendEmailResource sendEmailResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("adrian.iliescu@gp-chsh.ro", "xxx");

            MailMessage mm = new MailMessage("adrian.iliescu@gp-chsh.ro", "andrei.dobrin@snx.ro", "test", "test");
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);

            return Ok(true);
        }

    }
}
