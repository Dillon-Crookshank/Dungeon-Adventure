namespace DefaultNamespace;

internal abstract class AbstractPlayerCharacter : AbstractActor
{

    private int experience;

    // probably want to change this to a map or enum
    private List<Equipment> Gear;

    private void UseItem(Item theItem)
    {
        // code for using an item
    }

    private Action ChooseAction()
    {
        // Action code here
    }

}