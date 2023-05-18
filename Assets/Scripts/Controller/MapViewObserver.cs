using UnityEngine;
using System;

/// <summary>
/// The observer to the listener. This observes each map room individuals. When a room is left-clicked, its hashcode is sent to the GameController for processing.
/// </summary>
public class MapViewObserver : MonoBehaviour {
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
        //We use a boolean to make sure the user did not 'drag click'.
        if (Input.GetMouseButtonDown(0)) {
            myLeftPressedFlag = true;
        }
        else if (Input.GetMouseButtonUp(0) && myLeftPressedFlag) {
            myLeftPressedFlag = false;

            //We isolate the hashcode from the name so we can send it to the GameController.
            int ID = Int32.Parse((this.name.Split(':'))[1]);
            (GameObject.FindWithTag("GameController")).SendMessage("MapViewListener", ID);
        }
    }
}