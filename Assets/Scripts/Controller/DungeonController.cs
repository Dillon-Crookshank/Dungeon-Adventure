using System;
using UnityEngine;

/// <summary>
/// The Controller to the Dungeon Adventure Game. **Must atach this script to a 
/// </summary>
public class DungeonController : MonoBehaviour {
    /// <summary>
    /// The Sprites that are used by the Game. **Must initialize by using the Unity Editor**.
    /// </summary>
    [SerializeField]
    private Sprite[] mySprites;

    /// <summary>
    /// The View component that displays the Map.
    /// </summary>
    private MapView myMapView;

    /// <summary>
    /// The Model component that holds the Map.
    /// </summary>
    private DungeonMap myMapModel;

    /// <summary>
    /// The Start method is run once, after the DungeonController GameObject is initialized.
    /// </summary>
    public void Start() {
        myMapView = new MapView(new Vector2(0, 0), mySprites);
        myMapModel = new DungeonMap();
    }


    /// <summary>
    /// The Update method is called once per frame while the DungeonController GameObject is active.
    /// </summary>
    public void Update() {
        UpdateMapView();
    }

    /// <summary>
    /// Called once per frame to keep the MapView up to date with the MapModel.
    /// </summary>
    private void UpdateMapView() {
        //We call UnfocusAll to reset the mapView
        myMapView.UnfocusAll();
        myMapView.PrimaryFocusRoom(myMapModel.GetFocusedRoom());
        int i = 0;
        do {
            myMapView.FocusRoom(myMapModel.GetNthAdjacentRoom(i++));
        } while (myMapModel.GetNthAdjacentRoom(i) != null);
    }

    /// <summary>
    /// Called by the MapView whenever a valid adjacent room was left-clicked.
    /// </summary>
    /// <param name="theHashCode"> The hashcode of the DungeonRoom that the Primary Focus of the map should be set to.</param>
    public void MapViewListener(int theHashCode) {
        //We use the hashcode and look through the adjacent rooms until we find a matching hashcode
        int i = 0;
        while ( myMapModel.GetNthAdjacentRoom(i) != null
            && theHashCode != myMapModel.GetNthAdjacentRoom(i).GetHashCode()) {
            i++;
        }

        //Use the index to update the model if the index is valid.
        if (myMapModel.GetNthAdjacentRoom(i) != null) {
            myMapModel.MoveToNthAdjacentRoom(i);
        }
    }
}


