using System.Collections.Generic;
using System;

/// <summary>
/// This class represents a dungeon room, with fields such as the bounds of the room, or loot within the room.
/// </summary>
public class DungeonRoom
{   
    /// <summary>
    /// The cached hashcode of the room.
    /// </summary>
    private int myHash;

    /// <summary>
    /// The x-coordiante of the room.
    /// </summary>
    private int myX;

    /// <summary>
    /// The y-coordinate of the room.
    /// </summary>
    private int myY;

    /// <summary>
    /// The width of the room.
    /// </summary>
    private int myW;

    /// <summary>
    /// The height of the room.
    /// </summary>
    private int myH;

    /// <summary>
    /// **Unimplemented**
    /// </summary>
    private Stack<Object> myLoot;

    /// <summary>
    /// **Unimplemented**
    /// </summary>
    private Object myEnemyParty;

    /// <summary>
    /// Creates a new dungeon room with the given bounds.
    /// </summary>
    /// <param name="theX"> The x-coordiante of the room. </param>
    /// <param name="theY"> The y-coordinate of the room. </param>
    /// <param name="theW"> The width of the room. </param>
    /// <param name="theH"> The height of the room. </param>
    public DungeonRoom(int theX, int theY, int theW, int theH)
    {
        myX = theX;
        myY = theY;
        myW = theW;
        myH = theH;
    }

    /// <summary>
    /// An accessor for the x-coordinate of the room.
    /// </summary>
    /// <returns> The x-coordiante of the room. </returns>
    public int GetX()
    {
        return myX;
    }

    /// <summary>
    /// An accessor for the y-coordinate of the room. 
    /// </summary>
    /// <returns> The y-coordinate of the room. </returns>
    public int GetY()
    {
        return myY;
    }

    /// <summary>
    /// An accessor for the width of the room
    /// </summary>
    /// <returns> The width of the room. </returns>
    public int GetW()
    {
        return myW;
    }

    /// <summary>
    /// An accessor for the height of the room.
    /// </summary>
    /// <returns> The height of the room. </returns>
    public int GetH()
    {
        return myH;
    }

    /// <summary>
    /// An accessor for the hashcode of the room. The hashcode is generated based on the bounds of the room only.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        if (myHash == 0)
        {
            myHash = (myW ^ myX) + 31 * (myH ^ myY);
        }
        return myHash;
    }

    /// <summary>
    /// An accessor for the top of the Loot Stack. Pops the top of the stack once per method call, until none is left in the stack.
    /// </summary>
    /// <returns> The top of the loot stack. </returns>
    public Object PopLoot()
    {
        if (myLoot == null || myLoot.Count == 0)
        {
            return null;
        }

        return myLoot.Pop();
    }

    /// <summary>
    /// An accessor for the top of the Loot stack. Does not change the Loot stack.
    /// </summary>
    /// <returns> The top of the Loot stack. </returns>
    public Object PeekLoot()
    {
        return myLoot.Peek();
    }
}
