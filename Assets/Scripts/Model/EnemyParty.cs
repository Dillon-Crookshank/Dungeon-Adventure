using System.Collections.Generic;
using System;

namespace DungeonAdventure
{

    /// <summary>
    /// A collection of Enemy Characters.
    /// </summary>
    [Serializable]
    internal class EnemyParty : AbstractParty
    {

        /// <summary>
        /// Constructor for EnemyParty with the first EnemyCharacter as a parameter.
        /// </summary>
        /// <param name="theEnemy">The first EnemyCharacter in the party.</param>
        internal EnemyParty(in EnemyCharacter theEnemy)
        {
            partyPositions = new Dictionary<int, AbstractCharacter>();
            AddCharacter(theEnemy);
            isAllAlive = true;
        }

        /// <summary>
        /// Constructor for EnemyParty.
        /// </summary>
        internal EnemyParty()
        {
            partyPositions = new Dictionary<int, AbstractCharacter>();
            isAllAlive = true;
        }

    }
}