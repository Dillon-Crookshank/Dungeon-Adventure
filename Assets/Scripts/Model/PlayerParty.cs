using System;
using System.Collections.Generic;

namespace DungeonAdventure
{
    [Serializable]
    internal class PlayerParty : AbstractParty
    {

        internal PlayerParty(PlayerCharacter theHero)
        {
            partyPositions = new Dictionary<int, AbstractCharacter>();
            AddCharacter(theHero);
            isAllAlive = true;
        }

    }
}