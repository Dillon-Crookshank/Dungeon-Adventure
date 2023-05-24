using System.Collections.Generic;
using System;

/// <summary>
/// This class represents a dungeon room, with fields such as the bounds of the room, or loot within the room.
/// </summary>
[Serializable]
public class DungeonRoom
{   
    /// <summary>
    /// The cached hashcode of the room.
    /// </summary>
    private int myHash;

    /// <summary>
    /// The unique ID number of the room.
    /// </summary>
    private int myID;

    /// <summary>
    /// The x-coordinate of the room.
    /// </summary>
    private float myX;

    /// <summary>
    /// The y-coordinate of the room.
    /// </summary>
    private float myY;

    /// <summary>
    /// The width of the room.
    /// </summary>
    private float myW;

    /// <summary>
    /// The height of the room.
    /// </summary>
    private float myH;
    
    /// <summary>
    /// Only used when initialized using the grid constraints method. This holds the minimum X grid constraint.
    /// </summary>
    private int myGridXMin;

    /// <summary>
    /// Only used when initialized using the grid constraints method. This holds the maximum X grid constraint.
    /// </summary>
    private int myGridXMax;

    /// <summary>
    /// Only used when initialized using the grid constraints method. This holds the minimum Y grid constraint.
    /// </summary>
    private int myGridYMin;

    /// <summary>
    /// Only used when initialized using the grid constraints method. This holds the maximum Y grid constraint.
    /// </summary>
    private int myGridYMax;

    /// <summary>
    /// Used to determine if the room has been 'seen' by the user party.
    /// </summary>
    private bool mySeenFlag;

    /// <summary>
    /// **Unimplemented**
    /// </summary>
    private Stack<Object> myLoot;

    /// <summary>
    /// **Unimplemented**
    /// </summary>
    private Object myEnemyParty;

    /// <summary>
    /// Creates a DungeonRoom Given an integer domain that is snapped to a grid. 
    /// </summary>
    /// <param name="theXMin"> The Lowest X-value of the DungeonRoom. </param>
    /// <param name="theXMax"> The Highest X-value of the DungeonRoom. </param>
    /// <param name="theYMin"> The Lowest Y-value of the DungeonRoom. </param>
    /// <param name="theYMax"> The Highest Y-value of the DungeonRoom. </param>
    public DungeonRoom(int theXMin, int theXMax, int theYMin, int theYMax) {
        
        //Calculate the game unit coordinates based on the given grid domain
        myX = (theXMin + theXMax) / 2.0f + 0.5f;
        myY = (theYMin + theYMax) / 2.0f + 0.5f;
        myW = Math.Abs(theXMin - theXMax) + 1.0f;
        myH = Math.Abs(theYMin - theYMax) + 1.0f;


        myGridXMin = theXMin;
        myGridXMax = theXMax;
        myGridYMin = theYMin;
        myGridYMax = theYMax;
     }

    /// <summary>
    /// An accessor for the room ID number.
    /// </summary>
    /// <returns> The ID number of the room. </returns>
    public int GetID() {
        return myID;
    }

    /// <summary>
    /// A mutator for the room ID number.
    /// </summary>
    /// <returns> The ID number of the room. </returns>
    public void SetID(int theID) {
        myID = theID;
    }

    /// <summary>
    /// An accessor for the x-coordinate of the room.
    /// </summary>
    /// <returns> The x-coordinate of the room. </returns>
    public float GetX()
    {
        return myX;
    }

    /// <summary>
    /// An accessor for the y-coordinate of the room. 
    /// </summary>
    /// <returns> The y-coordinate of the room. </returns>
    public float GetY()
    {
        return myY;
    }

    /// <summary>
    /// An accessor for the width of the room
    /// </summary>
    /// <returns> The width of the room. </returns>
    public float GetW()
    {
        return myW;
    }

    /// <summary>
    /// An accessor for the height of the room.
    /// </summary>
    /// <returns> The height of the room. </returns>
    public float GetH()
    {
        return myH;
    }

    /// <summary>
    /// Only used when initialized using the grid constraints method. Returns the minimum X grid constraint.
    /// </summary>
    /// <returns> Returns the minimum X grid constraint. </returns>
    public int GetXMin() {
        return myGridXMin;
    }

    /// <summary>
    /// Only used when initialized using the grid constraints method. Returns the maximum X grid constraint.
    /// </summary>
    /// <returns> Returns the maximum X grid constraint. </returns>
    public int GetXMax() {
        return myGridXMax;
    }

    /// <summary>
    /// Only used when initialized using the grid constraints method. Returns the minimum Y grid constraint.
    /// </summary>
    /// <returns> Returns the minimum Y grid constraint. </returns>
    public int GetYMin() {
        return myGridYMin;
    }

    /// <summary>
    /// Only used when initialized using the grid constraints method. Returns the maximum Y grid constraint.
    /// </summary>
    /// <returns> Returns the maximum Y grid constraint. </returns>
    public int GetYMax() {
        return myGridYMax;
    }

    /// <summary>
    /// An accessor for the seen flag. True if the user party has seen the room. To see a room, you must be at least adjacent to the room.
    /// </summary>
    /// <returns> True if the user has 'seen' the dungeon room. </returns>
    public bool GetSeenFlag() {
        return mySeenFlag;
    }

    /// <summary>
    /// A mutator for the seen flag. True if the user party has seen the room. To see a room, you must be at least adjacent to the room.
    /// </summary>
    public void SetSeenFlag(bool theSeenFlag) {
        mySeenFlag = theSeenFlag;
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
    /// A predicate function to check if the room still contains uncollected loot.
    /// </summary>
    /// <returns> True if the room still contains loot. False otherwise. </returns>
    public bool HasLoot()
    {
        return myLoot.Count != 0;
    }

    /// <summary>
    /// Returns a reference to the stored enemy party.
    /// </summary>
    /// <returns> A reference to the enemy party that is stored inside the dungeon room. </returns>
    public Object GetEnemyParty() {
        return myEnemyParty;
    }
}
