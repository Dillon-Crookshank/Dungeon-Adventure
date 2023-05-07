using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textBehavior : MonoBehaviour
{
    public TextMesh textMesh;
    public void setString(Component sender, string data){
        textMesh.text = (sender.name);
    }
}
