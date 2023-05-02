using System.Collections.Generic;

internal class DungeonRoom {
    
    //Fields for the location and size of the room
    private int myX;
    private int myY;
    private int myW;
    private int myH;

    //The loot that was generated with the room
    private Stack<Loot> myLoot;

    //The enemy party within the room
    private EnemyParty myEnemyParty;

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
    public Loot GetLoot() {
        if (myLoot == null || myLoot.Count == 0) {
            return null;
        }

        return myLoot.Pop();
    }


}

//Stubed out classes
private class Loot {}
private class EnemyParty {}