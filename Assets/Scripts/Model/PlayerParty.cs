using System;
using System.Collections.Generic;

namespace DungeonAdventure
{

    /// <summary>
    /// A container for Player Characters.
    /// </summary>
    [Serializable]
    internal class PlayerParty : AbstractParty
    {


        /// <summary>
        /// Constructs a PlayerParty given a hero.
        /// </summary>
        /// <param name="theHero">The first PlayerCharacter of the PlayerParty.</param>
        internal PlayerParty(in PlayerCharacter theHero)
        {
            partyPositions = new Dictionary<int, AbstractCharacter>();
            AddCharacter(theHero);
            isAllAlive = true;
        }

    }
}