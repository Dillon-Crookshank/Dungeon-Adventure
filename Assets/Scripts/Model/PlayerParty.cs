using System.Collections.Generic;

namespace DefaultNamespace
{

    internal class PlayerParty : AbstractParty
    {

        private List<AbstractPlayerCharacter> playerPartyComposition;

        PlayerParty(AbstractPlayerCharacter theHero)
        {
            if (playerPartyComposition == null)
            {
                playerPartyComposition = new List<AbstractPlayerCharacter>();
                playerPartyComposition.Add(theHero);
            }
        }

        private void Flee()
        {
            // write flee code here
        }
    }
}