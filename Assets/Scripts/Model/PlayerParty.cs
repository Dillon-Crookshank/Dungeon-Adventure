using System;
using System.Collections.Generic;

namespace DefaultNamespace
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