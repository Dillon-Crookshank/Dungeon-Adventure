using UnityEngine;

/// <summary>
/// An instanceable unity button. By default, the button turns blue when hovering over, turns green when left-clicking, and turns red when right-clicking.
/// </summary>
public class UButton {
    /// <summary>
    /// The underlying GameObject of the button.
    /// </summary>
    private GameObject myButton;

    /// <summary>
    /// The Lone Constructor to the class.
    /// </summary>
    /// <param name="theName"> The Name of the GameObject. </param>
    /// <param name="theSprite"> The initial Sprite of the button. The Sprite must be 9-sliced. </param>
    /// <param name="thePosition"> The Position of the button, centered on the button. </param>
    /// <param name="theSize"> The size of the button. </param>
    public UButton(string theName, Sprite theSprite, Vector3 thePosition, Vector2 theSize) {
        myButton = new GameObject(
            theName,
            typeof(SpriteRenderer),
            typeof(BoxCollider2D),
            typeof(ButtonMouseListener)
        );

        (myButton.GetComponent<SpriteRenderer>()).drawMode = SpriteDrawMode.Sliced;
        myButton.transform.localScale = new Vector3(1, 1, 0);

        SetSprite(theSprite);
        SetSize(theSize);
        SetPosition(thePosition);
    }

    /// <summary>
    /// A mutator for the size of the button.
    /// </summary>
    /// <param name="theSize"> The new size of the button. </param>
    public void SetSize(Vector2 theSize) {
        (myButton.GetComponent<BoxCollider2D>()).size = theSize;
        (myButton.GetComponent<SpriteRenderer>()).size = theSize;
    }

    /// <summary>
    /// A mutator for the position of the button.
    /// </summary>
    /// <param name="thePosition"> The new Position of the button. </param>
    public void SetPosition(Vector3 thePosition) {
        myButton.transform.localPosition = thePosition;
    }

    /// <summary>
    /// An accessor for the position of the button.
    /// </summary>
    /// <returns> The position of the button. </returns>
    public Vector3 GetPosition() {
        return myButton.transform.localPosition;
    }

    /// <summary>
    /// A mutator for the Sprite of the button. 
    /// </summary>
    /// <param name="theSprite"> The new Sprite of the button. The Sprite must be 9-sliced. </param>
    public void SetSprite(Sprite theSprite) {
        (myButton.GetComponent<SpriteRenderer>()).sprite = theSprite;
    }

    /// <summary>
    /// A mutator for the color of the button.
    /// </summary>
    /// <param name="theColor"> The new color of the button. </param>
    public void SetColor(Color theColor) {
        (myButton.GetComponent<SpriteRenderer>()).color = theColor;
    }

    /// <summary>
    /// Lets you add a Monobehaviour script to the button for added functionality.
    /// </summary>
    /// <param name="theComponent"> The type of the MonoBehavior script. </param>
    public void AddComponent(System.Type theComponent) {
        myButton.AddComponent(theComponent);
    }
}

/// <summary>
/// A behavior class that adds the color changing fuctionality to the UButton.
/// </summary>
class ButtonMouseListener : MonoBehaviour {
    /// <summary>
    /// A flag to prevent 'Drag Clicking' with the left mouse button.
    /// </summary>
    bool myLeftPressedFlag;

    /// <summary>
    /// A flag to prevent 'Drag Clicking' with the right mouse button.
    /// </summary>
    bool myRightPressedFlag;

    /// <summary>
    /// Called once, after the GameObject is initialized.
    /// </summary>
    public void Start() {
        myLeftPressedFlag = false;
        myRightPressedFlag = false;
    }

    /// <summary>
    /// Called once per frame when the cursor is over the button.
    /// </summary>
    public void OnMouseOver() {
        ChangeColor(Color.blue);

        //Left mouse events
        if (Input.GetMouseButtonDown(0)) {
            myLeftPressedFlag = true;
            ChangeColor(Color.green);
        } else if (Input.GetMouseButtonUp(0) && myLeftPressedFlag) {
            myLeftPressedFlag = false;
            ChangeColor(Color.blue);
        }

        //Right mouse Events
        if (Input.GetMouseButtonDown(1)) {
            myRightPressedFlag = true;
            ChangeColor(Color.red);
        } else if (Input.GetMouseButtonUp(1) && myRightPressedFlag) {
            myRightPressedFlag = false;
            ChangeColor(Color.blue);
        }
    }

    /// <summary>
    /// Called once after every time the cursor leaves the button.
    /// </summary>
    public void OnMouseExit() {
        ChangeColor(Color.white);
    }

    /// <summary>
    /// A helper method to simplify changing the color of the button.
    /// </summary>
    /// <param name="theColor"> The new color of the button. </param>
    private void ChangeColor(Color theColor) {
        (this.gameObject.GetComponent<SpriteRenderer>()).color = theColor;
    }
}
