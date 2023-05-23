namespace DefaultNamespace
{

    /// An abstract representation of an Actor in the Dungeon Adventure.
    /// All enemies and player characters inherit from this class, and this defines
    /// basic behaviours and fields that are universal.
    internal abstract class AbstractActor
    {
        /// <summary>
        /// A String representation of the Actor.
        /// </summary>
        private string name;

        /// <summary>
        /// The maximum hitpoints of the Actor.
        /// This determines the max health of the Actor, they can't be healed above this point.
        /// Is represented as a double but should be shown as an integer (using a ceiling).
        /// </summary>
        private double maxHitpoints;

        /// <summary>
        /// The current hitpoints of the Actor.
        /// This determines the health of the Actor, if it reaches 0, the Actor dies.
        /// Is represented as a double but should be shown as an integer (using a ceiling).
        /// </summary>
        private double currentHitpoints;

        /// <summary>
        /// The attack of the Actor.
        /// This determines how much damage an Actor deals with their attacks.
        /// Is represented as a double but should be shown as an integer (using a ceiling).
        /// </summary>
        private double attack;

        /// <summary>
        /// The defence of the Actor.
        /// This determines how much damage is blocked when the Actor is attacked.
        /// Is represented as a double but should be shown as an integer (using a ceiling).
        /// </summary>
        private double defence;

        /// <summary>
        /// The maximum mana of the Actor.
        /// This is used by the Actor to perform special actions and regenerates turn-to-turn.
        /// Is represented as a double but should be shown as an integer (using a ceiling).
        /// </summary>
        private double maxMana;

        /// <summary>
        /// The current mana of the Actor. 
        /// Attacks that use mana must have at least a current mana value of the cost to perform the attack.
        /// Is represented as a double but should be shown as an integer (using a floor).
        /// </summary>
        private double currentMana;

        /// <summary>
        /// The base initiative of the Actor.
        /// This base value has an additional D20 roll added to it to determine turn order.
        /// </summary>
        private int initiative;

        /// <summary>
        /// The initiative of the Actor for this combat iteration.
        /// Base initiative + D20.
        /// This value is rerolled for each combat.
        /// </summary>
        private int combatInitiative;

        /// <summary>
        /// The position this Actor holds in the party.
        /// </summary>
        private int partyPosition;


        /// <summary>
        /// Base constructor for an Abstract Actor, player characters and enemies both inherit
        /// this constructor for their initialization.
        /// </summary>
        /// <param name="theName">The name of the Actor.</param>
        /// <param name="theHitpoints">The maximum hitpoints of the Actor.</param>
        /// <param name="theAttack">The attack of the Actor.</param>
        /// <param name="theDefence">The defence of the Actor.</param>
        /// <param name="theMana">The maximum mana of the Actor.</param>
        /// <param name="theInitiative">The initiative of the Actor.</param>
        internal AbstractActor(in string theName, in double theHitpoints, in double theAttack,
         in double theDefence, in double theMana, in int theInitiative)
        {
            SetName(theName);
            SetMaxHitpoints(theHitpoints);
            SetAttack(theAttack);
            SetDefence(theDefence);
            SetMaxMana(theMana);
            SetInitiative(theInitiative);
            SetCombatInitiative(0);
        }

        /// <summary>
        /// Getter for the name of the Actor.
        /// </summary>
        /// <returns>The current name of the Actor.</returns>
        internal string GetName()
        {
            return name;
        }


        /// <summary>
        /// Getter for the maxHitpoints stat.
        /// </summary>
        /// <returns>The current maxHitpoints of the Actor.</returns>
        internal double GetMaxHitpoints()
        {
            return maxHitpoints;
        }


        /// <summary>
        /// Getter for the current hitpoints of the Actor.
        /// </summary>
        /// <returns></returns>
        internal double GetCurrentHitpoints()
        {
            return currentHitpoints;
        }


        /// <summary>
        /// Getter for the attack stat.
        /// </summary>
        /// <returns>The current attack of the Actor.</returns>
        internal double GetAttack()
        {
            return attack;
        }


        /// <summary>
        /// Getter for the defence stat.
        /// </summary>
        /// <returns>The current defence of the Actor.</returns>
        internal double GetDefence()
        {
            return defence;
        }


        /// <summary>
        /// Getter for the maxMana of the Actor.
        /// </summary>
        /// <returns>The current maxMana of the Actor.</returns>
        internal double GetMaxMana()
        {
            return maxMana;
        }


        /// <summary>
        /// Getter for the current Mana of the Actor.
        /// </summary>
        /// <returns>The current Mana of the Actor.</returns>
        internal double GetCurrentMana()
        {
            return currentMana;
        }


        /// <summary>
        /// Getter for the initiative of the Actor.
        /// </summary>
        /// <returns>The base initiative of the Actor.</returns>
        internal int GetInitiative()
        {
            return initiative;
        }

        /// <summary>
        /// Getter for the combatInitiative of the Actor.
        /// </summary>
        /// <returns>The initiative for this specific combat after the D20 roll.</returns>
        internal int GetCombatInitiative()
        {
            return combatInitiative;
        }


        /// <summary>
        /// Getter for the Actor's party position.
        /// </summary>
        /// <returns>The party position of the Actor.</returns>
        internal int GetPartyPosition()
        {
            return partyPosition;
        }


        /// <summary>
        /// Getter for whether the Actor is currently alive or dead.
        /// </summary>
        /// <returns>True if alive, false if dead.</returns>
        internal bool IsAlive()
        {
            return currentHitpoints > 0;
        }


        /// <summary>
        /// Setter for the name of the Actor.
        /// </summary>
        /// <param name="theName">The name the Actor is to be set to.</param>
        /// <returns>True if the name is successfully set, false otherwise.</returns>
        internal bool SetName(string theName)
        {
            name = theName;
            return true;
        }


        /// <summary>
        /// Setter for the maxHitpoints of an Actor.
        /// If maxHitpoints are increased, currentHitpoints are increased by the same amount.
        /// If maxHitpoints drops below currentHitpoints, currentHitpoints are reduced to match maxHitpoints' value.
        /// </summary>
        /// <param name="theChange">Positive for an increase in maxHitpoints, negative for a decrease.</param>
        internal void SetMaxHitpoints(double theChange)
        {
            maxHitpoints += theChange;
            if (theChange > 0)
            {
                SetCurrentHitpoints(theChange);
            }
            if (maxHitpoints < currentHitpoints)
            {
                SetCurrentHitpoints(maxHitpoints - currentHitpoints);
            }
        }


        /// <summary>
        /// Setter for the currentHitpoints of the Actor.
        /// A positive change represents healing, and a negative change represents damage taken.
        /// If an Actor reaches 0 currentHitpoints, the Actor dies.
        /// </summary>
        /// <param name="theChange">Positive for healing, negative for damage.</param>
        internal void SetCurrentHitpoints(double theChange)
        {
            currentHitpoints += theChange;
            if (currentHitpoints > maxHitpoints){
                currentHitpoints = maxHitpoints;
            } else if (currentHitpoints < 0){
                currentHitpoints = 0;
            }
        }


        /// <summary>
        /// Setter for the attack of the Actor.
        /// A positive value represents an increase, and a negative represents a decrease.
        /// If reduced below 0, defence is set to 0.
        /// </summary>
        /// <param name="theChange">Positive for an increase in attack, negative for a decrease.</param>
        internal void SetAttack(double theChange)
        {
            attack += theChange;
            if (attack < 0)
            {
                attack = 0;
            }
        }


        /// <summary>
        /// Setter for the defence of the Actor.
        /// A positive value represents an increase, and a negative represents a decrease.
        /// If reduced below 0, defence is set to 0.
        /// </summary>
        /// <param name="theChange">Positive for an increase in defence, negative for a decrease.</param>
        internal void SetDefence(double theChange)
        {
            defence += theChange;
            if (defence < 0)
            {
                defence = 0;
            }
        }


        /// <summary>
        /// Setter for the maxMana of the Actor.
        /// A positive value represents an increase, and a negative represents a decrease.
        /// If reduced below 0, maxMana is set to 0.
        /// </summary>
        /// <param name="theChange">Positive for an increase in maxMana, negative for a decrease.</param>
        internal void SetMaxMana(double theChange)
        {
            maxMana += theChange;
            if (theChange > 0)
            {
                SetCurrentMana(theChange);
            }
            if (maxMana < currentMana)
            {
                SetCurrentMana(maxMana - currentMana);
            }
            if (maxMana < 0)
            {
                maxMana = 0;
            }
        }


        /// <summary>
        /// Setter for the currentMana of the Actor. 
        /// A positive value represents an increase, and a negative represents a decrease.
        /// If reduced below 0, currentMana is set to 0.
        /// </summary>
        /// <param name="theChange">Positive for an increase in currentMana, negative for a decrease.</param>
        internal void SetCurrentMana(double theChange)
        {
            currentMana += theChange;
            if (currentMana < 0)
            {
                currentMana = 0;
            }
            else if (maxMana < currentMana)
            {
                currentMana = maxMana;
            }
        }


        /// <summary>
        /// Setter for the initiative of the Actor.
        /// A positive value represents an increase, and a negative represents a decrease.
        /// If reduced below 0, initiative is set to 0.
        /// </summary>
        /// <param name="theChange">Positive for an increase in initiative, negative for a decrease.</param>
        internal void SetInitiative(int theChange)
        {
            initiative += theChange;
        }

        /// <summary>
        /// Setter for the combat initiative of the Actor.
        /// A positive value represents an increase, and a negative represents a decrease.
        /// If reduced below 0, initiative is set to 0.
        /// </summary>
        /// <param name="theChange">Positive for an increase in initiative, negative for a decrease.</param>
        internal void SetCombatInitiative(int theChange)
        {
            combatInitiative += theChange;
        }

        /// <summary>
        /// Updates the locally stored partyPosition after a move has occurred.
        /// </summary>
        /// <param name="thePosition">The position the Actor is attempting to move to.</param>
        /// <returns>True if the move was successful, false otherwise.</returns>
        internal bool SetPartyPosition(int thePosition)
        {
            if (thePosition < 1 || thePosition > AbstractParty.MAX_PARTY_SIZE)
            {
                return false;
            }
            partyPosition = thePosition;
            return true;
        }


        /// <summary>
        /// A string representation of the current status of the Actor.
        /// </summary>
        /// <returns>A string representation of the current status of the Actor.</returns>
        public override string ToString()
        {
            return "Information about this actor:" + "\nPosition: " + partyPosition +
            "\nName: " + name + "\nAttack: " + attack
            + "\nHitpoints: " + currentHitpoints + "/" + maxHitpoints + "\nDefence: " + defence
            + "\nMana: " + currentMana + "/" + maxMana + "\nInitiative: " + initiative + " + " +
            (combatInitiative - initiative) + "\nAlive?: " + IsAlive();
        }


    }

}
