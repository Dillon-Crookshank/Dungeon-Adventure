using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DungeonAdventure {
    class specialAttackButton : actionButton
    {
        public override void PressButton(){
            for (int i = 1; i <= 6; i++){
                GameObject.Find("E" + i).SendMessage("setBehaviorString", "DeliverSpecialAttack");
            }
        }
    }
}

