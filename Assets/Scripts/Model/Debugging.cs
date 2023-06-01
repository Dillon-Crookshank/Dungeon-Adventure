// using System.Collections.Generic;
// using UnityEngine;

// namespace DefaultNamespace
// {

//     /// <summary>
//     /// A debugging class for testing purposes.
//     /// </summary>
//     internal class Debugging : MonoBehaviour
//     {
//         void Start()
//         {
//             PlayerCharacter hero = new PlayerCharacter("hero", 25, 5, 0, 10, 5);
//             PlayerCharacter hero2 = new PlayerCharacter("hero2", 25, 5, 0, 10, 5);
//             PlayerParty player = new PlayerParty(hero);
//             player.AddCharacter(hero2);
//             EnemyCharacter enemy = new EnemyCharacter("enemy", 25, 5, 0, 10, 5);
//             EnemyCharacter enemy2 = new EnemyCharacter("enemy2", 25, 5, 0, 10, 5);
//             EnemyParty enemies = new EnemyParty(enemy);
//             enemies.AddCharacter(enemy2);
//             EnemyPartyQueue test = new EnemyPartyQueue();
//         }
//     }
// }