using System;

namespace DefaultNamespace
{

    /// <summary>
    /// An abstract representation of an Enemy.
    /// Includes a RNG and a lootTable.
    /// </summary>
    [Serializable]
    internal class EnemyCharacter : AbstractCharacter
    {

        // TODO: private List<Item> lootTable;

        /// <summary>
        /// Base constructor for an AbstractEnemy.
        /// </summary>
        /// <param name="theName">The name of the AbstractEnemy.</param>
        /// <param name="theHitpoints">The maximum hitpoints of the AbstractEnemy.</param>
        /// <param name="theAttack">The attack of the AbstractEnemy.</param>
        /// <param name="theDefence">The defence of the AbstractEnemy.</param>
        /// <param name="theMana">The maximum mana of the AbstractEnemy.</param>
        /// <param name="theInitiative">The initiative of the AbstractEnemy.</param>
        internal EnemyCharacter(in string theClass, in double theHitpoints, in double theAttack,
     in double theDefence, in double theMana, in int theInitiative) : base(theClass, theHitpoints, theAttack,
     theDefence, theMana, theInitiative)
        {
            //TODO: lootTable = new List<Item>();
        }

        //TODO: lootRoll

        //TODO: decideAction

    }
}