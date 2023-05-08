using System;
namespace DefaultNamespace
{

    internal class testEnemy : AbstractEnemy
    {

        internal testEnemy(string theName, double theHitpoints, double theAttack,
         double theDefence, double theMana, int theInitiative) : base(theName, theHitpoints, theAttack,
         theDefence, theMana, theInitiative)
        {
            // TODO: loot table
            // TODO: moveset
        }


        /// <summary>
        /// A basic attack that simply takes deals the attack value - theTargets 
        /// defence value as damage, with a minimum of 1.0.
        /// </summary>
        /// <param name="theTarget">The <see cref"AbstractActor"/> being targeted with this attack.</param>
        private bool basicAttack(AbstractActor theTarget)
        {
            if (theTarget.GetType() == typeof(AbstractEnemy))
            {
                return false;
            }
            double theDamage = Math.Max(1, GetAttack() - theTarget.GetDefence());
            theTarget.SetCurrentHitpoints(-1 * theDamage);
            return true;
        }

        /// <summary>
        /// A heavier attack that uses 5 mana, deals twice the damage, and ignores half the targets armor.
        /// </summary>
        /// <param name="theTarget"></param>
        private bool heavyAttack(AbstractEnemy theTarget)
        {
            if (theTarget.GetType() == typeof(AbstractEnemy))
            {
                return false;
            }
            if (GetCurrentMana() < 5)
            {
                return false;
            }
            SetCurrentMana(-5);
            double theDamage = Math.Max(1, 2 * (GetAttack() - (theTarget.GetDefence() / 2)));
            theTarget.SetCurrentHitpoints(-1 * theDamage);
            return true;
        }

    }
}