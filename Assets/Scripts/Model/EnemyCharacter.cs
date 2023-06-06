using System;

namespace DungeonAdventure
{

    /// <summary>
    /// An class that represents an EnemyCharacter.
    /// </summary>
    [Serializable]
    internal class EnemyCharacter : AbstractCharacter
    {

        /// <summary>
        /// Base constructor for an EnemyCharacter.
        /// </summary>
        /// <param name="theName">The name of the EnemyCharacter.</param>
        /// <param name="theHitpoints">The maximum hitpoints of the EnemyCharacter.</param>
        /// <param name="theAttack">The attack of the EnemyCharacter.</param>
        /// <param name="theDefence">The defence of the EnemyCharacter.</param>
        /// <param name="theMana">The maximum mana of the EnemyCharacter.</param>
        /// <param name="theInitiative">The initiative of the EnemyCharacter.</param>
        internal EnemyCharacter(in string theClass, in double theHitpoints, in double theAttack,
     in double theDefence, in double theMana, in int theInitiative) : base(theClass, theHitpoints, theAttack,
     theDefence, theMana, theInitiative)
        {
            // Important to differentiate between Enemies and Players, and leaves room for
            // eventual further distinctions such as levelling and loot tables.
        }

    }
}