using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.ErrorLog
{
    public class WriteLog
    {
        public static void AddLog(string Log)
        {
            var fileName = "Log.txt";
            const string dir = @"App_Data";
            var filePath = Path.Combine(dir, fileName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            Console.WriteLine(Log);
            File.AppendAllText(filePath, "--- Entry Dated " + DateTime.Now.ToLongTimeString() + " ---" + Environment.NewLine);
            File.AppendAllText(filePath, Log);
            File.AppendAllText(filePath, Environment.NewLine);


        }
    }
}
