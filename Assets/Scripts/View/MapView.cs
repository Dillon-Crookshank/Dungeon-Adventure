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
    private Dictionary<int, TiledSprite> myRooms;

    /// <summary>
    /// The collection of sprite icon components
    /// </summary>
    private Dictionary<int, TiledSprite> myIcons;

    /// <summary>
    /// The lone constructor.
    /// </summary>
    /// <param name="theOrigin"> The origin of the Map. All components are created about this point. </param>
    /// <param name="theSprites"> The Sprite array used by the map. Index 0: PrimaryFocus, Index 1: Focused, Index 2: Unfocused. </param>
    public MapView(Vector2 theOrigin, Sprite[] theSprites) {
        myOrigin = theOrigin;
        mySprites = theSprites;
        myRooms = new Dictionary<int, TiledSprite>();
        myIcons = new Dictionary<int, TiledSprite>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="theRoom"></param>
    /// <param name="theSprite"></param>
    public void GiveSprite(DungeonRoom theRoom, Sprite theSprite) {
        //Create the TiledSprite if it doesn't exist yet
        if (!myRooms.ContainsKey(theRoom.GetID())) {
            myRooms.Add(
                theRoom.GetID(),
                new TiledSprite(
                    $"Room:{theRoom.GetID()}",
                    theSprite,
                    new Vector3(theRoom.GetX() + myOrigin.x, theRoom.GetY() + myOrigin.y, 0),
                    new Vector2(theRoom.GetW(), theRoom.GetH())
                )
            );

            //Add observer and listener scripts to add UI functionality
            myRooms[theRoom.GetID()].AddComponent(typeof(MapObserver));
            myRooms[theRoom.GetID()].AddComponent(typeof(ButtonListener));
        }
        else {
            //If the room already exists, just update it's sprite
            myRooms[theRoom.GetID()].SetSprite(theSprite);
        }
    }

    public void GiveAllSprite(Sprite theSprite) {
        foreach (TiledSprite button in myRooms.Values) {
            button.SetSprite(theSprite);
        }
    }

    //Create a "SetIcon" method that takes a dungeonRoom, checks if an icon already exists on it, and sets/creates an icon based on the given sprite/state

    /// <summary>
    /// Sets the primary focused room. Does not override any existing PrimaryFocused rooms, you must reset them beforehand.
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
        //Create the SlicedSprite if it doesn't exist yet
        if (!myRooms.ContainsKey(theRoom.GetID())) {
            myRooms.Add(
                theRoom.GetID(),
                new TiledSprite(
                    $"Room:{theRoom.GetID()}",
                    mySprites[0],
                    new Vector3(theRoom.GetX() + myOrigin.x, theRoom.GetY() + myOrigin.y, 0),
                    new Vector2(theRoom.GetW(), theRoom.GetH())
                )
            );

            //Add observer and listener scripts to add UI functionality
            myRooms[theRoom.GetID()].AddComponent(typeof(MapObserver));
            myRooms[theRoom.GetID()].AddComponent(typeof(ButtonListener));
        }
        else {
            myRooms[theRoom.GetID()].SetSprite(mySprites[(int)RoomFocus.Focused]);
        }
    }

    /// <summary>
    /// Resets the sprites of the Map View to unfocused sprites.
    /// </summary>
    public void UnfocusAll() {
        foreach (TiledSprite button in myRooms.Values) {
            button.SetSprite(mySprites[(int)RoomFocus.Unfocused]);
        }
    }

    /// <summary>
    /// Destroys every view component in the map
    /// </summary>
    public void ClearMap() {
        foreach (int id in myRooms.Keys) {
            myRooms[id].Destroy();
        }

        foreach (int id in myIcons.Keys) {
            myIcons[id].Destroy();
        }

        myRooms = new Dictionary<int, TiledSprite>();
        myIcons = new Dictionary<int, TiledSprite>();
    }

    /// <summary>
    /// Gives the given room an icon. Use "ClearIcon()" to remove the icon.
    /// </summary>
    /// <param name="theRoom"> The room that should be given an icon. </param>
    /// <param name="theIcon"> The sprite that should go on the room. </param>
    public void GiveIcon(DungeonRoom theRoom, Sprite theIcon) {
        //Make sure to create an icon if it doesn't exist already
        if (!myIcons.ContainsKey(theRoom.GetID())) {
            myIcons.Add(
                theRoom.GetID(),
                new TiledSprite(
                    $"Icon:{theRoom.GetID()}",
                    theIcon,
                    new Vector3(theRoom.GetX() + myOrigin.x, theRoom.GetY() + myOrigin.y, -0.1f),
                    new Vector2(1, 1)
                )
            );
        }

        //Simply change the sprite of the icon if it does exist
        else {
            myIcons[theRoom.GetID()].SetSprite(theIcon);
        }
    }

    /// <summary>
    /// Removes the icon from the given dungeon room.
    /// </summary>
    /// <param name="theRoom"> The room that should have its icon removed. </param>
    public void ClearIcon(DungeonRoom theRoom) {
        if (!myIcons.ContainsKey(theRoom.GetID())) {
            return;
        }

        //Destroy the underlying view component and remove the key-value pair from the dictionary
        myIcons[theRoom.GetID()].Destroy();
        myIcons.Remove(theRoom.GetID());
    }
}
