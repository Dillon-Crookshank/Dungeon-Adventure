using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{

    /// <summary>
    /// A debugging class for testing purposes.
    /// </summary>
    internal class Debugging : MonoBehaviour
    {
        void Start()
        {
            testHero hero = new testHero("hero", 25, 5, 0, 10, 5);
            testHero hero2 = new testHero("hero2", 25, 5, 0, 10, 5);
            PlayerParty player = new PlayerParty(hero);
            player.AddActor(hero2);
            testEnemy enemy = new testEnemy("enemy", 25, 5, 0, 10, 5);
            testEnemy enemy2 = new testEnemy("enemy2", 25, 5, 0, 10, 5);
            EnemyParty enemies = new EnemyParty(enemy);
            enemies.AddActor(enemy2);
            Combat encounter = new Combat();
            List<AbstractActor> allChars = encounter.InitiativeRoll(player, enemies);
            Debug.Log(allChars.Count);
            allChars.Sort((x, y) => y.GetCombatInitiative() - x.GetCombatInitiative());
            foreach (AbstractActor character in allChars)
            {
                Debug.Log(character.toString());
            }
            PlayerCharacter testDatabase = accessDB.accessCharacterDatabase("warrior");
            PlayerCharacter testError = accessDB.accessCharacterDatabase("error");
            Debug.Log(testDatabase.toString());
        }
    }
}