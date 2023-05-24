using System.Collections.Generic;
using DefaultNamespace;

internal class EnemyParty : AbstractParty
{

    internal EnemyParty(AbstractEnemy theEnemy)
    {
        partyPositions = new Dictionary<int, AbstractCharacter>();
        AddActor(theEnemy);
        isAllAlive = true;
    }



}