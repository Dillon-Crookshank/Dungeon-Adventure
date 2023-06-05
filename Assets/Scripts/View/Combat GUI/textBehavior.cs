using UnityEngine;

sealed class textBehavior : MonoBehaviour
{
    [SerializeField]
    TextMesh textMesh;

    void setText(string theText){
        textMesh.text = theText;
    }
}
