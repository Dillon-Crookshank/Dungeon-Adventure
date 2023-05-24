using System.Collections.Generic;

namespace DefaultNamespace
{

    internal class PlayerParty : AbstractParty
    {

        internal PlayerParty(AbstractPlayerCharacter theHero)
        {
            partyPositions = new Dictionary<int, AbstractCharacter>();
            AddActor(theHero);
            isAllAlive = true;
        }

    }
}