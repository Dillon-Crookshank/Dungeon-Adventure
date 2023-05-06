using System.Collections.Generic;

/// <summary>
/// The DungeonMap class is a collection of DungeonRooms held in a Graph, represented as an Adjacency List.
/// </summary>
internal class DungeonMap
{
    /// <summary>
    /// The Underlying Adjacency list of the class.
    /// </summary>
    private List<MapEntry> myAdjacentcyList;

    /// <summary>
    /// The room that is currently being focused.
    /// </summary>
    private MapEntry myFocusedRoom;

    /// <summary>
    /// A nested class, used as the building block of the adjacentcy list.
    /// </summary>
    private class MapEntry {
        /// <summary>
        /// The dungeon room within this slot of the adjacentcy list.
        /// </summary>
        public DungeonRoom myRoom;

        /// <summary>
        /// The list of adjacent rooms.
        /// </summary>
        public List<MapEntry> myAdjacentRooms;

        /// <summary>
        /// A constructor that initializes given a room. **The adjacent rooms list needs to be initialized manualy**
        /// </summary>
        /// <param name="theRoom"></param>
        public MapEntry(in DungeonRoom theRoom) {
            myRoom = theRoom;
            myAdjacentRooms = new List<MapEntry>();
        }
    }

    /// <summary>
    /// Initializes a unique DungeonMap. **WIP: For now, an example map is created**
    /// </summary>
    public DungeonMap() {
        myAdjacentcyList = new List<MapEntry>();

        //Hard Coded example map
        //Initialize rooms
        myAdjacentcyList.Add(new MapEntry(new DungeonRoom(-4, 4, 5, 5)));
        myAdjacentcyList.Add(new MapEntry(new DungeonRoom(-4, 0, 1, 3)));
        myAdjacentcyList.Add(new MapEntry(new DungeonRoom(-4, -4, 5, 5)));
        myAdjacentcyList.Add(new MapEntry(new DungeonRoom(0, -4, 3, 1)));
        myAdjacentcyList.Add(new MapEntry(new DungeonRoom(4, -4, 5, 5)));
        myAdjacentcyList.Add(new MapEntry(new DungeonRoom(4, 0, 1, 3)));
        myAdjacentcyList.Add(new MapEntry(new DungeonRoom(4, 4, 5, 5)));

        //Initalize Graph connections
        myAdjacentcyList[0].myAdjacentRooms.Add(myAdjacentcyList[1]);

        myAdjacentcyList[1].myAdjacentRooms.Add(myAdjacentcyList[0]);
        myAdjacentcyList[1].myAdjacentRooms.Add(myAdjacentcyList[2]);

        myAdjacentcyList[2].myAdjacentRooms.Add(myAdjacentcyList[1]);
        myAdjacentcyList[2].myAdjacentRooms.Add(myAdjacentcyList[3]);

        myAdjacentcyList[3].myAdjacentRooms.Add(myAdjacentcyList[2]);
        myAdjacentcyList[3].myAdjacentRooms.Add(myAdjacentcyList[4]);

        myAdjacentcyList[4].myAdjacentRooms.Add(myAdjacentcyList[3]);
        myAdjacentcyList[4].myAdjacentRooms.Add(myAdjacentcyList[5]);

        myAdjacentcyList[5].myAdjacentRooms.Add(myAdjacentcyList[4]);
        myAdjacentcyList[5].myAdjacentRooms.Add(myAdjacentcyList[6]);

        myAdjacentcyList[6].myAdjacentRooms.Add(myAdjacentcyList[5]);

        //Set current room
        myFocusedRoom = myAdjacentcyList[0];
    }

    /// <summary>
    /// Get the Room that the Map is currently focused on.
    /// </summary>
    /// <returns></returns>
    public DungeonRoom GetFocusedRoom()
    {
        return myFocusedRoom.myRoom;
    }

    /// <summary>
    /// Given an index N, this method returns the Nth Adjacent room. If  N is out of bounds, a null reference is returned. 
    /// </summary>
    /// <param name="index"> The index of the adjacent room. If out of bounds, a null reference is returned. </param>
    /// <returns></returns>
    public DungeonRoom GetNthAdjacentRoom(int index)
    {
        if (index < 0 || index >= myFocusedRoom.myAdjacentRooms.Count) {
            return null;
        }

        return myFocusedRoom.myAdjacentRooms[index].myRoom;
    }

    /// <summary>
    /// Using the same index from GetNthAdjacentRoom, the method switches the focused room to the given adjacent room index.
    /// </summary>
    /// <param name="index"> The index of the adjacent room that the new focused room should be set to. </param>
    public void MoveToNthAdjacentRoom(int index)
    {
        if (index < 0 || index >= myFocusedRoom.myAdjacentRooms.Count) {
            return;
        }

        myFocusedRoom = myFocusedRoom.myAdjacentRooms[index];
    }
}
