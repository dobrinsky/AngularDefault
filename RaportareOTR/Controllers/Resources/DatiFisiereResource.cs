using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareOTR.Controllers.Resources
{
    public class DatiFisiereResource
    {
        public long Id { get; set; }

        public int Count { get; set; }

        [StringLength(10)]
        public string LunaAn { get; set; }

        [StringLength(450)]
        public string UserId { get; set; }
    }
}
