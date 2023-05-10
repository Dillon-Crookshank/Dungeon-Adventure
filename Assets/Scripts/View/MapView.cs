using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that manages all the view components related to the Map.
/// </summary>
public class MapView {
    /// <summary>
    /// A nested enum to help identify the specific sprite being used.
    /// </summary>
    private enum RoomFocus {
        PrimaryFocus,
        Focused,
        Unfocused
    }

    /// <summary>
    /// The origin of the map.
    /// </summary>
    private Vector2 myOrigin;

    /// <summary>
    /// The sprites used by the Map.
    /// </summary>
    private Sprite[] mySprites;

    /// <summary>
    /// The collection of Sprite components.
    /// </summary>
    private Dictionary<int, SlicedSprite> myRooms;

    /// <summary>
    /// The lone constructor.
    /// </summary>
    /// <param name="theOrigin"> The origin of the Map. All components are created about this point. </param>
    /// <param name="theSprites"> The Sprite array used by the map. Index 0: PrimaryFocus, Index 1: Focused, Index 2: Unfocused. </param>
    public MapView(Vector2 theOrigin, Sprite[] theSprites) {
        myOrigin = theOrigin;
        mySprites = theSprites;
        myRooms = new Dictionary<int, SlicedSprite>();
    }

    /// <summary>
    /// Sets the primary focused room. Does not override any existing PrimaryFocused rooms.
    /// </summary>
    /// <param name="theRoom"></param>
    public void SetPrimaryFocus(DungeonRoom theRoom) {
        SetSecondaryFocus(theRoom);
        myRooms[theRoom.GetID()].SetSprite(mySprites[(int)RoomFocus.PrimaryFocus]);
    }

    /// <summary>
    /// Sets a room to be focused.
    /// </summary>
    /// <param name="theRoom"> The DungeonRoom to be focused. </param>
    public void SetSecondaryFocus(DungeonRoom theRoom) {
        //Create the SlicedSprite if it dosen't exist yet
        if (!myRooms.ContainsKey(theRoom.GetID())) {
            myRooms.Add(
                theRoom.GetID(),
                new SlicedSprite(
                    $"Room:{theRoom.GetID()}",
                    mySprites[0],
                    new Vector3(theRoom.GetX() + myOrigin.x, theRoom.GetY() + myOrigin.y, 0),
                    new Vector2(theRoom.GetW(), theRoom.GetH())
                )
            );

            //Add observer and listener scripts to add UI functionality
            myRooms[theRoom.GetID()].AddComponent(typeof(MapViewObserver));
            myRooms[theRoom.GetID()].AddComponent(typeof(ButtonMouseListener));
        }
        else {
            myRooms[theRoom.GetID()].SetSprite(mySprites[(int)RoomFocus.Focused]);
        }
    }

    /// <summary>
    /// Resets the sprites of the Map View to unfocused sprites.
    /// </summary>
    public void UnfocusAll() {
        foreach (SlicedSprite button in myRooms.Values) {
            button.SetSprite(mySprites[(int)RoomFocus.Unfocused]);
        }
    }

    /// <summary>
    /// Destroys every view component in the map
    /// </summary>
    public void Clear() {
        foreach (int id in myRooms.Keys) {
            myRooms[id].Destroy();
        }
    }
}
