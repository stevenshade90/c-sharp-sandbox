using AbstractClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOPPracticeInterfaces
{
    public interface IDamageable
    {
        void TakeDamage(int damageAmount);
    }

    public interface ILootable
    {
        void TakeDamage(Hero h, int damage);
    }

    public interface IConsumables
    {
        void ConsumePotion(Hero h, Potions p);
    }
}
