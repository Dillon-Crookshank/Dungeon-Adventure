using System.Collections.Generic;
using System;

/// <summary>
/// The DungeonMap class is a collection of DungeonRooms held in a Graph. 
/// Uses the Iterator design pattern to implicitly store the current location of a user party within the map.
/// </summary>
[Serializable]
public class DungeonMap
{
    /// <summary>
    /// The minimum side length of a room.
    /// </summary>
    private const int SIDE_MIN = 3;

    /// <summary>
    /// The maximum side length of a room.
    /// </summary>
    private const int SIDE_MAX = 6;

    /// <summary>
    /// The absolute maximum x-coordinate in game units (The rooms can exist from -X_BOUND to X-BOUND).
    /// </summary>
    private const int X_BOUND = 32;

    /// <summary>
    /// The absolute maximum y-coordinate in game units (The rooms can exist from -Y_BOUND to Y-BOUND).
    /// </summary>
    private const int Y_BOUND = 18;

    /// <summary>
    /// The number of times the algorithm tries to place a random room. 
    /// This doesn't mean there will be 100 rooms, but 100 random rooms will be generated, 
    /// and only the ones that fit without colliding with an existing room will stay.
    /// </summary>
    private const int ROOM_ATTEMPTS = 100;

    /// <summary>
    /// The minimum number of rooms that the algorithm expects to be traversable. If an isolated section of the map has less rooms than this, it is deleted.
    /// </summary>
    private const int EXPECTED_VOLUME = 50;

    /// <summary>
    /// The number of enemy parties that are placed in the map.
    /// </summary>
    private const int ENEMY_AMOUNT = 16;

    /// <summary>
    /// Random number generator used within the dungeon generating algorithm.
    /// </summary>
    private static Random myRand = new Random();

    /// <summary>
    /// The underlying adjacency list of the class.
    /// </summary>
    private List<GraphNode> myAdjacencyList;

    /// <summary>
    /// A 2D array used to help with hallway generation. Rooms are placed on the grid and hallways are created using a simple path-finding algorithm.
    /// </summary>
    private int[,] myGrid;

    /// <summary>
    /// The GraphEntry that contains the room that is currently being focused.
    /// The focused room is the room where the user's party currently resides.
    /// </summary>
    private GraphNode myFocusedRoom;

    /// <summary>
    /// A nested class, used as the building block of the adjacency list.
    /// </summary>
    [Serializable]
    private class GraphNode
    {
        /// <summary>
        /// The dungeon room within this slot of the adjacency list.
        /// </summary>
        public DungeonRoom myRoom;

        /// <summary>
        /// The list of adjacent rooms.
        /// </summary>
        public List<GraphNode> myAdjacentRooms;

        /// <summary>
        /// Used with hallway generation. When 2 rooms are connected by a hallway, their indices are noted in here so that the same hallway isn't drawn twice.
        /// </summary>
        public List<DungeonRoom> myConnectedRooms;

        /// <summary>
        /// A constructor that initializes given a room. **The adjacent rooms list needs to be initialized manually**
        /// </summary>
        /// <param name="theRoom"> The room in the GraphEntry. </param>
        public GraphNode(in DungeonRoom theRoom)
        {
            myRoom = theRoom;
            myAdjacentRooms = new List<GraphNode>();
            myConnectedRooms = new List<DungeonRoom>();
        }
    }

