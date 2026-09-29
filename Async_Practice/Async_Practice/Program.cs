
namespace Async_Practice
{
    internal class Program
    {
        static CancellationTokenSource tokenSource = new CancellationTokenSource();

        static async Task Main(string[] args)
        {

            //Console.WriteLine("Starting...");

            //Task<string> t = DoWorkAsync();
            //await LoadingImage(t);

            //Console.WriteLine("\nContinuing work on the main thread...");
            //for (int i = 0; i < 5; i++)
            //{
            //    Console.WriteLine($"Working... {i}");
            //    await Task.Delay(1000);
            //}

            //Console.WriteLine("Main thread finished!");

            //var s = await t;
            //Console.WriteLine(s);

            //Thread.Sleep(2000);
            Console.WriteLine("Beginning WhenAll tasks!");

            try
            {
                await TestingWhenAllAsync().WaitAsync(tokenSource.Token);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"Ended execution! Message: {ex.Message}");
            }

            //Async stream
            await foreach (var num in GenerateNumbers())
            {
                Console.WriteLine(num);
            }

            String[] names = { "Steve", "Tim", "Jeff" };
            await Parallel.ForEachAsync(names,  async (name, tokenSource) =>
            {
                Console.WriteLine("Hello {0}", name);

            });
        }

        static async IAsyncEnumerable<int> GenerateNumbers()
        {
            for (int i = 0; i < 50; i++)
            {
                await Task.Delay(100);
                yield return i+1;
            }
        }
        static async Task<string> DoWorkAsync()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(2_000);
                Console.Write("\rWaiting for task to complete... Task 1 Complete\n");
            });
            await Task.Run(() =>
            {
                Thread.Sleep(2_000);
                Console.Write("\rWaiting for task to complete... Task 2 Complete\n");
            });
            await Task.Run(() =>
            {
                Thread.Sleep(2_000);
                Console.Write("\rWaiting for task to complete... Task 3 Complete\n");
            });
            return "All preliminary tasks complete!";
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
                //tokenSource.Cancel();
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
        static async Task LoadingImage(Task awaitingTask)
        {
            int iterator = 1;
            char[] loading = { '|', '/', '-', '\\' };


            while (!awaitingTask.IsCompleted)
            {
                Console.Write("\rWaiting for task to complete... {0}", loading[iterator % 4]);
                iterator++;
                Thread.Sleep(100);
            }
        }
    }
}