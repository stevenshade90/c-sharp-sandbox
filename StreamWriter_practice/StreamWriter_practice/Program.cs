using System;

namespace CarRental
{
    public class Program
    {
        static void Main(string[] args)
        {
            String name = "";
            String prompt = "Enter your name: ";
            name = NameCheck(prompt, name);

            int days = 0;
            prompt = "Enter number of rental days: ";
            days = RentalDays(prompt, days);

            const double RENTAL_RATE = 45.00;
            String invoice = RentalPrice(name, days, RENTAL_RATE);
            Console.Write(invoice);


            static string NameCheck(String prompt, String name)
            {

                bool noText = true;
                do
                {
                    List<char> nameChars = new List<char>();

                    Console.Write(prompt);
                    String input = Console.ReadLine();

                    foreach (char c in input)
                    {
                        nameChars.Add(c);
                    }

                    if (string.IsNullOrWhiteSpace(input) || nameChars.Contains(' '))
                    {
                        Console.WriteLine("Do not enter blank spaces");
                        nameChars.Clear();
                        continue;
                    }

                    int unusedNum;
                    if (int.TryParse(input.Trim(), out unusedNum))
                    {
                        Console.WriteLine("Do not enter numbers");
                        nameChars.Clear();
                        continue;
                    }

                    name = input.Trim();
                    noText = false;
                    return name;
                } while (noText);

                return name.ToUpper(); 
            }

            static int RentalDays(String prompt, int days)
            {
                bool noNumber = true;
                do
                {
                    Console.Write(prompt);
                    String input = Console.ReadLine();

                    try
                    {
                        days = int.Parse(input);
                        noNumber = false;
                    }
                    catch 
                    {
                        Console.WriteLine("Enter a valid format");
                    }
                } while (noNumber);

                return days;
            }

            static string RentalPrice(String name, int days, double RENTAL_RATE)
            {
                String header = "\n\nHere is your invoice";
                Console.WriteLine(header);

                foreach (char c in header)
                {
                    Console.Write("_");
                }

                double price = 0;
                if (days >= 7)
                {
                    //Discounted rate
                    price = (days * RENTAL_RATE);

                    String invoice = String.Format("\nNAME: {0}\nRENTAL PERIOD: {1}\nORIGINAL PRICE: ${2:#,##0.00}\nDISCOUNT: 10%\nNEW PRICE: ${3:#,##0.00}", name, days, price, (price*.9));
                    return invoice;
                }
                else
                {
                    //Regular rate
                    price = (days * RENTAL_RATE);

                    String invoice = String.Format("\nNAME: {0}\nRENTAL PERIOD: {1}\nPRICE: ${2:#,##0.00}\n", name, days, price);
                    return invoice;
                }
            }
        }
    }
}
