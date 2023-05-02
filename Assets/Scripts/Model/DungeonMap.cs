using System.Collections.Generic;

internal class DungeonMap {
    //The base of the adjacency list
    private List<MapEntry> myAdjacentcyList;

    //Holds a reference to the room the party is currently inside
    private MapEntry myCurrentRoom;


    //The entry of the adjacency list
    private class MapEntry {
        DungeonRoom myRoom;
        List<DungeonRoom> myAdjacentRooms;
    }

    public DungeonMap() {
        //For now, creates a 
    }

    public DungeonRoom GetCurrentRoom() {
        return myCurrentRoom.myRoom;
    }

    public List<DungeonRoom> GetAdjacentRooms() {
        return myCurrentRoom.myAdjacentRooms;
    }

    public void SetCurrentRoom(DungeonRoom theRoom) {
        myCurrentRoom = theRoom;
    }
}