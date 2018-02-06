using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareOTR.CommonCode
{
    public static class RoleNames
    {
        public const string OTR = "OTR";
        public const string Client = "Client";

        public static List<string> Build()
        {
            List<string> toReturn = new List<string>();

            toReturn.Add(OTR);
            toReturn.Add(Client);

            return toReturn;
        }
    }
}
