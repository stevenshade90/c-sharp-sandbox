using AbstractClasses;
using AvailableEnemies;
using AvailableHeroes;

namespace OOPPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Troll t1;
            Goblin g1;
            Mage m1;
            Necromancer n1;

            // Random collection of the avilable types, just to see how things work using the "is" keyword down in "MethodTesting"
            object[] HeroAndEnemyCollection = new object[]
            {
                t1 = new Troll("Trollie", "The worst troll of them all", 5, 300, 5),
                g1 = new Goblin("Gobbie", "An OK goblin", 1, health: 200, 5),
                new Goblin(),
                m1 = new Mage("Kharid", "Mage", "A caster of magic", 7, 250, 100, 50),
                n1 = new Necromancer(name: "Nec", heroclass: "Necromancer", desc: "Summoner of the dead", level: 3, health: 100, mana: 200, armor: 50) { X = 500, Y = 200, Z = 321 },
            };

            // First section of testing
            TestingBanner("Collection of Units");
            foreach (object enemy in HeroAndEnemyCollection)
            {
                MethodTesting(enemy);
            }
            Console.WriteLine("\n");

            // Second section of testing
            TestingBanner("Mage");
            Console.WriteLine($"Current Mana: {m1.Mana}");
            m1.CastFireball();

            Console.WriteLine();
            Console.Write("Mage current location: ");
            m1.GetCurrentLocation();

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Current Mana: {m1.Mana}");
                m1.Teleport();
                Console.WriteLine();
            }
            Console.WriteLine("\n");

            // Third section of testing
            TestingBanner("Necromancer");
            n1.CurrentMinions();
            n1.DisplayHealthAndMana();
            Console.WriteLine();

            for (int i = 0; i < 5; i++)
            {
                n1.DisplayHealthAndMana();
                n1.SummonGolem();
                n1.SummonSkeleton();
                n1.Resurrect();
                Console.WriteLine();
            }
            Console.WriteLine();

            n1.DisplayHealthAndMana();
            n1.CurrentMinions();
            n1.GetCurrentLocation();

            Console.WriteLine();

            foreach (object o in HeroAndEnemyCollection)
            {
                Console.WriteLine(o.GetType().Name);
            }
            Console.WriteLine();

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine();
                n1.Sacrifice();
                n1.DisplayHealthAndMana();
                n1.CurrentMinions();
            }


            Console.WriteLine("\n\n\n\n\n");

            //Goblin attack!
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine();
                n1.DisplayHealthAndMana();
                g1.DealDamage(n1, 5);
                Console.WriteLine();
            }

            // Damaging Enemy type test -- killing goblin
            n1.ViewLoot();

            while (g1.Health > 0)
            {
                n1.DealDamage(g1, 50);
            }

            n1.ViewLoot();

            Console.WriteLine("\n\n\n");

            //killing troll, testing loot bag and items added
            while (t1.Health > 0)
            {
                n1.DealDamage(t1, 50);
            }

            Console.WriteLine();
            TestingBanner("After Enemy Death");
            n1.ViewLoot();
            Console.WriteLine(n1.ViewHealth);

            Thread.Sleep(2000);
            Console.WriteLine("\n\n\n");

            n1.ViewLoot();

            Console.WriteLine();
            n1.PotionConsumption();
            Console.WriteLine(n1.ViewHealth);

            Thread.Sleep(2000);
            n1.ViewLoot();
        }

        static void MethodTesting(object o)
        {
            if (o is Enemy e)
            {
                try
                {
                    e.StringText();
                    e.EngagementText();
                    Console.WriteLine();
                }
                catch (NotImplementedException ex)
                {
                    Console.WriteLine($"Method not implemented: \"{ex.Message}\"");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to execute method: \"{0}\"", ex.Message);
                }
            }    
        }
        static void TestingBanner(string EnemyOrHeroName)
        {
            Console.WriteLine($"========= TESTING FOR {EnemyOrHeroName.ToUpper()} =========");
        }
    }
}