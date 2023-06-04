using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combatLog : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TextMesh[] myLogs;

    void UpdateCombatLog (string theNewLog){
        for (int i = 0; i < myLogs.Length - 1; i++){
            myLogs[i].text = myLogs[i + 1].text;
        }
        myLogs[myLogs.Length - 1].text = theNewLog;
    }

    void ClearCombatLog(){
        for (int i = 0; i < myLogs.Length; i++){
            myLogs[i].text = "";
        }
    }

}
