using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DungeonAdventure
{
    class attackButton : actionButton
    {
        public override void PressButton()
        {
            if (Input.GetMouseButtonDown(0))
            {
                for (int i = 1; i <= 6; i++)
                {
                    GameObject.Find("E" + i).SendMessage("setBehaviorString", "DeliverBasicAttack");
                }
            }
        }
    }
}

