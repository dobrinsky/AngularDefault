using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGAD.CommonCode.Validation
{
    public class ValidationReturn
    {
        public ValidationReturn()
        {
            FieldName = "";
            Message = "";
        }

        public ValidationReturn(string fieldName, string message)
        {
            FieldName = fieldName;
            Message = message;
        }

        public string FieldName { get; set; }
        public string Message { get; set; }
    }
}
