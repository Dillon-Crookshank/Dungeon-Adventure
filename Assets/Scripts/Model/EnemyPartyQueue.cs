using System;
using System.Collections.Generic;
using DungeonAdventure;

internal static class EnemyPartyQueue
{

    private static int QUEUE_SIZE = 16;

    private static string[] classes = new string[] { "skeleton", "zombie", "goblin", "orc", "skeleton archer",
    "necromancer", "goblin archer"};

    static internal Queue<EnemyParty> CreateEnemyQueue()
    {
        Random rng = new Random();
        Queue<EnemyParty> enemies = new Queue<EnemyParty>();


        for (int i = 0; i < QUEUE_SIZE; i++)
        {
            int partySize = rng.Next(1, 4);
            EnemyParty party = new EnemyParty();
            for (int j = 0; j < partySize; j++)
            {
                EnemyCharacter theEnemy = AccessDB.EnemyDatabaseConstructor(classes[rng.Next(1, 7)]);
                party.AddCharacter(theEnemy);
            }
            enemies.Enqueue(party);
        }

        return enemies;
    }
}