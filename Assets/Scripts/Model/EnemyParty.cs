using System.Collections.Generic;
using DefaultNamespace;

internal class EnemyParty : AbstractParty
{

    internal EnemyParty(EnemyCharacter theEnemy)
    {
        partyPositions = new Dictionary<int, AbstractCharacter>();
        AddCharacter(theEnemy);
        isAllAlive = true;
    }



}