using System.Collections.Generic;
using DefaultNamespace;

internal class EnemyParty : AbstractParty
{

    private List<AbstractEnemy> enemyPartyComposition;


    private EnemyParty(AbstractEnemy theEnemy)
    {
        if (enemyPartyComposition == null)
        {
            enemyPartyComposition = new List<AbstractEnemy>();
        }
        enemyPartyComposition.Add(theEnemy);
    }

}