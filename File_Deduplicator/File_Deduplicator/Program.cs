using File_Deduplicator.Methods;
using File_Deduplicator.UserClass;
using Microsoft.Extensions.Logging;

namespace File_Deduplicator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

            ProgramOrchestrator orchestrator = new ProgramOrchestrator(loggerFactory);

            DisplayMethods.DisplayMessageGeneric("Loading User");
            User user1 = SerializationMethods.FileExistCheck();

            await orchestrator.ExecuteProgram(user1);
        }
    }
}