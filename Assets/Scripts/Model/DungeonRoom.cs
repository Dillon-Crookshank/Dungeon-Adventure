using System.Collections.Generic;
using System;

/// <summary>
/// This class represents a dungeon room, with fields such as the bounds of the room, or loot within the room.
/// </summary>
[Serializable]
public class DungeonRoom
{

    /// <summary>
    /// The unique ID number of the room. This number usually directly correlates with the index of this room inside a collection.
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
    /// Used to determine if an enemy party exists in this room.
    /// </summary>
    private bool myEnemyFlag;

    /// <summary>
    /// Creates a DungeonRoom Given an integer domain that is snapped to a grid. 
    /// </summary>
    /// <param name="theXMin"> The Lowest X-value of the DungeonRoom. </param>
    /// <param name="theXMax"> The Highest X-value of the DungeonRoom. </param>
    /// <param name="theYMin"> The Lowest Y-value of the DungeonRoom. </param>
    /// <param name="theYMax"> The Highest Y-value of the DungeonRoom. </param>
    public DungeonRoom(in int theXMin, in int theXMax, in int theYMin, in int theYMax) {
        
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
    /// An accessor for the room ID number.  This number usually directly correlates with the index of this room inside a collection.
    /// </summary>
    /// <returns> The ID number of the room. </returns>
    public int GetID() {
        return myID;
    }

    /// <summary>
    /// A mutator for the room ID number.  This number usually directly correlates with the index of this room inside a collection.
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
    public void SetSeenFlag(in bool theSeenFlag) {
        mySeenFlag = theSeenFlag;
    }

    /// <summary>
    /// An accessor for the enemy flag.
    /// </summary>
    /// <returns> True if an enemy exists inside the room; false otherwise. </returns>
    public bool GetEnemyFlag() {
        return myEnemyFlag;
    }

    /// <summary>
    /// A mutator for the enemy flag.
    /// </summary>
    public void SetEnemyFlag(in bool theEnemyFlag) {
        myEnemyFlag = theEnemyFlag;
    }
}
