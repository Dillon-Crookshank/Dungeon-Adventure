using System.Collections.Generic;

namespace DefaultNamespace
{

    internal abstract class AbstractActor
    {
        /// <summary>
        /// A String representation of the Actor.
        /// </summary>
        internal string name;

        /// <summary>
        /// The current hitpoints of the Actor.
        /// This determines the health of the Actor, if it reaches 0, the Actor dies.
        /// Is represented as a double but should be shown as an integer (using a ceiling).
        /// </summary>
        internal double currentHitpoints;

        /// <summary>
        /// The maximum hitpoints of the Actor.
        /// This determines the max health of the Actor, they can't be healed above this point.
        /// Is represented as a double but should be shown as an integer (using a ceiling).
        /// </summary>
        internal double maxHitpoints;

        /// <summary>
        /// The attack of the Actor.
        /// This determines how much damage an Actor deals with their attacks.
        /// Is represented as a double but should be shown as an integer (using a ceiling).
        /// </summary>
        internal double attack;

        /// <summary>
        /// The defence of the Actor.
        /// This determines how much damage is blocked when the Actor is attacked.
        /// Is represented as a double but should be shown as an integer (using a ceiling).
        /// </summary>
        internal double defence;

        /// <summary>
        /// The mana of the Actor.
        /// This is used by the Actor to perform special actions and regenerates turn-to-turn.
        /// Is represented as a double but should be shown as an integer (using a ceiling).
        /// </summary>
        internal double mana;

        /// <summary>
        /// The initiative of the Actor.
        /// This primarily determines how early in the turn order that the Actor will be placed.
        /// </summary>
        internal int initiative;

        /// <summary>
        /// A boolean that represents whether the Actor is currently alive.
        /// </summary>
        internal bool isAlive;

        // may want to refactor this to map specific changes to values rather than a list
        //private List<Buff> Buffs;

        /// <summary>
        /// An enumeration of the possible actions an Actor can take.
        /// </summary>
        internal enum Actions
        {
            Action1,
            Action2,
            Action3,
            Action4,
            Action5,
            Action6
        }

        internal AbstractActor(string theName, double theHitpoints, double theAttack,
         double theDefence, double theMana, int theInitiative)
        {

            // can probably simplify this when we move it to the database
            name = theName;
            maxHitpoints = theHitpoints;
            currentHitpoints = maxHitpoints;
            attack = theAttack;
            defence = theDefence;
            mana = theMana;
            initiative = theInitiative;
            isAlive = true;
            //Buffs = new List<Buff>();
        }

        // need to work out exactly the data flow for this
        internal void Action(int theChoice)
        {

        }


        /// <summary>
        /// Getter for whether the Actor is currently alive or dead.
        /// </summary>
        /// <returns>True if alive, false if dead.</returns>
        internal bool IsAlive()
        {
            return isAlive;
        }


        /// <summary>
        /// Getter for the current hitpoints of the Actor.
        /// </summary>
        /// <returns></returns>
        internal double getCurrentHitpoints()
        {
            return currentHitpoints;
        }

        // TODO this method
        internal void setCurrentHitpoints(double theChange)
        {

        }

        internal double getDefence()
        {
            return defence;
        }

        internal double getmaxHitpoints()
        {
            return maxHitpoints;
        }
    }
}
