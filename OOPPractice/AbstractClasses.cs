namespace AbstractClasses
{
    public abstract class Enemy
    {
        /* A blueprint for a basic Enemy class 
           An "idea" of an enemy, just not a concrete entity
           Contains the common data and functionality of derived types
         */
        public string Name { get; set; }
        public string Description { get; set; }

        public int Level { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }


        public Enemy(string name, string desc, int level, int health, int damage)
        {
            Name = name;
            Description = desc;
            Level = level;
            Health = health;
            Damage = damage;
        }
        public abstract void TakeDamage(Hero h, int amount);
        public abstract void DealDamage(Hero HeroClass, int baseDamage);
        public abstract void EngagementText(); // Must be implemented by inheriting class (in this case, Goblin and Troll)

        public virtual void StringText()  // Can be implemented, but NOT necessary with 'virtual' keyword
        {
            Console.WriteLine("StringText Method not implemented!");
        }
    }
    public abstract class Hero
    {
        /* A blueprint for a basic Hero class */
        public string Name { get; set; }
        public string Class { get; set; }
        public string Description { get; set; }

        public List<object> InventoryBag = new List<object>();
        public Random rand = new Random();

        public int Level { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Armor { get; set; }
        public int X, Y, Z;

        public Hero(string name, string heroclass, string desc, int level, int health, int mana, int armor)
        {
            Name = name;
            Class = heroclass;
            Description = desc;
            Level = level;
            Health = health;
            Mana = mana;
            Armor = armor;
            X = 15;
            Y = 35;
            Z = 5;
        }
        public abstract void ViewLoot();
        public abstract void TakeDamage(int amount);
        public abstract void DealDamage(Enemy e, int baseDamage);
        public abstract void ReduceMana(int manaCost);
        public virtual void EngagementText(string text)
        {
            Console.WriteLine("I have nothing to say!");
        }
        public virtual void DisplayHealthAndMana()
        {
            Console.WriteLine($"Health: {this.Health}");
            Console.WriteLine($"Mana: {this.Mana}");
        }
        public virtual void GetCurrentLocation()
        {
            Console.WriteLine($"({X}, {Y}, {Z})");
        }
    }

    public class Potions
    {
        public string PotionName { get; set; }
        public int BaseValue { get; set; }
        public int MaxValue { get; set; }

        public Potions(string name, int baseValue, int maxValue)
        {
            PotionName = name;
            BaseValue = baseValue;
            MaxValue = maxValue;
        }

        /*
        public void ConsumePotion(Hero h, Potions p)
        {
            int healAmount = h.rand.Next(p.BaseValue, p.MaxValue);
            h.Health += healAmount;

            Console.WriteLine($"You just healed for +{healAmount}!");
        }
        */

        public override string ToString()
        {
            return $"{PotionName}: Heals between {BaseValue}-{MaxValue}";
        }
    }
}