using System.Collections.Generic;

/// <summary>
/// The DungeonMap class is a collection of DungeonRooms held in a Graph, represented as an Adjacency List.
/// </summary>
internal class DungeonMap
{
    /// <summary>
    /// The underlying adjacency list of the class.
    /// </summary>
    private List<GraphEntry> myAdjacentcyList;

    /// <summary>
    /// The GraphEntry that contains the room that is currently being focused.
    /// </summary>
    private GraphEntry myFocusedRoom;

    /// <summary>
    /// A nested class, used as the building block of the adjacentcy list.
    /// </summary>
    private class GraphEntry {
        /// <summary>
        /// The dungeon room within this slot of the adjacentcy list.
        /// </summary>
        public DungeonRoom myRoom;

        /// <summary>
        /// The list of adjacent rooms.
        /// </summary>
        public List<GraphEntry> myAdjacentRooms;

        /// <summary>
        /// A constructor that initializes given a room. **The adjacent rooms list needs to be initialized manualy**
        /// </summary>
        /// <param name="theRoom"> The room in the GraphEntry. </param>
        public GraphEntry(in DungeonRoom theRoom) {
            myRoom = theRoom;
            myAdjacentRooms = new List<GraphEntry>();
        }
    }

    /// <summary>
    /// Initializes a unique DungeonMap. **WIP: For now, an example map is created**
    /// </summary>
    public DungeonMap() {
        myAdjacentcyList = new List<GraphEntry>();

        //Hard Coded example map
        FullExample();
        //SimpleExample();
    }

    /// <summary>
    /// Get the Room that the Map is currently focused on.
    /// </summary>
    /// <returns> The focused room. </returns>
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
    public void FocusNthAdjacentRoom(int index)
    {
        if (index < 0 || index >= myFocusedRoom.myAdjacentRooms.Count) {
            return;
        }

        myFocusedRoom = myFocusedRoom.myAdjacentRooms[index];
    }

    /// <summary>
    /// 
    /// </summary>
    private void SimpleExample() {
        //Create DungeonRooms
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(-4, 4, 5, 5)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(-4, 0, 1, 3)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(-4, -4, 5, 5)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(0, -4, 3, 1)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(4, -4, 5, 5)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(4, 0, 1, 3)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(4, 4, 5, 5)));

        //Create Graph Connections
        AddAdjacentRooms(0, new int[] {1});
        AddAdjacentRooms(1, new int[] {0, 2});
        AddAdjacentRooms(2, new int[] {1, 3});
        AddAdjacentRooms(3, new int[] {2, 4});
        AddAdjacentRooms(4, new int[] {3, 5});
        AddAdjacentRooms(5, new int[] {4, 6});
        AddAdjacentRooms(6, new int[] {5});

        //Set initial room
        myFocusedRoom = myAdjacentcyList[0];
    }

    /// <summary>
    /// 
    /// </summary>
    private void FullExample() {
        //Create DungeonRooms
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(-13.5f, 6.5f, 3.0f, 3.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(-10.0f, 7.5f, 4, 1)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(-6.0f, 5.5f, 4.0f, 5.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(-1.5f, 5.5f, 5.0f, 1.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(3.5f, 4.5f, 5.0f, 5.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(8.5f, 5.5f, 5.0f, 1.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(12.5f, 5.5f, 3.0f, 3.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(12.5f, 2.0f, 1.0f, 4.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(3.5f, 0.5f, 1.0f, 3.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(-4.5f, 2.0f, 1.0f, 2.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(-11.5f, 0.5f, 3.0f, 3.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(-8.0f, 0.5f, 4.0f, 1.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(-4.0f, -1.0f, 4.0f, 4.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(4.5f, -1.5f, 13.0f, 1.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(12.5f, -1.5f, 3.0f, 3.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(7.5f, -3.5f, 1.0f, 3.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(0.5f, -3.0f, 1.0f, 2.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(-11.5f, -1.5f, 1.0f, 1.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(-11.0f, -4.0f, 4.0f, 4.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(0.0f, -6.0f, 4.0f, 4.0f)));
        myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(7.5f, -6.5f, 3.0f, 3.0f)));

        //Create Graph Connections
        AddAdjacentRooms(0, new int[] {1});
        AddAdjacentRooms(1, new int[] {0, 2});
        AddAdjacentRooms(2, new int[] {1, 3, 9});
        AddAdjacentRooms(3, new int[] {2, 4});
        AddAdjacentRooms(4, new int[] {3, 5, 8});
        AddAdjacentRooms(5, new int[] {4, 6});
        AddAdjacentRooms(6, new int[] {5, 7});
        AddAdjacentRooms(7, new int[] {6, 14});
        AddAdjacentRooms(8, new int[] {4, 13});
        AddAdjacentRooms(9, new int[] {2, 12});
        AddAdjacentRooms(10, new int[] {11, 17});
        AddAdjacentRooms(11, new int[] {10, 12});
        AddAdjacentRooms(12, new int[] {9, 11, 13});
        AddAdjacentRooms(13, new int[] {8, 12, 14, 15, 16});
        AddAdjacentRooms(14, new int[] {7, 13});
        AddAdjacentRooms(15, new int[] {13, 20});
        AddAdjacentRooms(16, new int[] {13, 19});
        AddAdjacentRooms(17, new int[] {10, 18});
        AddAdjacentRooms(18, new int[] {17});
        AddAdjacentRooms(19, new int[] {16});
        AddAdjacentRooms(20, new int[] {15});

        //Set initial room
        myFocusedRoom = myAdjacentcyList[0];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="theMainRoom"></param>
    /// <param name="theAdjacentRooms"></param>
    private void AddAdjacentRooms(int theMainRoom, int[] theAdjacentRooms) {
        foreach (int i in theAdjacentRooms) {
            myAdjacentcyList[theMainRoom].myAdjacentRooms.Add(myAdjacentcyList[i]);
        }
    }
}