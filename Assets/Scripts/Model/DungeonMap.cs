using System.Collections.Generic;

internal class DungeonMap {
    //The base of the adjacency list
    private List<MapEntry> myAdjacentcyList;

    //Holds a reference to the room the party is currently inside
    private MapEntry myCurrentRoom;


    //The entry of the adjacency list
    private class MapEntry {
        public MapEntry(DungeonRoom theRoom) {
            myRoom = theRoom;
            myAdjacentRooms = new List<MapEntry>();
        }

        public DungeonRoom myRoom;
        public List<MapEntry> myAdjacentRooms;
    }

    public DungeonMap() {
        //For now, creates an example map with 2 rooms and a hallway
        DungeonRoom room0 = new DungeonRoom(0, 0, 5, 5);
        DungeonRoom room1 = new DungeonRoom(2, 5, 1, 3);
        DungeonRoom room2 = new DungeonRoom(0, 8, 5, 5);

        myAdjacentcyList.Add(new MapEntry(room0));
        myAdjacentcyList.Add(new MapEntry(room1));
        myAdjacentcyList.Add(new MapEntry(room2));

        myAdjacentcyList[0].myAdjacentRooms.Add(myAdjacentcyList[1]);

        myAdjacentcyList[1].myAdjacentRooms.Add(myAdjacentcyList[0]);
        myAdjacentcyList[1].myAdjacentRooms.Add(myAdjacentcyList[2]);

        myAdjacentcyList[2].myAdjacentRooms.Add(myAdjacentcyList[1]);
    }

    public DungeonRoom GetCurrentRoom() {
        return myCurrentRoom.myRoom;
    }

    public DungeonRoom GetNthAdjacentRoom(int index) {
        if (index < 0 || index >= myCurrentRoom.myAdjacentRooms.Count) {
            return null;
        }

        return myCurrentRoom.myAdjacentRooms[index].myRoom;
    }

    public void MoveToNthAdjacentRoom(int index) {
        if (index < 0 || index >= myCurrentRoom.myAdjacentRooms.Count) {
            return;
        }

        myCurrentRoom = myCurrentRoom.myAdjacentRooms[index];
    }
}