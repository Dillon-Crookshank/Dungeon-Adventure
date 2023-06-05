using UnityEngine;

sealed class textBehavior : MonoBehaviour
{
    [SerializeField]
    private TextMesh myTextMesh;

    void setText(string theText){
        myTextMesh.text = theText;
    }
}
