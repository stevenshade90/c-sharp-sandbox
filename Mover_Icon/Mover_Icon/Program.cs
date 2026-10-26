namespace MoverProgram
{ 
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(Thread2);

            List<string> Xmover = new List<string>();

            int x = 0;
            int y = 0;
            int counter = 0;

            string icon = "*";


            while (true)
            {
                while (counter != 10)
                {
                    if (x < 10 + y)
                    {
                        Console.Write("\r");
                        Xmover.ForEach(moverItem => Console.Write(moverItem));
                        Console.Write($"{icon}");
                        Thread.Sleep(1);

                        Xmover.Add(" ");
                        x++;
                    }
                    else
                    {
                        Console.WriteLine();
                        Xmover.Clear();
                        x = 0;
                        y++;
                        counter++;
                    }
                }

                counter = 0;
                y -= 2;

                while (counter != 10)
                {
                    if (x < 10 + y)
                    {
                        Console.Write("\r");
                        Xmover.ForEach(moverItem => Console.Write(moverItem));
                        Console.Write($"{icon}");
                        Thread.Sleep(1);

                        Xmover.Add(" ");
                        x++;
                    }
                    else
                    {
                        Console.WriteLine();
                        Xmover.Clear();
                        x = 0;
                        --y;
                        counter++;
                    }
                }
                counter = 0;
                break;             
            }

            thread1.Start();

            static void Thread2()
            {

                List<string> Xmover = new List<string>();

                int x = 0;
                int y = 0;
                int counter = 0;

                string icon = "*";

                while (true)
                {
                    while (counter != 10)
                    {
                        if (x < 10 + y)
                        {
                            Console.Write("\r");
                            Xmover.ForEach(moverItem => Console.Write(moverItem));
                            Console.Write($"{icon}");
                            Thread.Sleep(1);

                            Xmover.Add(" ");
                            x++;
                        }
                        else
                        {
                            Console.WriteLine();
                            Xmover.Clear();
                            x = 0;
                            y++;
                            counter++;
                        }
                    }

                    counter = 0;
                    y -= 2;

                    while (counter != 10)
                    {
                        if (x < 10 + y)
                        {
                            Console.Write("\r");
                            Xmover.ForEach(moverItem => Console.Write(moverItem));
                            Console.Write($"{icon}");
                            Thread.Sleep(1);
                            Xmover.Add(" ");
                            x++;
                        }
                        else
                        {
                            Console.WriteLine();
                            Xmover.Clear();
                            x = 0;
                            --y;
                            counter++;
                        }
                    }
                    counter = 0;
                    break;
                }
            }
        }
    }
}