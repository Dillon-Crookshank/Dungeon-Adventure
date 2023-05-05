using System;
using System.Collections.Generic;

namespace DefaultNamespace
{

    internal abstract class AbstractEnemy : AbstractActor
    {

        Random enemyRng;

        //private List<Item> lootTable;

        internal AbstractEnemy(string theName, double theHitpoints, double theAttack,
     double theDefence, double theMana, int theInitiative) : base(theName, theHitpoints, theAttack,
     theDefence, theMana, theInitiative)
        {
            enemyRng = new Random();
            //lootTable = new List<Item>();
        }

        private int randomizeAction(int theActionCount)
        {
            int num = (enemyRng.Next(1, theActionCount));
            return num;
        }

    }
}