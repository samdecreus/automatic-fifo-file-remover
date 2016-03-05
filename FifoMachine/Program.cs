using System;

namespace FifoMachine
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var configurationPath = args.Length == 0 ? "settings.json" : args[0];

            Console.WriteLine(@"starting program...");
            var fifoManager = new FifoManager();
            fifoManager.Execute(configurationPath);
            Console.WriteLine(@"program finished...");

            Console.ReadKey();

        }
    }
}
