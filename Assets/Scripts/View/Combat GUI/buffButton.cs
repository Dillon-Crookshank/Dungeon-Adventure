using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DungeonAdventure {
    class buffButton : actionButton
    {
        public override void PressButton(){
            GameObject.Find("Dungeon Controller").SendMessage("Buff");
        }
    }
}

