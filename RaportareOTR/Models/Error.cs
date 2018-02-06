using RaportareOTR.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareOTR.Models
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


        public static async Task Add(Exception e, ApplicationDbContext _context)
        {
            // Get stack trace for the exception with source file information
            var st = new StackTrace(e, true);
            // Get the stack frames

            string file = "";
            string fileTemp = "";
            string method = "";
            string lineNumber = "";

            foreach (StackFrame frame in st.GetFrames())
            {
                // Get the file name from the stack frame
                fileTemp = frame.GetFileName() ?? "";
                fileTemp = fileTemp.Replace('\\', '-').Split('-').Last().Trim();

                int line = frame.GetFileLineNumber();

                if (line > 0)
                {
                    file += "-> " + fileTemp + "\n";

                    // Get the method from the stack frame
                    method = "-> " + frame.GetMethod().ToString().Substring(frame.GetMethod().ToString().IndexOf(' '), frame.GetMethod().ToString().IndexOf('(') - frame.GetMethod().ToString().IndexOf(' ')) + "\n";

                    // Get the line number from the stack frame
                    lineNumber += "-> " + frame.GetFileLineNumber().ToString() + "\n";
                }
            }

            string destails = e.Message;

            if (e.InnerException != null)
            {
                var innerException = e;

                Exception realerror = e;
                while (realerror.InnerException != null)
                {
                    realerror = realerror.InnerException;
                    destails += "\n" + realerror.Message;
                }
            }

            Error error = new Error
            {
                Description = destails,
                File = file,
                Line = lineNumber,
                Method = method,
                Moment = DateTime.Now
            };

            await _context.Error.AddAsync(error);
            await _context.SaveChangesAsync();
        }
    }
}

