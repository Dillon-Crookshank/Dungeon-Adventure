using UnityEngine;
using System;

/// <summary>
/// The observer to the listener. This observes each map room individuals. When a room is left-clicked, its ID is sent to the GameController for processing.
/// </summary>
public class MapObserver : MonoBehaviour {
    /// <summary>
    /// We use this flag to prevent 'Drag Clicks' from being registered as a valid click.
    /// </summary>
    private bool myLeftPressedFlag;

    /// <summary>
    /// Called once, after the GameObject this script is attached to is initialized.
    /// </summary>
    public void Start() {
        myLeftPressedFlag = false;
    }

    /// <summary>
    /// Called every frame that the cursor is over the 2D BoxCollider Bounds.
    /// </summary>
    public void OnMouseOver() {
        ChangeColor(Color.blue);

        //We use a boolean to make sure the user did not 'drag click'.
        if (Input.GetMouseButtonDown(0)) {
            myLeftPressedFlag = true;
        }
        
        else if (Input.GetMouseButtonUp(0) && myLeftPressedFlag) {
            myLeftPressedFlag = false;

            //We isolate the ID from the name so we can send it to the GameController.
            int ID = Int32.Parse((this.name.Split(':'))[1]);
            (GameObject.Find("Dungeon Controller")).SendMessage("MapViewListener", ID);
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