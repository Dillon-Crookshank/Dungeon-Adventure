using System;
using System.Collections.Generic;
using DungeonAdventure;

/// <summary>
/// A static class that generates a Queue of EnemyParty for the Dungeon.
/// </summary>
internal static class EnemyPartyQueue
{

    /// <summary>
    /// The size of the EnemyPartyQueue to generate.
    /// </summary>
    private static int QUEUE_SIZE = 16;

    /// <summary>
    /// The possible enemy classes to choose from in Character construction.
    /// </summary>
    private static string[] classes = new string[] { "skeleton", "zombie", "goblin", "orc", "skeleton archer",
    "necromancer", "goblin archer"};

    /// <summary>
    /// Constructs a Queue of EnemyParty with randomly generated parties.
    /// </summary>
    /// <returns>A Queue of EnemyParty for the Dungeon.</returns>
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