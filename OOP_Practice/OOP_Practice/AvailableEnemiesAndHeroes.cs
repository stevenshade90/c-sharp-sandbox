using AbstractClasses;
using OOPPracticeInterfaces;
using ItemClass;
using System.Text;

namespace AvailableEnemies
{
    public class Goblin : Enemy, ILootable
    {
        public bool IsAlive = true;

        public Goblin()
            : this("Not Entered", "Not Entered", 1, 1, 1) { }

        public Goblin(string name, string desc, int level, int health, int damage)
            : base(name, desc, level, health, damage) { }

        public override sealed void EngagementText()
        {
            Console.WriteLine("\"Goblin time!\"");
        }

        public Item DropLoot()
        {
            Console.WriteLine("The goblin dropped loot!");

            return new Item(name: "Goblin Armor", armor: 200, 2, 15, 25);
        }

        public override void TakeDamage(Hero h, int damage)
        {
            this.Health -= damage;
            Console.WriteLine($"Current Goblin health: {this.Health}");

            if (this.Health <= 0)
            {
                IsAlive = false;
            }

            if (!IsAlive)
            {
                Console.WriteLine("The goblin has been slain!");
                h.InventoryBag.Add(DropLoot());
            }
        }

        public Enemy ReturnToken()
        {
            return this;
        }
        public sealed override void DealDamage(Hero HeroClass, int baseDamage)
        {
            int damage = (int)((baseDamage * HeroClass.Level) - (HeroClass.Armor * 0.10));
            Console.WriteLine($"Goblin is about to deal this much damage: {damage}");

            HeroClass.TakeDamage(damage);
        }
        /*
        public override void StringText()
        {
            Console.WriteLine($"Enemy Details: \n\tName: {Name}\n\tDescription: {Description}\n\tLevel: {Level}\n\tHealth: {Health}\n\tDamage: {Damage}");
        }
        */
    }
    public class Troll : Enemy, ILootable
    {
        public bool IsALive = true;

        public Troll(string name, string desc, int level, int health, int damage)
            : base(name, desc, level, health, damage) { }
        public override sealed void EngagementText()
        {
            Console.WriteLine("\"Time for da big troll attack!\"");
        }
        public override void StringText()
        {
            Console.WriteLine($"Enemy Details: \n\tName: {Name}\n\tDescription: {Description}\n\tLevel: {Level}\n\tHealth: {Health}\n\tDamage: {Damage}");
        }
        public sealed override void DealDamage(Hero HeroClass, int baseDamage)
        {
            int damage = (int)((baseDamage * HeroClass.Level) - (HeroClass.Armor * 0.10));
            Console.WriteLine($"The Troll is about to deal this much damage: {damage}");

            HeroClass.TakeDamage(damage);
        }
        public Item DropLoot()
        {
            Console.WriteLine("The troll dropped loot!");

            return new Item(name: "Troll Helmet", armor: 50, reqlvl: 50, incHealth: 25, incMana: 50);
        }
        public Potions DropPotion()
        {
            Potions p = new HealthPotions().DroppedPotion;
            Console.WriteLine($"The troll dropped a {p.PotionName}!");

            return p;
        }
        public override void TakeDamage(Hero h, int damage)
        {
            this.Health -= damage;
            Console.WriteLine($"Current Troll health: {this.Health}");

            if (this.Health <= 0)
            {
                IsALive = false;
            }

            if (!IsALive)
            {
                Console.WriteLine("The Troll has been slain!");
                h.InventoryBag.Add(DropLoot());
                h.InventoryBag.Add(DropPotion());
            }
        }
    }
}

namespace AvailableHeroes
{
    public class Mage : Hero
    {
        public Mage(string name, string heroclass, string desc, int level, int health, int mana, int armor)
            : base(name, heroclass, desc, level, health, mana, armor) { }


        public void CastFireball()
        {
            int manaCost = 10 * Level;

            if (ManaCheck(manaCost))
            {
                ReduceMana(manaCost);
                EngagementText("Casting fireball!");
            }
            else
            {
                EngagementText("I'm out of mana!");
            }
        }

        public void Teleport()
        {
            int manaCost = 30;

            if (ManaCheck(manaCost))
            {
                ReduceMana(manaCost);
                this.Y += 20;

                EngagementText("Teleporting ahead!");
                GetCurrentLocation();
            }
            else
            {
                EngagementText("Im out of mana!");
            }
        }
        public override void ViewLoot()
        {
            throw new NotImplementedException();
        }
        public override void ReduceMana(int manaCost)
        {
            this.Mana -= manaCost;
        }
        public override void EngagementText(string text)
        {
            Console.WriteLine(text);
        }

        public bool ManaCheck(int manaCost)
        {
            if (this.Mana >= manaCost) { return true; }
            else { return false; }
        }
        public override void TakeDamage(int amount)
        {
            Health -= amount;
        }

        public override void DealDamage(Enemy e, int amount)
        {
            throw new NotImplementedException();
        }
    }
    public class Necromancer : Hero, IDamageable
    {
        public Necromancer(string name, string heroclass, string desc, int level, int health, int mana, int armor)
            : base(name, heroclass, desc, level, health, mana, armor) { }

