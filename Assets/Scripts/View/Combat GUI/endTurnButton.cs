using UnityEngine;

namespace DungeonAdventure
{
    /// <summary>
    /// An action button corresponding to the "EndTurn" action.
    /// </summary>
    class endTurnButton : actionButton
    {
        public override void PressButton()
        {
            GameObject.Find("Dungeon Controller").SendMessage("EndTurn");
            for (int i = 1; i <= 6; i++)
            {
                GameObject.Find("E" + i).SendMessage("setBehaviorString", "");
            }
        }
    }
}
