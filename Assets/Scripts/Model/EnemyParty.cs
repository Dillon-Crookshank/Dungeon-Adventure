using System.Collections.Generic;
using System;

namespace DefaultNamespace {

[Serializable]
internal class EnemyParty : AbstractParty
{

    internal EnemyParty(EnemyCharacter theEnemy)
    {
        partyPositions = new Dictionary<int, AbstractCharacter>();
        AddCharacter(theEnemy);
        isAllAlive = true;
    }

    internal EnemyParty()
    {
        partyPositions = new Dictionary<int, AbstractCharacter>();
        isAllAlive = true;
    }



}
}