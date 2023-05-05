using System.Collections.Generic;

namespace DefaultNamespace
{

    internal abstract class AbstractPlayerCharacter : AbstractActor
    {

        /// <summary>
        /// The current experience of the party member.
        /// When specific thresholds are reached, the party member levels up.
        /// Represented as a double underneath, but should be shown as a floored integer.
        /// </summary>
        private double experience;

        // probably want to change this to a map or enum
        //private List<Equipment> Gear;

        internal AbstractPlayerCharacter(string theName, double theHitpoints, double theAttack,
     double theDefence, double theMana, int theInitiative) : base(theName, theHitpoints, theAttack,
     theDefence, theMana, theInitiative)
        {
            experience = 0.0;
            //Gear = new List<Equipment>();
        }

        //private void UseItem(Item theItem)
        //{
        // code for using an item
        //}

        //private Action ChooseAction(int theChoice)
        //{
        //    return Action = (Actions)theChoice;
        //}

    }
}