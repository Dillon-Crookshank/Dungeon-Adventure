namespace DefaultNamespace;

internal class EnemyParty : AbstractParty
{

    private List<AbstractEnemy> enemyPartyComposition;

    // a crap constructor for now 
    EnemyParty(AbstractPlayerCharacter theEnemy)
    {
        if (enemyPartyComposition == null)
        {
            enemyPartyComposition = new List<AbstractPlayerCharacter>();
        }
        enemyPartyComposition.add(theEnemy);
    }

}