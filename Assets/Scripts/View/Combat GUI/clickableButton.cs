using UnityEngine;

/// <summary>
/// An abstraction of a GUI button that handles file editing.
/// </summary>
namespace DungeonAdventure
{
    public abstract class clickableButton : MonoBehaviour
    {

        [SerializeField]
        public Color myHighlightColor; 
        /// <summary>
        /// A reference to the rectangular backing of the cell.
        /// </summary>
        private SpriteRenderer myRenderer;

        void Start()
        {
            myRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Highlights the cell when hovered over, performs an action when clicked on.
        /// </summary>
        void OnMouseOver()
        {
            if (myHighlightColor != null){
                myRenderer.color = myHighlightColor;
            } else {
                myRenderer.color = Color.green;
            }
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
            myRenderer.color = Color.white;
        }
        public abstract void PressButton();
    }
}
