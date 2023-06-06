using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

/// <summary>
/// A testing class that tests the DungeonMap class.
/// </summary>
public class DungeonMapTests {
    [Test]
    /// <summary>
    /// We test to see if the generated map is traversable; That is, are a reasonable number of rooms reachable if we begin traversing from the first room?
    /// </summary>
    public void TraversalTest() {
        DungeonMap map;

        //We must test a large number of different maps since they are randomly generated, and certain cases may not show for most random dungeons.
        for (int n = 0; n < 250; n++) {
            map = new DungeonMap();
            HashSet<int> visitedRooms = new HashSet<int>();

            traverse(map, visitedRooms, 0);

            Assert.AreEqual(map.GetRoomCount(), visitedRooms.Count);
        }
    }

    [Test]
    /// <summary>
    /// We want to make sure that each dungeon we generate has at least 50 rooms.
    /// </summary>
    public void VolumeTest() {
        DungeonMap map;

        //We must test a large number of different maps since they are randomly generated, and a certain case may not show for most random dungeons.
        for (int n = 0; n < 250; n++) {
            map = new DungeonMap();
            
            Assert.IsTrue(map.GetRoomCount() >= 50, $"{map.GetRoomCount()}");
        }
    }

    /// <summary>
    /// A helper method used to recursively traverse a dungeon map.
    /// </summary>
    /// <param name="theMap"> A reference to the dungeon map you would like to traverse. </param>
    /// <param name="theVisitedRooms"> A set of int to store visited rooms. </param>
    /// <param name="theCurrentRoom"> The index of the starting room. </param>
    private void traverse(in DungeonMap theMap, in HashSet<int> theVisitedRooms, in int theCurrentRoom) {
        theVisitedRooms.Add(theCurrentRoom);
        
        //We must focus the room so we can access its adjacent rooms
        theMap.FocusNthRoom(theCurrentRoom);

        for (int i = 0; theMap.GetNthAdjacentRoom(i) != null; i++) {
            int adjRoom = theMap.GetNthAdjacentRoom(i).GetID();

            if (!theVisitedRooms.Contains(adjRoom)) {
                traverse(theMap, theVisitedRooms, adjRoom);
                theMap.FocusNthRoom(theCurrentRoom);
            }
        }
    }
}