using UnityEngine;

/// <summary>
/// A button that provides an action.
/// </summary>
namespace DungeonAdventure
{
    class actionButton : clickableButton
    {
        /// <summary>
        /// Determines whether or not this button is clickable.
        /// </summary>
        private bool myClickability;

        /// <summary>
        /// The color to set the sprite renderer when this button is not clickable.
        /// </summary>
        private Color myLockedColor = new Color(0.2f, 0.2f, 0.2f, 1f);

        /// <summary>
        /// The string to display in the action help header. Defined in the editor.
        /// </summary>
        [SerializeField]
        private string myDisplayHeader;

        /// <summary>
        /// The string to display in the action help description. Defined in the editor.
        /// </summary>
        [SerializeField]
        private string myDisplayDescription;

        /// <summary>
        /// The reference to the sprite renderer of the object.
        /// </summary>
        private SpriteRenderer myActionRenderer;

        void Start()
        {
            myActionRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (!myClickability)
            {
                myActionRenderer.color = myLockedColor;
            }
        }

        void OnMouseOver()
        {
            if (myClickability)
            {
                GameObject.Find("ActionHeader").SendMessage("setText", myDisplayHeader);
                GameObject.Find("ActionDescription").SendMessage("setText", myDisplayDescription);
                myActionRenderer.color = myHighlightColor;
                if (Input.GetMouseButtonDown(0))
                {
                    PressButton();
                }
            }
        }

        void OnMouseExit()
        {
            if (myClickability)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                GameObject.Find("ActionHeader").SendMessage("setText", "");
                GameObject.Find("ActionDescription").SendMessage("setText", "");
            }
        }

        /// <summary>
        /// The actions that happen when this button is clicked (implemented in a child class).
        /// </summary>
        public override void PressButton() { }

        /// <summary>
        /// Changes the description string that appears when the button is hovered.
        /// </summary>
        public void SetDescription(in string theDescription)
        {
            myDisplayDescription = theDescription;
        }

        /// <summary>
        /// Changes whether or not this button can be clicked.
        /// </summary>
        /// <summary>
        /// <param name="theClickability"> The current clickability of the button. </param>
        void SetClickable(bool theClickability)
        {
            myClickability = theClickability;
        }
    }
}
