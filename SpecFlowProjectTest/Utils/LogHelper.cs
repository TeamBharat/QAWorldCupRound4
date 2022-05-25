using System;
using System.IO;

namespace SpecFlowProjectTest.Utils
{
    public static class LogHelper
    {
        //log file name
        private static string logFile = DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";
        private static StreamWriter streamW = null;


        //Create a file which can store log information
        public static void CreateLogFile()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var dir = Path.Combine(baseDir, "TestResults");
            if (Directory.Exists(dir))
            {
                streamW = File.AppendText(Path.Combine(dir, logFile));
            }
            else
            {
                Directory.CreateDirectory(dir);
                streamW = File.AppendText(Path.Combine(dir, logFile));
            }
        }

        //Create a method which can write text in log file
        public static void Write(string LogMessage)
        {
            streamW.Write("{0}  {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            streamW.WriteLine("   {0}", LogMessage);
            streamW.Flush();
        }
    }
}
