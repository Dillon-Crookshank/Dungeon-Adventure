using System.Collections.Generic;

namespace DefaultNamespace
{

    internal class PlayerParty : AbstractParty
    {

        internal PlayerParty(AbstractPlayerCharacter theHero)
        {
            partyPositions = new Dictionary<int, AbstractActor>();
            AddActor(theHero);
            isAllAlive = true;
        }

    }
}