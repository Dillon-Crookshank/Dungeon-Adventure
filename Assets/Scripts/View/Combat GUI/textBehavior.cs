using UnityEngine;

/// <summary>
/// Changes the text held in an assigned text mesh.
/// </summary>
sealed class textBehavior : MonoBehaviour
{
    /// <summary>
    /// The assigned text mesh to change. Defined in the editor.
    /// </summary>
    [SerializeField]
    private TextMesh myTextMesh;

    /// <summary>
    /// Changes the text held in an assigned text mesh.
    /// </summary>
    /// <param name="theText"> The text to set the text mesh. </param>
    void setText(string theText)
    {
        myTextMesh.text = theText;
    }
}
