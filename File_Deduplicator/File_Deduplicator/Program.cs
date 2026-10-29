using File_Deduplicator.Methods;
using File_Deduplicator.UserClass;
using Microsoft.Extensions.Logging;

namespace File_Deduplicator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string logPath = $"DiagnosticReport/DeduplicationLog-{DateTime.Now.ToString("HH-mm-ss")}.txt";
            using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddFile(logPath));

            ProgramOrchestrator orchestrator = new ProgramOrchestrator(loggerFactory);

            await orchestrator.ExecuteProgram();
        }
    }
}