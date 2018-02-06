using RaportareOTR.CommonCode;
using RaportareOTR.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareOTR.Models
{
    public class DatiFisiere
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        public int Count { get; set; }

        [StringLength(10)]
        public string LunaAn { get; set; }

        [ForeignKey("AspNetUsers")]
        [StringLength(450)]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<File> Files { get; set; }

        public DatiFisiere()
        {
            Files = new Collection<File>();
        }

        public static async Task<DatiFisiere> GetDate(long id, ApplicationDbContext _context)
        {
            try
            {
                var date = await _context.DatiFisiere.SingleOrDefaultAsync(t => t.Id == id);

                if (date == null)
                    return null;

                return date;
            }
            catch (Exception e)
            {
                await AddError.AddErrorToDatabase(e, _context);

                return new DatiFisiere();
            }
        }
    }
}
