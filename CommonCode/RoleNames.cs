using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGAD.CommonCode
{
    public static class RoleNames
    {
        public const string Admin = "Admin";
        public const string Candidate = "Candidate";
        public const string Company = "Company";
        public const string HrPerson = "HrPerson";

        public static List<string> Build()
        {
            List<string> toReturn = new List<string>();

            toReturn.Add(Admin);
            toReturn.Add(Candidate);
            toReturn.Add(Company);
            toReturn.Add(HrPerson);

            return toReturn;
        }
    }
}
