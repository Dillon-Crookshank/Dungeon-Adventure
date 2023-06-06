using UnityEngine;

sealed class textBehavior : MonoBehaviour
{
    public TextMesh textMesh;

    void setText(string theText){
        textMesh.text = theText;
    }
}
