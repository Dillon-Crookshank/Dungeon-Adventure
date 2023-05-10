using System.Collections.Generic;
using System;

/// <summary>
/// The DungeonMap class is a collection of DungeonRooms held in a Graph, represented as an Adjacency List.
/// </summary>
internal class DungeonMap
{
    private const int SIDE_MIN = 3;
    private const int SIDE_MAX = 8;
    private const int X_BOUND = 32;
    private const int Y_BOUND = 18;
    private const float MIN_ALLEY_SPACE = 1.5f;

    private const int ATTEMPTS = 50;

    private static Random myRand = new Random();


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
        GenerateDungeon();
        //FullExample();
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
    /// <param name="theIndex"> The index of the adjacent room. If out of bounds, a null reference is returned. </param>
    /// <returns></returns>
    public DungeonRoom GetNthAdjacentRoom(int theIndex)
    {
        if (theIndex < 0 || theIndex >= myFocusedRoom.myAdjacentRooms.Count) {
            return null;
        }

        return myFocusedRoom.myAdjacentRooms[theIndex].myRoom;
    }

    /// <summary>
    /// Given an index N, this method retuns the Nth room in the adjacency list.
    /// </summary>
    /// <param name="theIndex"> The index of the room in the base adjacency list. If out of bounds, a null reference is returned. </param>
    /// <returns></returns>
    public DungeonRoom GetNthRoom(int theIndex) {
        if (theIndex < 0 || theIndex >= myAdjacentcyList.Count) {
            return null;
        }

        return myAdjacentcyList[theIndex].myRoom;
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


    private void GenerateDungeon() {
        //Attempt to generate multiple rooms
        for (int n = 0; n < ATTEMPTS; n++) {
            CreateRandomRoom();
        }

        //Place initial focus on the room that was generated first.
        myFocusedRoom = myAdjacentcyList[0];
    }

    /// <summary>
    /// Used in the procedural algorithm. Creates a random room with side lengths that varry from 3 to 5 and tries to randomly place it without colliding with other rooms
    /// </summary>
    private void CreateRandomRoom() {
        float width = (float) myRand.NextDouble() * (SIDE_MAX - SIDE_MIN) + SIDE_MIN;
        float height = (float) myRand.NextDouble() * (SIDE_MAX - SIDE_MIN) + SIDE_MIN;
        float x = (float) ((myRand.NextDouble() - 0.5) * 2 * (X_BOUND - MIN_ALLEY_SPACE - width / 2.0));
        float y = (float) ((myRand.NextDouble() - 0.5) * 2 * (Y_BOUND - MIN_ALLEY_SPACE - height / 2.0));
        
        if (!CheckCollision(x, y, width, height)) {
            myAdjacentcyList.Add(new GraphEntry(new DungeonRoom(x, y, width, height)));
        }
    }



    /// <summary>
    /// This method checks if the given room bounds collides with any existing rooms.
    /// </summary>
    /// <param name="theX"> The x-coordinate of the room. </param>
    /// <param name="theY"> The y-coordinate of the room. </param>
    /// <param name="theW"> The width of the room. </param>
    /// <param name="theH"> The height of the room. </param>
    /// <returns> True if the given room colides with an existing room. False otherwise. </returns>
    private bool CheckCollision(float theX, float theY, float theW, float theH) {
        for (int i = 0; i < myAdjacentcyList.Count; i++) {
            if (Math.Abs(myAdjacentcyList[i].myRoom.GetX() - theX) < (myAdjacentcyList[i].myRoom.GetW() + theW) / 2.0 + MIN_ALLEY_SPACE 
                && Math.Abs(myAdjacentcyList[i].myRoom.GetY() - theY) < (myAdjacentcyList[i].myRoom.GetH() + theH) / 2.0 + MIN_ALLEY_SPACE) {
                return true;
            }
        }

        return false;
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
