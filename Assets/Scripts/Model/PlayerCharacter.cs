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
        }



    }
}