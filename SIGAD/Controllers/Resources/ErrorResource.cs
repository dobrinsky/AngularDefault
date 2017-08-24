using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGAD.Controllers.Resources
{
    public class ErrorResource
    {
        public long ID { get; set; }

        public string Description { get; set; }

        public DateTime Moment { get; set; }

        public string File { get; set; }

        public string Method { get; set; }

        public string Line { get; set; }
    }
}
