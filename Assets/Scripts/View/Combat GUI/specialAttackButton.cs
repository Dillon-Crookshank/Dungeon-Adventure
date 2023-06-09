using UnityEngine;

namespace DungeonAdventure
{
    /// <summary>
    /// An action button corresponding to the "Special Attack" action.
    /// </summary>
    class specialAttackButton : actionButton
    {
        public override void PressButton()
        {
            for (int i = 1; i <= 6; i++)
            {
                GameObject.Find("E" + i).SendMessage("setBehaviorString", "DeliverSpecialAttack");
            }
        }
    }
}
