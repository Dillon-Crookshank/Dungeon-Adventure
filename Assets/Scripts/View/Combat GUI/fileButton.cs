using UnityEngine;

/// <summary>
/// An abstraction of a GUI button that handles file editing.
/// </summary>
abstract class fileButton : MonoBehaviour
{
    /// <summary>
    /// A reference to the rectangular backing of the cell.
    /// </summary>
    private SpriteRenderer rend;

    /// <summary>
    /// The GameEVent to be called whenever there is a file request.
    /// </summary>
    public GameEvent fileChangeRequest;

    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        Debug.Log(rend != null);
    }

    /// <summary>
    /// Highlights the cell when hovered over, performs an action when clicked on.
    /// </summary>
    void OnMouseOver()
    {
        rend.color = Color.green;
        if (Input.GetMouseButtonDown(0))
        {
            PressButton();
        }
    }

    /// <summary>
    /// Dehighlights the cell when the mouse no longer hovers over it.
    /// </summary>
    void OnMouseExit()
    {
        rend.color = Color.white;
    }

    public abstract void PressButton();
}
