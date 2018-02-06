using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareOTR.Models
{
    public class File
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        [Required]
        public string FileType { get; set; }

        public byte[] Content { get; set; }

        public DateTime CreationDate { get; set; }

        public File()
        {
            CreationDate = DateTime.Now;
        }
    }
}

