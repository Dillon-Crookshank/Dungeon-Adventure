using UnityEngine;

namespace DungeonAdventure
{
    /// <summary>
    /// An action button corresponding to the "Defend" action.
    /// </summary>
    class defendButton : actionButton
    {
        public override void PressButton()
        {
            GameObject.Find("Dungeon Controller").SendMessage("Defend");
            for (int i = 1; i <= 6; i++)
            {
                GameObject.Find("E" + i).SendMessage("setBehaviorString", "");
            }
        }
    }
}
