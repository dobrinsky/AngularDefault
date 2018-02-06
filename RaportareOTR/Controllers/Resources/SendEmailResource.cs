using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareOTR.Controllers.Resources
{
    public class SendEmailResource
    {
        [Required]
        public string Nume { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telefon { get; set; }
        public string Mesaj { get; set; }
    }
}
