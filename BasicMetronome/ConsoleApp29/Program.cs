using System.Threading;

//1) Get use # BPM + error catch
//Letters entered??

bool userValue = false;
int userBPM = 0;
const int msDelay = 60000;

do
{
    Console.Write("Enter a BPM between 1-200: ");

    try
    {
        userBPM = Convert.ToInt32(Console.ReadLine());

        if (userBPM >= 1 && userBPM <=200)
        {
            userValue = true;
        }
        else
        {
            Console.WriteLine("\tInvalid BPM. Enter a BPM between 1-200.\n");
        }
    }
    catch (FormatException e)
    {
        Console.WriteLine($"\t{e.Message}");
    }
} while (!userValue);


//2)Calculate BPM forumla: delay = 60,000/BPM
//take user BPM input for formula;
//Place in loop to keep going
//beep and delay
//import beep sound? maybe use built-in first
//way to exit loop -- maybe press key? While true/false


int clickDelay = msDelay / userBPM;
Console.Write("Press any key to exit\n");

while (true)
{
    Console.Beep();
    Thread.Sleep(clickDelay);

    if (Console.KeyAvailable)
    {
        break;
    }
}
Console.WriteLine("Bye"!);