        public int SummonedSkeletons { get; set; } = 0;
        public int ResurrectedMinions { get; set; } = 0;
        public int SummonedGolem { get; set; } = 0;

        // Loot interaction
        public string ViewHealth => $"Current Health => {Health}";
        public override void ViewLoot()
        {
            if (this.InventoryBag.Count() == 0)
            {
                Console.WriteLine("No loot!");
                return;
            }

            int i = 1;
            foreach (var item in this.InventoryBag) 
            {
                Console.WriteLine($"ITEM {i}");
                Console.WriteLine(item.ToString());
                i++;
            }
        }
        public void PotionConsumption()
        {
            Console.Write("Select the item from your bag to consume: ");
            int.TryParse(Console.ReadLine(), out int x);

            try
            {
                if (this.InventoryBag[x] is Potions p)
                {
                    HealthPotions.ConsumePotion(this, p);
                    this.InventoryBag.Remove(p);
                }
                else
                {
                    Console.WriteLine("I can't consume that");
                }
            }
            catch
            {
                Console.WriteLine("Your bag isnt that big!");
            }
        }

        // Summoning spells
        public void SummonSkeleton()
        {
            int manaCost = 20 * Level;

            if (ManaCheck(manaCost))
            {
                ReduceMana(manaCost);

                EngagementText("Summoning a skeleton!");
                SummonedSkeletons += 1;
            }
            else
            {
                EngagementText("I can't summon a skeleton, I'm out of mana!");
            }
        }
        public void Resurrect()
        {
            int healthCost = 10;
            int manaCost = 25;

            if (this.Health - healthCost > 0 && ManaCheck(manaCost))
            {
                ReduceMana(manaCost);
                Health -= healthCost;

                EngagementText("You resurrected a minion!");
                ResurrectedMinions += 1;
            }
            else
            {
                EngagementText("I can't resurrect this minion!");
            }
        }
        public void SummonGolem()
        {
            int manaCost = 50;

            if (ManaCheck(manaCost))
            {
                Health += (int)(manaCost * 0.25);
                ReduceMana(manaCost);

                EngagementText("Summoned a golem and gaining health!");
                SummonedGolem += 1;
            }
            else
            {
                EngagementText("I can't summon a golem!");
            }
        }
        public void Sacrifice()
        {
            if (SummonedGolem > 0)
            {
                Health += 100;
                Mana += 100;

                EngagementText("Sacrified a golem and gaining health and mana!");
                SummonedGolem--;
            }
            else
            {
                EngagementText("No golems to sacrifice");
            }
        }
        public void CurrentMinions()
        {
            Console.WriteLine($"Skeletons: {SummonedSkeletons}");
            Console.WriteLine($"Resurrected Minions: {ResurrectedMinions}");
            Console.WriteLine($"Golems: {SummonedGolem}");
        }

        // Combat
        public override void TakeDamage(int amount)
        {
            Health -= amount;
        }
        public override void DealDamage(Enemy e, int amount)
        {
            e.TakeDamage(this, amount);
        }
        public override void EngagementText(string text)
        {
            Console.WriteLine(text);
        }
        private bool ManaCheck(int manaCost)
        {
            if (this.Mana >= manaCost) { return true; }
            else { return false; }
        }
        public override void ReduceMana(int manaCost)
        {
            this.Mana -= manaCost;
        }
    }
}

namespace ItemClass
{
    public class Item
    {
        public string Name { get; set; }
        public int Armor { get; set; }
        public int RequiredLevel { get; set; }
        public int IncreaseHealth { get; set; }
        public int IncreaseMana { get; set; }


        public Item(string name, int armor, int reqlvl, int incHealth, int incMana)
        {
            this.Name = name;
            this.Armor = armor;
            this.RequiredLevel = reqlvl;
            this.IncreaseHealth = incHealth;
            this.IncreaseMana = incMana;
        }

        public override string ToString()
        {
            StringBuilder itemText = new StringBuilder();
            itemText.AppendLine($"Item Name: {Name}");
            itemText.AppendLine($"Armor: +{Armor}");
            itemText.AppendLine($"Required Level: {RequiredLevel}");
            itemText.AppendLine($"Increase Health: +{IncreaseHealth}");
            itemText.AppendLine($"Increase Mana: +{IncreaseMana}");

            return itemText.ToString();
        }
    }
    public class HealthPotions : Potions
    {
        private static List<Potions> potions = new List<Potions>()
        {
            new Potions("Minor Health Potion", 5, 25),
            new Potions("Greater Health Potion", 25, 30),
            new Potions("Major Health Potion", 30, 100)
        };

        public HealthPotions() 
            : base ("", 0,0) { }

        public static void ConsumePotion(Hero hero, Potions healthPotion)
        {
            bool criticalHeal = hero.rand.Next(0, 2) == 0 ? true : false;
            int healAmount = hero.rand.Next(healthPotion.BaseValue, healthPotion.MaxValue);

            if (criticalHeal)
            {
                Console.WriteLine("A critical heal!");
                healAmount *= 2;
                Thread.Sleep(2000);
            }      
            hero.Health += healAmount;

            Console.WriteLine($"You just healed for +{healAmount}!");
        }
        public Potions DroppedPotion => potions[(new Random().Next(0, potions.Count))];
    }
}