    /// <summary>
    /// Initializes a unique DungeonMap.
    /// </summary>
    public DungeonMap()
    {
        myAdjacencyList = new List<GraphNode>();
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
    public DungeonRoom GetNthAdjacentRoom(in int theIndex)
    {
        if (theIndex < 0 || theIndex >= myFocusedRoom.myAdjacentRooms.Count)
        {
            return null;
        }

        return myFocusedRoom.myAdjacentRooms[theIndex].myRoom;
    }

    /// <summary>
    /// Given an index N, this method returns the Nth room in the adjacency list.
    /// </summary>
    /// <param name="theIndex"> The index of the room in the base adjacency list. If out of bounds, a null reference is returned. </param>
    /// <returns> The Nth room in the adjacency list. If N is out of bounds, a nul reference is returned. </returns>
    public DungeonRoom GetNthRoom(in int theIndex)
    {
        if (theIndex < 0 || theIndex >= myAdjacencyList.Count)
        {
            return null;
        }

        return myAdjacencyList[theIndex].myRoom;
    }

    /// <summary>
    /// Using the same index from GetNthAdjacentRoom, the method switches the focused room to the given adjacent room index.
    /// </summary>
    /// <param name="index"> The index of the adjacent room that should be focused. </param>
    public void FocusNthAdjacentRoom(in int index)
    {
        //Make sure that the index is valid
        if (index < 0 || index >= myFocusedRoom.myAdjacentRooms.Count)
        {
            return;
        }

        myFocusedRoom = myFocusedRoom.myAdjacentRooms[index];

        //Mark any adjacent rooms as 'seen'
        foreach (GraphNode entry in myFocusedRoom.myAdjacentRooms)
        {
            entry.myRoom.SetSeenFlag(true);
        }
    }

    /// <summary>
    /// Returns the number of rooms that are in the map.
    /// </summary>
    /// <returns> The number of rooms in the map. </returns>
    public int GetRoomCount()
    {
        return myAdjacencyList.Count;
    }

    /// <summary>
    /// Focuses the Nth room in the adjacency list.
    /// </summary>
    /// <param name="index"> The index of the room that should be focused. </param>
    public void FocusNthRoom(in int index)
    {
        //Make sure that the index is valid
        if (index < 0 || index >= myAdjacencyList.Count)
        {
            return;
        }

        myFocusedRoom = myAdjacencyList[index];

        //Since this one can be arbitrarily selected, we don't know if the focused room has already been marked as 'seen'
        myFocusedRoom.myRoom.SetSeenFlag(true);


        //Mark any adjacent rooms as 'seen'
        foreach (GraphNode entry in myFocusedRoom.myAdjacentRooms)
        {
            entry.myRoom.SetSeenFlag(true);
        }
    }

    /// <summary>
    /// Initializes the adjacency list with a randomly generated dungeon.
    /// </summary>
    private void GenerateDungeon()
    {
        //Attempt to generate multiple rooms
        for (int n = 0; n < ROOM_ATTEMPTS; n++)
        {
            GenerateRandomGridRoom();
        }

        //Clear the -1's from the grid so we can properly use CheckGridSpace() with hallways
        for (int y = 0; y < Y_BOUND * 2; y++)
        {
            for (int x = 0; x < X_BOUND * 2; x++)
            {
                if (myGrid[y, x] == -1)
                {
                    myGrid[y, x] = 0;
                }
            }
        }


        int size = myAdjacencyList.Count;
        //Iterate through each room and place every valid hallway.
        for (int i = 0; i < size; i++)
        {
            List<int[]> validHallways = GetValidHallways(i);

            while (validHallways.Count != 0)
            {
                //Get a random hallway from the valid hallways list
                int index = myRand.Next(validHallways.Count);
                int[] room = validHallways[index];

                //Attempt to place the hallway
                DrawHallway(room);

                //Remove the hallway since we have either drawn it or determined that the hallway wouldn't fit with existing rooms
                validHallways.RemoveAt(index);
            }
        }

        size -= RemoveIsolatedRooms();
        FocusNthRoom(0);

        //Randomly fill the dungeon with enemies
        for (int n = 0; n < ENEMY_AMOUNT;)
        {
            int index = myRand.Next(1, size);
            if (!myAdjacencyList[index].myRoom.GetEnemyFlag())
            {
                myAdjacencyList[index].myRoom.SetEnemyFlag(true);
                n++;
            }
        }

    }

    /// <summary>
    /// Generates a random room snapped to a grid and tries to place it in the map. If it doesn't fit, the method returns without placing a room.
    /// </summary>
    private void GenerateRandomGridRoom()
    {
        int xMin, xMax, yMin, yMax, w, h;

        //Generate side lengths first so we can safely generate coordinates without violating bounds
        w = myRand.Next(SIDE_MIN, SIDE_MAX) - 1;
        h = myRand.Next(SIDE_MIN, SIDE_MAX) - 1;

        //Find the bottom left x and y value
        xMin = myRand.Next(-X_BOUND, X_BOUND - w);
        yMin = myRand.Next(-Y_BOUND, Y_BOUND - h);

        //Find the top right x and y values by using the width and height
        xMax = xMin + w;
        yMax = yMin + h;

        if (CheckGridSpace(xMin, xMax, yMin, yMax))
        {
            myAdjacencyList.Add(new GraphNode(new DungeonRoom(xMin, xMax, yMin, yMax)));
            myAdjacencyList[myAdjacencyList.Count - 1].myRoom.SetID(myAdjacencyList.Count - 1);
            StampRoomOnGrid(myAdjacencyList.Count, xMin, xMax, yMin, yMax);
        }
    }

    /// <summary>
    /// Given the Grid domain of a 'virtual' room, the method checks if any rooms already exist too close.
    /// </summary>
    /// <param name="xMin"> The minimum x-coordinate of the domain. </param>
    /// <param name="xMax"> The maximum x-coordinate of the domain. </param>
    /// <param name="yMin"> The minimum y-coordinate of the domain. </param>
    /// <param name="yMax"> The maximum y-coordinate of the domain. </param>
    /// <param name="tolerance"> The minimum grid value needed to have the specified domain be invalid. For example: -1 for regular rooms and 1 for hallways. </param>
    /// <returns> True if the room can fit in the map. False otherwise. </returns>
    private bool CheckGridSpace(in int xMin, in int xMax, in int yMin, in int yMax)
    {
        //Make sure that any of the input isn't out of bounds
        if ((xMin < -X_BOUND || xMin >= X_BOUND)
            || (xMax < -X_BOUND || xMax >= X_BOUND)
            || (yMin < -Y_BOUND || yMin >= Y_BOUND)
            || (yMax < -Y_BOUND || yMax >= Y_BOUND))
        {
            return false;
        }

        //Check each element in the matrix within the given domain.
        for (int y = yMin; y <= yMax; y++)
        {
            for (int x = xMin; x <= xMax; x++)
            {
                if (myGrid[y + Y_BOUND, x + X_BOUND] != 0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// Given the Grid constraints of an existing room, this method 'stamps' the rooms shadow on the matrix 'myGrid', so that the CheckGridSpace() method knows it exists.
    /// </summary>
    /// <param name="theIndex"> The index of the DungeonRoom in the adjacency list. </param>
    /// <param name="xMin"> The minimum x-coordinate of the domain. </param>
    /// <param name="xMax"> The maximum x-coordinate of the domain. </param>
    /// <param name="yMin"> The minimum y-coordinate of the domain. </param>
    /// <param name="yMax"> The maximum y-coordinate of the domain. </param>
    private void StampRoomOnGrid(in int theIndex, in int xMin, in int xMax, in int yMin, in int yMax)
    {
        for (int y = yMin; y <= yMax; y++)
        {
            for (int x = xMin; x <= xMax; x++)
            {
                myGrid[y + Y_BOUND, x + X_BOUND] = theIndex;
            }
        }

        //Place -1's over the perimeter of the room so that rooms aren't generated too close together.
        int y1 = yMin + Y_BOUND - 1;
        int y2 = yMax + Y_BOUND + 1;
        int x1 = xMin + X_BOUND - 1;
        int x2 = xMax + X_BOUND + 1;

        for (int x = xMin + X_BOUND; x <= xMax + X_BOUND; x++)
        {
            if (y1 >= 0) myGrid[y1, x] = -1;
            if (y2 < Y_BOUND * 2) myGrid[y2, x] = -1;
        }

        for (int y = yMin + Y_BOUND; y <= yMax + Y_BOUND; y++)
        {
            if (x1 >= 0) myGrid[y, x1] = -1;
            if (x2 < X_BOUND * 2) myGrid[y, x2] = -1;
        }
    }

    /// <summary>
    /// Given the index of a room, this method collects every possible hallway in a list and returns it.
    /// Data in the form: {xMin, xMax, yMin, yMax, index0, index1}
    /// </summary>
    /// <param name="theIndex"> The index of the DungeonRoom in the adjacency list. </param>
    /// <returns> Returns a list of valid hallways. </returns>
    private List<int[]> GetValidHallways(in int theIndex)
    {
        DungeonRoom room0 = myAdjacencyList[theIndex].myRoom;

        //Get the domain of the room
        int xMin = room0.GetXMin();
        int xMax = room0.GetXMax();
        int yMin = room0.GetYMin();
        int yMax = room0.GetYMax();

        //A place to store everything needed to create multiple hallways
        List<int[]> validHallways = new List<int[]>();

        //Iterate over the perimeter
        for (int x = xMin; x <= xMax; x++)
        {
            // -y-axis
            int y0 = PathFindHallway(x, yMin - 1, 0, -1).Item2;

            if (y0 > -Y_BOUND && yMin - y0 > 1)
            {
                //Possible hallway found!
                validHallways.Add(new int[] { x, x, y0 + 1, yMin - 1, theIndex, myGrid[y0 + Y_BOUND, x + X_BOUND] - 1 });
            }

            // +y-axis
            int y1 = PathFindHallway(x, yMax + 1, 0, 1).Item2;

            if (y1 != Y_BOUND && y1 - yMax > 1)
            {
                //Possible hallway found!
                validHallways.Add(new int[] { x, x, yMax + 1, y1 - 1, theIndex, myGrid[y1 + Y_BOUND, x + X_BOUND] - 1 });
            }
        }
        for (int y = yMin; y <= yMax; y++)
        {
            // -x-axis
            int x0 = PathFindHallway(xMin - 1, y, -1, 0).Item1;

            if (x0 > -X_BOUND && xMin - x0 > 1)
            {
                //Possible hallway found!
                validHallways.Add(new int[] { x0 + 1, xMin - 1, y, y, theIndex, myGrid[y + Y_BOUND, x0 + X_BOUND] - 1 });
            }


            // +x-axis
            int x1 = PathFindHallway(xMax + 1, y, 1, 0).Item1;

            if (x1 != X_BOUND && x1 - xMax > 1)
            {
                //Possible hallway found!
                validHallways.Add(new int[] { xMax + 1, x1 - 1, y, y, theIndex, myGrid[y + Y_BOUND, x1 + X_BOUND] - 1 });
            }
        }


        return validHallways;
    }

    /// <summary>
    /// This method is meant to interface with GetValidHallways(...) returned list of int[].
    /// Preforms checks, and will fail to draw the hallway if any of these checks are failed.
    /// </summary>
    /// <param name="theRoom"> An int[] from the list returned from GetValidHallways(...). </param>
    private void DrawHallway(in int[] theRoom)
    {
        //Check if the room connection was already made
        if (myAdjacencyList[theRoom[4]].myConnectedRooms.Contains(myAdjacencyList[theRoom[5]].myRoom))
        {
            return;
        }

        //Make sure that the hallway isn't too short.
        if (theRoom[0] == theRoom[1] && theRoom[2] == theRoom[3])
        {
            return;
        }

        //Make sure that there is enough room for the hallway
        if (!CheckGridSpace(theRoom[0], theRoom[1], theRoom[2], theRoom[3]))
        {
            return;
        }

        //Make sure that the hallway isn't directly adjacent to an existing room
        if ((theRoom[0] == theRoom[1] && !CheckGridSpace(theRoom[0] - 1, theRoom[1] + 1, theRoom[2], theRoom[3]))
            || (theRoom[2] == theRoom[3] && !CheckGridSpace(theRoom[0], theRoom[1], theRoom[2] - 1, theRoom[3] + 1)))
        {
            return;
        }

        //Make sure that the hallway isn't too long
        if (theRoom[1] - theRoom[0] > Y_BOUND || theRoom[3] - theRoom[2] > Y_BOUND)
        {
            return;
        }


        //Make sure that, if this isn't the first hallway being drawn, that we aren't trying to draw a hallway into a hallway
        if (myAdjacencyList[theRoom[4]].myAdjacentRooms.Count > 2 && (myAdjacencyList[theRoom[5]].myRoom.GetW() == 1.0f || myAdjacencyList[theRoom[5]].myRoom.GetH() == 1.0f))
        {
            return;
        }


        //We begin to create the DungeonRoom instance inside the adjacency list./
        myAdjacencyList.Add(new GraphNode(new DungeonRoom(theRoom[0], theRoom[1], theRoom[2], theRoom[3])));
        myAdjacencyList[myAdjacencyList.Count - 1].myRoom.SetID(myAdjacencyList.Count - 1);

        //We now initialize the graph connections
        myAdjacencyList[theRoom[4]].myAdjacentRooms.Add(myAdjacencyList[myAdjacencyList.Count - 1]);
        myAdjacencyList[theRoom[5]].myAdjacentRooms.Add(myAdjacencyList[myAdjacencyList.Count - 1]);
        myAdjacencyList[myAdjacencyList.Count - 1].myAdjacentRooms.Add(myAdjacencyList[theRoom[4]]);
        myAdjacencyList[myAdjacencyList.Count - 1].myAdjacentRooms.Add(myAdjacencyList[theRoom[5]]);
        myAdjacencyList[theRoom[4]].myConnectedRooms.Add(myAdjacencyList[theRoom[5]].myRoom);
        myAdjacencyList[theRoom[5]].myConnectedRooms.Add(myAdjacencyList[theRoom[4]].myRoom);

        //We 'stamp' the hallway to the grid matrix so that hallways in the future can connect to it
        if (theRoom[0] == theRoom[1])
        {
            //Hallway spans the y-axis
            for (int y = theRoom[2]; y <= theRoom[3]; y++)
            {
                myGrid[y + Y_BOUND, theRoom[0] + X_BOUND] = myAdjacencyList.Count;
            }
        }
        else
        {
            //Hallway spans the x-axis
            for (int x = theRoom[0]; x <= theRoom[1]; x++)
            {
                myGrid[theRoom[2] + Y_BOUND, x + X_BOUND] = myAdjacencyList.Count;
            }
        }
    }

    /// <summary>
    /// Given a starting point and a direction, this method attempts to draw a hallway and returns the ending point of the hallway
    /// </summary>
    /// <param name="theX"> The starting x-coordinate. </param>
    /// <param name="theY"> The starting y-coordinate. </param>
    /// <param name="theDeltaX"> This value can either be -1, 0, or 1. </param>
    /// <param name="theDeltaY"> This value can either be -1, 0, or 1. </param>
    /// <returns> The ending point of the hallway. </returns>
    private (int, int) PathFindHallway(in int theX, in int theY, in int theDeltaX, in int theDeltaY)
    {
        //Makes sure at least one of these values are 0, and at least one is -1 or 1
        if (!(theDeltaX == 0 ^ theDeltaY == 0) || !(theDeltaX == -1 ^ theDeltaY == -1 || theDeltaX == 1 ^ theDeltaY == 1))
        {
            return (X_BOUND * theDeltaX, Y_BOUND * theDeltaY);
        }

        int x = theX, y = theY;

        //Iterate through the matrix as if you were drawing a straight line
        while (x >= -X_BOUND && x < X_BOUND && y >= -Y_BOUND && y < Y_BOUND && myGrid[y + Y_BOUND, x + X_BOUND] == 0)
        {
            x += theDeltaX;
            y += theDeltaY;
        }

        return (x, y);
    }

    /// <summary>
    /// A simple recursive traversal helper method that sets the seen flag of each room to help specify which rooms are reachable from the specified initial room.
    /// </summary>
    /// <param name="theCurrentRoom"></param>
    /// <returns> The number of rooms that are traversable, not including the first room. </returns>
    private int traverse(in int theCurrentRoom)
    {
        myAdjacencyList[theCurrentRoom].myRoom.SetSeenFlag(true);
        int cnt = 0;
        foreach (GraphNode node in myAdjacencyList[theCurrentRoom].myAdjacentRooms)
        {
            if (!node.myRoom.GetSeenFlag())
            {
                cnt += traverse(node.myRoom.GetID());
                cnt++;
            }
        }

        return cnt;
    }

    /// <summary>
    /// This helper method traverses through the graph and removes any rooms that cannot be traversed to.
    /// </summary>
    /// <returns> The number of rooms that were deleted. </returns>
    private int RemoveIsolatedRooms()
    {
        //We want to make sure that the first room we traverse from isn't an isolated room
        int firstRoom = 0;
        while (traverse(firstRoom) < 50)
        {
            firstRoom++;

            foreach (GraphNode node in myAdjacencyList)
            {
                node.myRoom.SetSeenFlag(false);
            }
        }

        //Remove any rooms that were not seen during the traversal
        int count = 0;
        for (int i = 0; i < myAdjacencyList.Count; i++)
        {
            if (!myAdjacencyList[i].myRoom.GetSeenFlag())
            {
                myAdjacencyList.RemoveAt(i--);
                count++;
            }
        }

        //Reset the ID and the seen flag of each room
        for (int i = 0; i < myAdjacencyList.Count; i++)
        {
            myAdjacencyList[i].myRoom.SetID(i);
            myAdjacencyList[i].myRoom.SetSeenFlag(false);
        }

        return count;
    }

}
