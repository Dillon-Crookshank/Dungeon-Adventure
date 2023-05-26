using System;
using System.Collections.Generic;

using DefaultNamespace;

/// <summary>
/// An abstract class representing a party of AbstractActors.
/// </summary>
[Serializable]
internal abstract class AbstractParty
{

    /// <summary>
    /// The maximum party size allowed.
    /// </summary>
    public const int MAX_PARTY_SIZE = 6;

    /// <summary>
    /// True if at least one party member is alive, false if all are killed.
    /// </summary>
    internal bool isAllAlive;


    /// <summary>
    /// A Dictionary that contains the AbstractActors that make up the party, and their positions.
    /// </summary>
    internal Dictionary<int, AbstractCharacter> partyPositions;


    /// <summary>
    /// Getter method for whether the party is defeated or not.
    /// </summary>
    /// <returns> True if party is not defeated, false if all party members are dead.</returns>
    private bool IsAllAlive()
    {
        return isAllAlive;
    }

    /// <summary>
    /// Adds an Actor into the party.
    /// Party size must be positive, and max party size is <see cref=MAX_PARTY_SIZE>
    /// </summary>
    /// <param name="theCharacter">The Actor to be added to the party.</param>
    /// <returns>True if added successfully, false otherwise.</returns>
    internal bool AddCharacter(AbstractCharacter theCharacter)
    {
        int key = 0;
        for (int i = 1; i <= MAX_PARTY_SIZE; i++)
        {
            if (!partyPositions.ContainsKey(i))
            {
                key = i;
                break;
            }
        }
        if (key == 0)
        {
            return false;
        }
        partyPositions.Add(key, theCharacter);
        theCharacter.PartyPosition = key;
        return true;
    }

    /// <summary>
    /// Getter for the partyPositions Dictionary. 
    /// </summary>
    /// <returns>The AbstractActors in the party, mapped by the positions.</returns>
    internal Dictionary<int, AbstractCharacter> GetPartyPositions()
    {
        return partyPositions;
    }

    /// <summary>
    /// Moves an Actor into an open position in the party.
    /// </summary>
    /// <param name="thePosition">The party position the Actor is attempting to move to.</param>
    /// <param name="theCharacter">The Actor attempting to move.</param>
    /// <returns>True if move is successful, false otherwise.</returns>
    internal bool moveCharacter(int thePosition, AbstractCharacter theCharacter)
    {
        if (thePosition < 1 || thePosition > 6 || partyPositions.ContainsKey(thePosition))
        {
            return false;
        }
        partyPositions.Remove(theCharacter.PartyPosition);
        theCharacter.PartyPosition = thePosition;
        partyPositions.Add(thePosition, theCharacter);
        return true;
    }


}