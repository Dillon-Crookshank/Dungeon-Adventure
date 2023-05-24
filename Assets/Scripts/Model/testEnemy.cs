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
        /// <param name="theTarget">The <see cref"AbstractCharacter"/> being targeted with this attack.</param>
        private bool basicAttack(AbstractCharacter theTarget)
        {
            if (theTarget.GetType() == typeof(AbstractEnemy))
            {
                return false;
            }
            double theDamage = Math.Max(1, attack - theTarget.defence);
            theTarget.currentHitpoints = (-1 * theDamage);
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
            if (currentMana < 5)
            {
                return false;
            }
            currentMana = -5;
            double theDamage = Math.Max(1, 2 * (attack - (theTarget.defence / 2)));
            theTarget.currentHitpoints = (-1 * theDamage);
            return true;
        }

    }
}