namespace DefaultNamespace;


internal abstract class AbstractParty
{

    private bool isAllAlive;

    private enum partyOrder
    {
        party1,
        party2,
        party3,
        party4,
        party5,
        party6
    }

    private List<AbstractActor> partyComposition;

    private int[2, 3] partyGrid;

    private bool IsAllAlive()
    {
        return isAllAlive;
    }

}