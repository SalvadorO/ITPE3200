using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace WebApp
{
    public sealed class ExceptionUtility
    {
        private ExceptionUtility()
        { }

        public static void LogException(Exception exception, string source)
        {
            string logfile = @"~/Errors/Error.txt";
            logfile = HttpContext.Current.Server.MapPath(logfile);

            StreamWriter sw = new StreamWriter(logfile, true);
            sw.WriteLine("********** {0} **********", DateTime.Now);
            if (exception.InnerException != null)
            {
                sw.Write("Inner Exception Type: ");
                sw.WriteLine(exception.InnerException.GetType().ToString());
                sw.Write("Inner Exception: ");
                sw.WriteLine(exception.InnerException.Message);
                sw.Write("Inner Source: ");
                sw.WriteLine(exception.InnerException.Source);
                if (exception.InnerException.StackTrace != null)
                {
                    sw.WriteLine("Inner Stack Trace: ");
                    sw.WriteLine(exception.InnerException.StackTrace);
                }
            }
            sw.Write("Exception Type: ");
            sw.WriteLine(exception.GetType().ToString());
            sw.WriteLine("Exception: " + exception.Message);
            sw.WriteLine("Source: " + source);
            sw.WriteLine("Stack Trace: ");
            if (exception.StackTrace != null)
            {
                sw.WriteLine(exception.StackTrace);
                sw.WriteLine();
            }
            sw.Close();
        }

     

    }
}
