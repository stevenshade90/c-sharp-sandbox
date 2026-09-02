using System.Media;

namespace SoundPlayer_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<SoundPlayer> collectionOfMusic = new List<SoundPlayer>()
            {
                new SoundPlayer
                {
                    SoundLocation = @"C:\Users\steve\OneDrive\Desktop\Dorico Files\I. Catalog of Original Works\Orchestral Overture (PDF, MP3)\Orchestral Overture - Audio.wav",
                    Tag = "Orchestral Overture"
                },
                new SoundPlayer
                {
                    SoundLocation = @"C:\Users\steve\OneDrive\Desktop\Flows from Inferno\01 - Inferno\Inferno_Dorico - Inferno.wav",
                    Tag = "Inferno"
                },
                new SoundPlayer
                {
                    SoundLocation = @"C:\Users\steve\OneDrive\Desktop\Reaper Projects\Schubert\Impromptus, Op_ 90\No_ 3 in Gb Major\gb.wav",
                    Tag = "Impromptu No. 3"
                }
            };

            DisplaySongs(collectionOfMusic);
            GetSong(collectionOfMusic);
        }

        static void DisplaySongs(List<SoundPlayer> collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                Console.WriteLine($"{i+1}: {collection[i].Tag}");
            }
        }

        static void GetSong(List<SoundPlayer> collection)
        {
            Console.Write("Select a song to play:");

            int choice = int.TryParse(Console.ReadLine(), out int y) ? y - 1 : 0;

            SoundPlayer player = collection[choice];
            Console.WriteLine(player.Tag);

            try
            {
                player.Play();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}