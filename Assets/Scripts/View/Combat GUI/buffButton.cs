using UnityEngine;

namespace DungeonAdventure
{
    /// <summary>
    /// An action button corresponding to the "Buff" action.
    /// </summary>
    class buffButton : actionButton
    {
        public override void PressButton()
        {
            GameObject.Find("Dungeon Controller").SendMessage("Buff");
            for (int i = 1; i <= 6; i++)
            {
                GameObject.Find("E" + i).SendMessage("setBehaviorString", "");
            }
        }
    }
}
