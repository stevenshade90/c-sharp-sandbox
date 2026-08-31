using System.Diagnostics;

namespace PLINQ_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numsto1M = Enumerable.Range(0, 1_000_000).ToArray();

            var watch = new Stopwatch();

            // JIT warm-up
            ShowEvenNums(new[] { 1, 2, 3, 4, 5 });
            ShowEvenNumsWithParallel(new[] { 1, 2, 3, 4, 5 });

            // LINQ vs. PLINQ processing times
            watch.Start();
            ShowEvenNums(numsto1M);
            watch.Stop();
            Console.WriteLine($"Sequential Sort: {watch.ElapsedMilliseconds}ms");

            watch.Restart();
            ShowEvenNumsWithParallel(numsto1M);
            watch.Stop();
            Console.WriteLine($"PLINQ Sort: {watch.ElapsedMilliseconds}ms");
        }

        static void ShowEvenNums(int[] nums)
        {
            _ = (
                from num in nums
                where num % 2 == 0
                orderby num descending
                select num).ToArray(); 
        }

        static void ShowEvenNumsWithParallel(int[] nums)
        {
            _ = (
                from num in nums.AsParallel().WithDegreeOfParallelism(4)
                where num % 2 == 0
                orderby num descending
                select num).ToArray();
        }
    }
}