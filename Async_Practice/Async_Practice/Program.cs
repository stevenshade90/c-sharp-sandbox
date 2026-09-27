namespace Async_Practice
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting...");

            Task<string> t = DoWorkAsync();

            Console.WriteLine("Continuing work on the main thread...");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Working... {i}");
                await Task.Delay(1000);
            }

            Console.WriteLine("Main thread finished!");

            var s = await t;
            Console.WriteLine(s);

            Thread.Sleep(2000);
            Console.WriteLine("Beginning whenall tasks!");

            await TestingWhenAllAsync();

        }

        static async Task<string> DoWorkAsync()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(2_000);
                Console.WriteLine("Task 1 complete");
            });
            await Task.Run(() =>
            {
                Thread.Sleep(2_000);
                Console.WriteLine("Task 2 complete");
            });
            await Task.Run(() =>
            {
                Thread.Sleep(2_000);
                Console.WriteLine("Task 3 complete");
            });
            return "All inner tasks complete!";
        }

        static async Task TestingWhenAllAsync()
        {
            await Task.WhenAll(Task.Run(() =>
            {
                Thread.Sleep(5_000);
                Console.WriteLine("Completed Task 4");
            }), Task.Run(() =>
            {
                Thread.Sleep(1_000);
                Console.WriteLine("Completed Task 5");
            }), Task.Run(() =>
            {
                Thread.Sleep(3_000);
                Console.WriteLine("Completed Task 6");
            }), Task.Run(() =>
            {
                Thread.Sleep(2_000);
                Console.WriteLine("Completed Task 7");
            }));       
        }
    }
}
