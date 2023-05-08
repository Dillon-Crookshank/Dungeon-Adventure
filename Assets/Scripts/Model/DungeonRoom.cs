using UnityEngine;
namespace DefaultNamespace
{

    /// <summary>
    /// Debugging class for now
    /// </summary>
    internal class DungeonRoom : MonoBehaviour
    {
        void Start()
        {
            {
                testHero TESTHERO = new testHero("TESTHERO", 25, 5, 0, 10, 5);
                testEnemy TESTENEMY = new testEnemy("TESTENEMY", 25, 5, 0, 10, 5);
                Debug.Log(TESTHERO.GetName());
                Debug.LogFormat("{0}/{1}, {2}", TESTHERO.GetCurrentHitpoints(), TESTHERO.GetMaxHitpoints(),
                 TESTHERO.GetAttack());
                TESTHERO.basicAttack(TESTENEMY);
                Debug.Log(TESTENEMY.GetName());
                Debug.LogFormat("{0}/{1}, {2}", TESTENEMY.GetCurrentHitpoints(), TESTENEMY.GetMaxHitpoints(),
                 TESTENEMY.GetAttack());
            }

        }

    }
}