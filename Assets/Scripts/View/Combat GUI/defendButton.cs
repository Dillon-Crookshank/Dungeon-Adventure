using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DungeonAdventure {
    class defendButton : actionButton
    {
        public override void PressButton(){
            GameObject.Find("Dungeon Controller").SendMessage("Defend");
        }
    }
}

