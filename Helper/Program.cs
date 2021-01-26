using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helper
{
    class Program
    {
        public static string output = "";
        public static bool is_used = true;
        static async Task Main()
        {
            var calculationTask = CalculatingAsync();
            var loadingTask = LoadingAsync();

            var tasks = new List<Task> { calculationTask, loadingTask };
            while (output == "")
            {
                Task finishedTask = await Task.WhenAny(tasks);
                if (finishedTask == calculationTask)
                {
                    output = "Calculation is done";
                    is_used = false;
                    Console.WriteLine(output);
                }
            }
        }

        private static async Task LoadingAsync()
        {
            while(is_used)
            {
                Console.WriteLine("loading...");
                await Task.Delay(1000);
            }
        }

        private static async Task CalculatingAsync()
        {
            await Task.Delay(10000);
            Console.WriteLine("Work is at the end!");
        }
    }
}
