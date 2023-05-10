using UnityEngine;
using System;

/// <summary>
/// A behavior class that adds the color changing fuctionality to the UButton.
/// </summary>
public class ButtonMouseListener : MonoBehaviour {
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