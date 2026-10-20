public void LoadingImage(Task awaitingTask)
{
    //Loading image that will only display as the csv processing task is running
    int iterator = 1;
    Console.ForegroundColor = ConsoleColor.Green;
    char[] loading = { '|', '/', '-', '\\' };

    while (!awaitingTask.IsCompleted)
    {
        Console.Write("\rLoading information... {0}", loading[iterator % 4]);
        iterator++;
        Thread.Sleep(100);
    }
    Console.ResetColor();
}