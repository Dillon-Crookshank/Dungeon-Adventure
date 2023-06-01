using System;
using System.Collections.Generic;
using DefaultNamespace;

[Serializable]
internal class EnemyPartyQueue
{

    private string[] classes = new string[] { "skeleton", "zombie", "goblin", "orc", "skeleton archer",
    "necromancer", "goblin archer"};

    internal Queue<EnemyParty> enemies;

    internal EnemyPartyQueue()
    {
        Random rng = new Random();
        enemies = new Queue<EnemyParty>();


        for (int i = 0; i < 16; i++)
        {
            int partySize = rng.Next(1, 4);
            EnemyParty party = new EnemyParty();
            for (int j = 0; j < partySize; j++)
            {
                EnemyCharacter theEnemy = accessDB.EnemyDatabaseConstructor(classes[rng.Next(1, 7)]);
                party.AddCharacter(theEnemy);
            }
            enemies.Enqueue(party);
        }
    }

}