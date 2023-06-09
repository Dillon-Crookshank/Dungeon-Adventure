using UnityEngine;

namespace DungeonAdventure
{
    /// <summary>
    /// An action button corresponding to the "Attack" action.
    /// </summary>
    class attackButton : actionButton
    {
        public override void PressButton()
        {
            for (int i = 1; i <= 6; i++)
            {
                GameObject.Find("E" + i).SendMessage("setBehaviorString", "DeliverBasicAttack");
            }
        }
    }
}
