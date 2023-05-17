using System.Collections.Generic;
using System;
using static UnityEngine.Debug;

/// <summary>
/// The DungeonMap class is a collection of DungeonRooms held in a Graph, represented as an Adjacency List.
/// </summary>
internal class DungeonMap
{
    /// <summary>
    /// The minimum side length of a room
    /// </summary>
    private const int SIDE_MIN = 3;

    /// <summary>
    /// The maximum side length of a room
    /// </summary>
    private const int SIDE_MAX = 6;

    /// <summary>
    /// The absolute maximum x-coordinate in game units (The rooms can exist from -X_BOUND to X-BOUND)
    /// </summary>
    private const int X_BOUND = 32;

    /// <summary>
    /// The absolute maximum y-coordinate in game units (The rooms can exist from -Y_BOUND to Y-BOUND)
    /// </summary>
    private const int Y_BOUND = 18;

    /// <summary>
    /// The number of times the algorithm tries to place a random room. 
    /// This doesn't mean there will be 100 rooms, but 100 random rooms will be generated, 
    /// and only the ones that fit without colliding with an existing room will stay.
    /// </summary>
    private const int ROOM_ATTEMPTS = 100;

    /// <summary>
    /// Random number generator used within the dungeon generating algorithm
    /// </summary>
    private static Random myRand = new Random();

    /// <summary>
    /// The underlying adjacency list of the class.
    /// </summary>
    private List<GraphEntry> myAdjacencyList;

    /// <summary>
    /// A 2D array used to help with hallway generation. Rooms are placed on the grid and hallways are created using a simple path-finding algorithm.
    /// </summary>
    private int[,] myGrid;

    /// <summary>
    /// The GraphEntry that contains the room that is currently being focused.
    /// The focused room is the room where the user's party currently resides.
    /// </summary>
    private GraphEntry myFocusedRoom;

    /// <summary>
    /// A nested class, used as the building block of the adjacency list.
    /// </summary>
    private class GraphEntry {
        /// <summary>
        /// The dungeon room within this slot of the adjacency list.
        /// </summary>
        public DungeonRoom myRoom;

        /// <summary>
        /// The list of adjacent rooms.
        /// </summary>
        public List<GraphEntry> myAdjacentRooms;
        
        /// <summary>
        /// Used with hallway generation. When 2 rooms are connected by a hallway, their indices are noted in here so that the same hallway isn't drawn twice.
        /// </summary>
        public List<int> myConnectedRooms;

        /// <summary>
        /// A constructor that initializes given a room. **The adjacent rooms list needs to be initialized manually**
        /// </summary>
        /// <param name="theRoom"> The room in the GraphEntry. </param>
        public GraphEntry(in DungeonRoom theRoom) {
            myRoom = theRoom;
            myAdjacentRooms = new List<GraphEntry>();
            myConnectedRooms = new List<int>();
        }
    }

    /// <summary>
    /// Initializes a unique DungeonMap.
    /// </summary>
    public DungeonMap() {
        myAdjacencyList = new List<GraphEntry>();
        myGrid = new int[Y_BOUND * 2, X_BOUND * 2];
        
        GenerateDungeon(); 
    }

    /// <summary>
    /// Get the Room that the Map is currently focused on.
    /// The focused room should be the room the user's party currently resides.
    /// </summary>
    /// <returns> The focused room. </returns>
    public DungeonRoom GetFocusedRoom()
    {
        return myFocusedRoom.myRoom;
    }

    /// <summary>
    /// Given an index N, this method returns the Nth Adjacent room of the focused room. If N is out of bounds, a null reference is returned. 
    /// </summary>
    /// <param name="theIndex"> The index of the adjacent room. If out of bounds, a null reference is returned. </param>
    /// <returns> The Nth adjacent room of the focused room. If N is out of bounds, a null reference is returned. </returns>
    public DungeonRoom GetNthAdjacentRoom(int theIndex)
    {
        if (theIndex < 0 || theIndex >= myFocusedRoom.myAdjacentRooms.Count) {
            return null;
        }

        return myFocusedRoom.myAdjacentRooms[theIndex].myRoom;
    }

