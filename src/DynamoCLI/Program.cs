using System;
using Dynamo.Applications;
using Dynamo.Models;
using System.IO;

namespace DynamoCLI
{
    internal class Program
    {
        [STAThread]
        static internal void Main(string[] args)
        {
            /*
             * once the process is running,create/open a log file.
             */
            DateTime dt = DateTime.Now;
            string filepath = "F:\\log-"+ dt.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt";
            FileStream fs = new FileStream(filepath, FileMode.Create);
            fs.Close();
            //if (!File.Exists(filepath))
            //{
            //    fs = new FileStream(filepath, FileMode.Create);
            //}
            //fs = File.OpenWrite(filepath);
            
            //string tmp = "CLI is running in " + dt.ToString("yyyy - MM - dd HH: mm: ss");
            //byte[] data = System.Text.Encoding.Default.GetBytes(tmp);
            ////开始写入
            //fs.Write(data, 0, data.Length);
            //fs.Flush();
            //fs.Close();
            try
            {
                var cmdLineArgs = StartupUtils.CommandLineArguments.Parse(args);
                var locale = StartupUtils.SetLocale(cmdLineArgs);
                var model = StartupUtils.MakeModel(true);
                var runner = new CommandLineRunner(model);
                runner.Run(cmdLineArgs);
                
            }
            catch (Exception e)
            {
                try
                {
                    DynamoModel.IsCrashing = true;
                    Dynamo.Logging.Analytics.TrackException(e, true);
                }
                catch
                {
                }

                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

    }
}
