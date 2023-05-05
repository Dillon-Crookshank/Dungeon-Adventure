using System;
using System.Collections.Generic;
namespace DefaultNamespace
{

    internal abstract class testEnemy : AbstractEnemy
    {

        private testEnemy(string theName, double theHitpoints, double theAttack,
         double theDefence, double theMana, int theInitiative) : base(theName, theHitpoints, theAttack,
         theDefence, theMana, theInitiative)
        {

        }

        // private enum Actions
        // {
        //     Action1 = basicAttack,
        //     Action2 = heavyAttack,
        //     Action3 = testEnemy.defend()
        // }

        /// <summary>
        /// A basic attack that simply takes deals the attack value - theTargets 
        /// defence value as damage, with a minimum of 1.0.
        /// </summary>
        /// <param name="theTarget">The <see cref"AbstractActor"/> being targeted with this attack.</param>
        private void basicAttack(AbstractActor theTarget)
        {
            double theDamage = Math.Max(1, attack - theTarget.getDefence());
            theTarget.setCurrentHitpoints(theTarget.getCurrentHitpoints() - theDamage);
        }

        /// <summary>
        /// A heavier attack that uses 5 mana, deals twice the damage, and ignores half the targets armor.
        /// </summary>
        /// <param name="theTarget"></param>
        private void heavyAttack(AbstractEnemy theTarget)
        {
            if (mana < 5.0)
            {
                // dont allow attack and dont use up turn
            }
            mana -= 5.0;
            double theDamage = Math.Max(1.0, 2.0 * (attack - (theTarget.getDefence() / 2.0)));
            theTarget.setCurrentHitpoints(theTarget.getCurrentHitpoints() - theDamage);
        }

        private void defend()
        {
            defence += 5;
        }


    }
}