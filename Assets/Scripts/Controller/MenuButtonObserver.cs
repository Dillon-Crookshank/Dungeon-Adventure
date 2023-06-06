using UnityEngine;

/// <summary>
/// The behaviour given to menu button GameObjects. 
/// </summary>
public class MenuButtonObserver : MonoBehaviour {
    /// <summary>
    /// The color that the button should be whenever not interacting with it.
    /// </summary>
    [SerializeField]
    private Color myDefaultColor;

    /// <summary>
    /// The color that the button should be while hovering over the button.
    /// </summary>
    [SerializeField]
    private Color myHoverColor;

    /// <summary>
    /// The color that the button should be during a left click.
    /// </summary>
    [SerializeField]
    private Color myLeftClickColor;

    /// <summary>
    /// The name of the method in the controller to call.
    /// </summary>
    [SerializeField]
    private string myListener;

    /// <summary>
    /// A flag used to make sure a click isn't registered twice.
    /// </summary>
    private bool myLeftPressFlag;

    /// <summary>
    /// Called when the Game Object is initialized.
    /// </summary>
    public void Start() {
        myLeftPressFlag = false;
    }

    /// <summary>
    /// Called each frame where the mouse is within the BoxCollider2D that is attached to the Game Object
    /// </summary>
    public void OnMouseOver() {
        ChangeColor(myHoverColor);

        //Left mouse events
        if (Input.GetMouseButtonDown(0)) {
            myLeftPressFlag = true;
            ChangeColor(myLeftClickColor);
        } else if (Input.GetMouseButtonUp(0) && myLeftPressFlag) {
            myLeftPressFlag = false;
            ChangeColor(myHoverColor);

            (GameObject.Find("Dungeon Controller")).SendMessage(myListener);
        }
    }

    /// <summary>
    /// Called once after every time the cursor leaves the button.
    /// </summary>
    public void OnMouseExit() {
        ChangeColor(myDefaultColor);
        myLeftPressFlag = false;
    }

    /// <summary>
    /// A helper method to simplify changing the color of the button.
    /// </summary>
    /// <param name="theColor"> The new color of the button. </param>
    private void ChangeColor(in Color theColor) {
        (this.gameObject.GetComponent<SpriteRenderer>()).color = theColor;
    }
}