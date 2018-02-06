using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGAD.CommonCode.Validation
{
    public class CodesValidationMethods
    {
        public static bool VerificaCod(string input)
        {
            bool rezultatValidareCod = true;

            if (input == "" || (input.Length > 10 && input.Length != 13))
            {
                rezultatValidareCod = false;
            }
            else if (input != "" && input.Length == 13)
            {
                if (!VerificaCNP(input))
                {
                    rezultatValidareCod = false;
                }
            }
            else if (input != "" && input.Length <= 10)
            {
                if (!verificaCIF(input))
                {
                    rezultatValidareCod = false;
                }
            }

            return rezultatValidareCod;
        }

        private static bool VerificaCNP(string cnp)
        {

            if (cnp.Length != 13)
                return false;

            const string a = "279146358279";

            long j = 0;

            if (!long.TryParse(cnp, out j))
                return false;

            long suma = 0;

            for (int i = 0; i < 12; i++)
                suma += long.Parse(cnp.Substring(i, 1)) * long.Parse(a.Substring(i, 1));

            long rest = suma - 11 * (int)(suma / 11);

            rest = rest == 10 ?

            1 : rest;

            if (long.Parse(cnp.Substring(12, 1)) != rest)
                return false;

            return true;
        }

        private static bool verificaCIF(string cif)
        {
            bool rezultatParse = true;

            string justNumbers = new String(cif.Where(Char.IsDigit).ToArray());

            long reallyJustNumbers = 0;

            rezultatParse = long.TryParse(justNumbers, out reallyJustNumbers);

            if (!rezultatParse) return false;

            long controlNumber = 753217532;
            int cifraControl = (int)reallyJustNumbers % 10;

            reallyJustNumbers = (long)reallyJustNumbers / 10;

            double temp = 0.0;

            while (reallyJustNumbers > 0)
            {
                temp += (reallyJustNumbers % 10) * (controlNumber % 10);
                reallyJustNumbers = (long)reallyJustNumbers / 10;
                controlNumber = (long)controlNumber / 10;
            }

            double c = temp * 10 % 11;

            if (c == 10) c = 0;

            return cifraControl == c;
        }
    }
}
