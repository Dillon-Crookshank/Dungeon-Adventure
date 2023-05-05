using System.Collections.Generic;
using DefaultNamespace;

/// <summary>
/// An abstract class representing a party of AbstractActors.
/// </summary>
internal abstract class AbstractParty
{

    /// <summary>
    /// True if at least one party member is alive, false if all are killed.
    /// </summary>
    private bool isAllAlive;

    /// <summary>
    /// An enumeration to track the order of the party members.
    /// </summary>
    private enum partyOrder
    {
        partyMember1,
        partyMember2,
        partyMember3,
        partyMember4,
        partyMember5,
        partyMember6
    }

    /// <summary>
    /// A List that contains the AbstractActors that make up the party.
    /// </summary>
    private List<AbstractActor> partyComposition;

    /// <summary>
    /// A 2D Array that represents the 'layout' of the party.
    /// </summary>
    private int[] partyGrid;

    /// <summary>
    /// Getter method for whether the party is defeated or not.
    /// </summary>
    /// <returns> True if party is not defeated, false if all party members are dead.</returns>
    private bool IsAllAlive()
    {
        return isAllAlive;
    }

}