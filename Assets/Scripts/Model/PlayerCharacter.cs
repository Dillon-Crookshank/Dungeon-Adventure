using System;

namespace DungeonAdventure
{
    [Serializable]
    internal class PlayerCharacter : AbstractCharacter
    {

        /// <summary>
        /// Base constructor for a PlayerCharacter.
        /// </summary>
        /// <param name="theName">The name of the PlayerCharacter.</param>
        /// <param name="theHitpoints">The maximum hitpoints of the PlayerCharacter.</param>
        /// <param name="theAttack">The attack of the PlayerCharacter.</param>
        /// <param name="theDefence">The defence of the PlayerCharacter.</param>
        /// <param name="theMana">The maximum mana of the PlayerCharacter.</param>
        /// <param name="theInitiative">The initiative of the PlayerCharacter.</param>
        internal PlayerCharacter(in string theClass, in double theHitpoints, in double theAttack,
     in double theDefence, in double theMana, in int theInitiative) : base(theClass, theHitpoints, theAttack,
     theDefence, theMana, theInitiative)
        {
            // Important to differentiate between Players and Enemies, and leaves room for
            // eventual further distinctions such as levelling and loot tables.
            MyBuff = AccessDB.BuffDatabaseConstructor(theClass);
        }



    }
}