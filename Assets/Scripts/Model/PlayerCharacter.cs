using System;

namespace DefaultNamespace
{

    internal class PlayerCharacter : AbstractCharacter
    {

        /// <summary>
        /// The current experience of the party member.
        /// When specific thresholds are reached, the party member levels up.
        /// Represented as a double underneath, but should be shown as a floored integer.
        /// </summary>
        private double experience;

        //TODO: private List<Equipment> Gear;


        /// <summary>
        /// Base constructor for a PlayerCharacter.
        /// </summary>
        /// <param name="theName">The name of the AbstractPlayerCharacter.</param>
        /// <param name="theHitpoints">The maximum hitpoints of the AbstractPlayerCharacter.</param>
        /// <param name="theAttack">The attack of the AbstractPlayerCharacter.</param>
        /// <param name="theDefence">The defence of the AbstractPlayerCharacter.</param>
        /// <param name="theMana">The maximum mana of the AbstractPlayerCharacter.</param>
        /// <param name="theInitiative">The initiative of the AbstractPlayerCharacter.</param>
        internal PlayerCharacter(in string theClass, in double theHitpoints, in double theAttack,
     in double theDefence, in double theMana, in int theInitiative) : base(theClass, theHitpoints, theAttack,
     theDefence, theMana, theInitiative)
        {
            experience = 0.0;
            //TODO: Gear = new List<Equipment>();
        }

        //TODO: useItem

        //TODO: decideAction

        /// <summary>
        /// A basic attack that simply takes deals the attack value - theTargets 
        /// defence value as damage, with a minimum of 1.0.
        /// </summary>
        /// <param name="theTarget">The <see cref"AbstractCharacter"/> being targeted with this attack.</param>
        internal bool basicAttack(AbstractCharacter theTarget)
        {
            double theDamage = (Math.Max(1, Attack - theTarget.Defence));
            theTarget.CurrentHitpoints = (-1 * theDamage);
            return true;
        }

        /// <summary>
        /// A heavier attack that uses 5 mana, deals twice the damage, and ignores half the targets armor.
        /// </summary>
        /// <param name="theTarget"></param>
        internal void heavyAttack(AbstractEnemy theTarget)
        {
            if (CurrentMana < 5.0)
            {
                // dont allow attack and dont use up turn
            }
            CurrentMana = -5;
            double theDamage = Math.Max(1.0, 2.0 * (Attack - (theTarget.Defence / 2.0)));
            theTarget.CurrentHitpoints = (-1 * theDamage);

        }
    }
}