    /// <summary>
    /// Given an index N, this method returns the Nth room in the adjacency list.
    /// </summary>
    /// <param name="theIndex"> The index of the room in the base adjacency list. If out of bounds, a null reference is returned. </param>
    /// <returns> The Nth room in the adjacency list. If N is out of bounds, a nul reference is returned. </returns>
    public DungeonRoom GetNthRoom(int theIndex) {
        if (theIndex < 0 || theIndex >= myAdjacencyList.Count) {
            return null;
        }

        return myAdjacencyList[theIndex].myRoom;
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
    /// Initializes the adjacency list with a randomly generated dungeon.
    /// </summary>
    private void GenerateDungeon() {
        //Attempt to generate multiple rooms
        for (int n = 0; n < ROOM_ATTEMPTS; n++) {
            GenerateRandomGridRoom();
        }

        //Iterate through each room and place every valid hallway.
        int size = myAdjacencyList.Count;
        for (int i = 0; i < size; i++) {
            List<int[]> validHallways = GetValidHallways(i);

            while (validHallways.Count != 0) {
                //Get a random hallway from the valid hallways list
                int index = myRand.Next(validHallways.Count);
                int[] room = validHallways[index];
                
                //Check if the room connection was already made
                if (!myAdjacencyList[room[4]].myConnectedRooms.Contains(room[5])) {
                    DrawHallway(room);
                } 

                //Remove the hallway since we have either drawn it or determined that a hallway was already drawn between the rooms
                validHallways.RemoveAt(index);
            }
        }

        //Place initial focus on the room that was generated first.
        myFocusedRoom = myAdjacencyList[0];
    }

    /// <summary>
    /// Generates a random room snapped to a grid and tries to place it in the map. If it doesn't fit, the method returns without placing a room.
    /// </summary>
    private void GenerateRandomGridRoom() {
        int xMin, xMax, yMin, yMax, w, h;

        //Generate side lengths first so we can safely generate coordinates without violating bounds
        w = myRand.Next(SIDE_MIN, SIDE_MAX) - 1;
        h = myRand.Next(SIDE_MIN, SIDE_MAX) - 1;
        
        //Find the bottom left x andy value
        xMin = myRand.Next(-X_BOUND, X_BOUND - w);
        yMin = myRand.Next(-Y_BOUND, Y_BOUND - h);

        //Find the top right x and y values by using the width and height
        xMax = xMin + w;
        yMax = yMin + h;

        if (CheckGridSpace(xMin, xMax, yMin, yMax)) {
            myAdjacencyList.Add(new GraphEntry(new DungeonRoom(xMin, xMax, yMin, yMax)));
            StampRoomOnGrid(myAdjacencyList.Count, xMin, xMax, yMin, yMax);
        }
    }

    /// <summary>
    /// Given the Grid constraints of a 'virtual' room, the method checks if any rooms already exist too close.
    /// </summary>
    /// <param name="xMin"></param>
    /// <param name="xMax"></param>
    /// <param name="yMin"></param>
    /// <param name="yMax"></param>
    /// <returns> True if the room can fit in the map. False otherwise. </returns>
    private bool CheckGridSpace(int xMin, int xMax, int yMin, int yMax) {
        for (int y = yMin; y <= yMax; y++) {
            for (int x = xMin; x <= xMax; x++) {
                if (myGrid[y + Y_BOUND, x + X_BOUND] != 0) {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// Given the Grid constraints of an existing room, this method 'stamps' the rooms 'shadow' on a matrix, so that the CheckGridSpace() method knows it exists.
    /// </summary>
    /// <param name="theIndex"> The index of the DungeonRoom in the adjacency list. </param>
    /// <param name="xMin"></param>
    /// <param name="xMax"></param>
    /// <param name="yMin"></param>
    /// <param name="yMax"></param>
    private void StampRoomOnGrid(int theIndex, int xMin, int xMax, int yMin, int yMax) {
        for (int y = yMin; y <= yMax; y++) {
            for (int x = xMin; x <= xMax; x++) {
                myGrid[y + Y_BOUND, x + X_BOUND] = theIndex;
            }
        }

        //Place -1's over the perimeter of the room so that rooms aren't generated too close together.
        int y1 = yMin + Y_BOUND - 1;
        int y2 = yMax + Y_BOUND + 1;
        int x1 = xMin + X_BOUND - 1; 
        int x2 = xMax + X_BOUND + 1;

        for (int x = xMin + X_BOUND; x <= xMax + X_BOUND; x++) {
            if (y1 >= 0) myGrid[y1, x] = -1;
            if (y2 < Y_BOUND * 2) myGrid[y2, x] = -1;
        }

        for (int y = yMin + Y_BOUND; y <= yMax + Y_BOUND; y++) {
            if (x1 >= 0) myGrid[y, x1] = -1;
            if (x2 < X_BOUND * 2) myGrid[y, x2] = -1;
        }
    }

    /// <summary>
    /// Given the index of a room, this method collects every possible hallway in a list and returns it.
    /// Data in the form: {xMin, xMax,yMin, yMax, index0, index1}
    /// </summary>
    /// <param name="theIndex"></param>
    /// <returns> Returns a list of valid hallways. </returns>
    private List<int[]> GetValidHallways(int theIndex) {
        DungeonRoom room0 = myAdjacencyList[theIndex].myRoom;
        
        int xMin = room0.GetXMin();
        int xMax = room0.GetXMax();
        int yMin = room0.GetYMin();
        int yMax = room0.GetYMax();

        //A place to store everything needed to create multiple hallways
        List<int[]> validHallways = new List<int[]>();
        //0: xMin
        //1: xMax
        //2: yMin
        //3: yMax
        //4: otherIndex

        //Iterate over the perimeter
        for (int x = xMin; x <= xMax; x++) {
            // -y-axis
            int y0 = CastRay(x, yMin - 1, -1, false);

            if (y0 != -1 && y0 + 1 != yMin - 1) {
                //Possible hallway found!
                validHallways.Add(new int[] {x, x, y0 + 1, yMin - 1, myGrid[y0 + Y_BOUND, x + X_BOUND] - 1, theIndex});
            }

            // +y-axis
            int y1 = CastRay(x, yMax + 1, 1, false);

            if (y1 != -1 && y1 - 1 != yMax + 1) {
                //Possible hallway found!
                validHallways.Add(new int[] {x, x, y1 - 1, yMax + 1, myGrid[y1 + Y_BOUND, x + X_BOUND] - 1, theIndex});
            }
        }

        for (int y = yMin; y <= yMax; y++) {
            // -x-axis
            int x0 = CastRay(xMin - 1, y, -1, true);

            if (x0 != -1 && x0 + 1 != xMin - 1) {
                //Possible hallway found!
                validHallways.Add(new int[] {x0 + 1, xMin - 1, y, y, myGrid[y + Y_BOUND, x0 + X_BOUND] - 1, theIndex});
            }


            // +x-axis
            int x1 = CastRay(xMax + 1, y, 1, true);

            if (x1 != -1 && x1 - 1 != xMax + 1) {
                //Possible hallway found!
                validHallways.Add(new int[] {x1 - 1, xMax + 1, y, y, myGrid[y + Y_BOUND, x1 + X_BOUND] - 1, theIndex});
            }
        }


        return validHallways;
    }

    private void DrawHallway(int[] theRoom) {
        if (!CheckGridSpace(theRoom[0], theRoom[1], theRoom[2], theRoom[3])) {
            return;
        }
        
        myAdjacencyList.Add(new GraphEntry(new DungeonRoom(theRoom[0], theRoom[1], theRoom[2], theRoom[3])));

        //We stamp the hallway to the grid so that hallways in the future can connect to it
        
        //Hallway spans the y-axis
        if (theRoom[0] == theRoom[1]) {
            for (int y = theRoom[2]; y <= theRoom[3]; y++) {
                myGrid[theRoom[0] + X_BOUND, y + Y_BOUND] = myAdjacencyList.Count;
            }
        } 
        //Hallway spans the x-axis
        else {
            for (int x = theRoom[0]; x <= theRoom[1]; x++) {
                myGrid[x + X_BOUND, theRoom[2] + Y_BOUND] = myAdjacencyList.Count;
            }
        }

        //We now initialize the graph connections
        myAdjacencyList[theRoom[4]].myAdjacentRooms.Add(myAdjacencyList[myAdjacencyList.Count - 1]);
        myAdjacencyList[theRoom[5]].myAdjacentRooms.Add(myAdjacencyList[myAdjacencyList.Count - 1]);
        myAdjacencyList[myAdjacencyList.Count - 1].myAdjacentRooms.Add(myAdjacencyList[theRoom[4]]);
        myAdjacencyList[myAdjacencyList.Count - 1].myAdjacentRooms.Add(myAdjacencyList[theRoom[5]]);
        myAdjacencyList[theRoom[4]].myConnectedRooms.Add(theRoom[5]);
        myAdjacencyList[theRoom[5]].myConnectedRooms.Add(theRoom[4]);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="theX"> The initial x-coordinate. </param>
    /// <param name="theY"> The initial y-coordinate. </param>
    /// <param name="theStep"> The step length of the Ray. (if casting down/left, value should be negative)</param>
    /// <param name="theAxis"> Specify the Axis: True if x-axis, False if y-axis</param>
    /// <returns></returns>
    private int CastRay(int theX, int theY, int theStep, bool theAxis) {
        
        //x-axis3
        if (theAxis) {
            for (int x = theX; x >= -X_BOUND && x < X_BOUND; x += theStep) {
                //Check if the casted ray has reached a room
                if (myGrid[theY + Y_BOUND, x + X_BOUND] > 0) {
                    return x;
                }

                //Check if any rooms are adjacent to the casted ray
                if (theY + Y_BOUND != 0 && myGrid[theY + Y_BOUND - 1, x + X_BOUND] > 0
                    || theY + Y_BOUND != Y_BOUND * 2 - 1 && myGrid[theY + Y_BOUND + 1, x + X_BOUND] > 0) {
                    return -1;
                }
            }

            return -1;
        } 
        
        //y-axis
        else {
            for (int y = theY; y >= -Y_BOUND && y < Y_BOUND; y += theStep) {
                //Check if the casted ray has reached a room
                if (myGrid[y + Y_BOUND, theX + X_BOUND] > 0) {
                    return y;
                }

                //Check if any rooms are adjacent to the casted ray
                if (theX + X_BOUND != 0 && myGrid[y + Y_BOUND, theX + X_BOUND - 1] > 0
                    || theX + X_BOUND != X_BOUND * 2 - 1 && myGrid[y + Y_BOUND, theX + X_BOUND + 1] > 0) {
                    return -1;
                }
            }

            return -1;
        }
    }
}
