using System.Collections.Generic;
using System;

class DungeonRoom {
    
    //Fields for the location and size of the room
    private int myX;
    private int myY;
    private int myW;
    private int myH;

    //The loot that was generated with the room
    private Stack<Object> myLoot;

    //The enemy party within the room
    private Object myEnemyParty;

    public DungeonRoom(int theX, int theY, int theW, int theH) {
        myX = theX;
        myY = theY;
        myW = theW;
        myH = theH;
    }

    //Accessors for the room bounds
    public int GetX() { 
        return myX; 
    } 

    public int GetY() { 
        return myY; 
    }

    public int GetW() { 
        return myW; 
    }

    public int GetH() { 
        return myH; 
    }

    //An accessor for the top of the Loot Stack. Pops the top of the stack once per method call, until none is left in the stack.
    public Object GetLoot() {
        if (myLoot == null || myLoot.Count == 0) {
            return null;
        }

        return myLoot.Pop();
    }


}