using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SIGAD.Models
{
    public class Error
    {
        public Error()
        {
            ID = 0;
            Moment = DateTime.Now;
            Description = "";
            File = "";
            Method = "";
            Line = "";
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "ID")]
        public long ID { get; set; }

        public string Description { get; set; }

        public DateTime Moment { get; set; }

        public string File { get; set; }

        public string Method { get; set; }

        public string Line { get; set; }
    }
}
