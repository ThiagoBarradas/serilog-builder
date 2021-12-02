using System;
using System.Threading;

namespace Serilog.Builder.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerBuilder builder = new LoggerBuilder();
            builder.UseSuggestedSetting("Domainnn", "Apppp");
            builder.EnableDataDog("xxxx");

            var logger = builder.BuildConfiguration().CreateLogger();

            for (int i=0; i < 99; i++)
            {
                logger.Information("[{Domain}] [{Application}] Test Data Dog log "+i.ToString()+" :)");
                Thread.Sleep(300);
            }

            Console.WriteLine("Finish!");
        }
    }
}
