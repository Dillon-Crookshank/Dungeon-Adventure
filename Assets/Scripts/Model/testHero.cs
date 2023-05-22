using System;

namespace DefaultNamespace
{

    internal class testHero : AbstractPlayerCharacter
    {

        public testHero(string theName, double theHitpoints, double theAttack,
         double theDefence, double theMana, int theInitiative) : base(theName, theHitpoints, theAttack,
         theDefence, theMana, theInitiative)
        {

        }


        /// <summary>
        /// A basic attack that simply takes deals the attack value - theTargets 
        /// defence value as damage, with a minimum of 1.0.
        /// </summary>
        /// <param name="theTarget">The <see cref"AbstractActor"/> being targeted with this attack.</param>
        internal bool basicAttack(AbstractActor theTarget)
        {
            double theDamage = (Math.Max(1, GetAttack() - theTarget.GetDefence()));
            theTarget.SetCurrentHitpoints(-1 * theDamage);
            return true;
        }

        /// <summary>
        /// A heavier attack that uses 5 mana, deals twice the damage, and ignores half the targets armor.
        /// </summary>
        /// <param name="theTarget"></param>
        internal void heavyAttack(AbstractEnemy theTarget)
        {
            if (GetCurrentMana() < 5.0)
            {
                // dont allow attack and dont use up turn
            }
            SetCurrentMana(-5);
            double theDamage = Math.Max(1.0, 2.0 * (GetAttack() - (theTarget.GetDefence() / 2.0)));
            theTarget.SetCurrentHitpoints(-1 * theDamage);
        }


    }
}