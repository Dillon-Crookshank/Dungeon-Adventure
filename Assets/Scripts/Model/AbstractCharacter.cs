namespace DefaultNamespace
{

    /// An abstract representation of an Character in the Dungeon Adventure.
    /// All enemies and player characters inherit from this class, and this defines
    /// basic behaviours and fields that are universal.
    internal abstract class AbstractCharacter
    {

        private string _name;

        /// <summary>
        /// A String representation of the Character.
        /// </summary>
        internal string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private double _maxHitpoints;

        /// <summary>
        /// The maximum hitpoints of the Character.
        /// This determines the max health of the Character, they can't be healed above this point.
        /// Is represented as a double but should be shown as an integer (using a ceiling).
        /// </summary>
        internal double maxHitpoints
        {
            get { return _maxHitpoints; }
            set
            {
                _maxHitpoints += value;
                if (value > 0) { currentHitpoints = value; }
                if (_maxHitpoints < currentHitpoints) { currentHitpoints = _maxHitpoints - currentHitpoints; }
            }
        }

        private double _currentHitpoints;

        /// <summary>
        /// The current hitpoints of the Character.
        /// This determines the health of the Character, if it reaches 0, the Character dies.
        /// Is represented as a double but should be shown as an integer (using a ceiling).
        /// </summary>
        internal double currentHitpoints
        {
            get { return _currentHitpoints; }
            set
            {
                _currentHitpoints += value;
                if (_currentHitpoints > maxHitpoints) { _currentHitpoints = maxHitpoints; }
                else if (_currentHitpoints < 0) { _currentHitpoints = 0; }
            }

        }

        /// <summary>
        /// The attack of the Character.
        /// This determines how much damage an Character deals with their attacks.
        /// Is represented as a double but should be shown as an integer (using a ceiling).
        /// </summary>
        private double _attack;

        internal double attack
        {
            get { return _attack; }
            set
            {
                _attack += value;
                if (_attack < 0) { _attack = 0; }
            }
        }

        private double _defence;

        /// <summary>
        /// The defence of the Character.
        /// This determines how much damage is blocked when the Character is attacked.
        /// Is represented as a double but should be shown as an integer (using a ceiling).
        /// </summary>
        internal double defence
        {
            get { return _defence; }
            set
            {
                _defence += value;
                if (_defence < 0) { _defence = 0; }
            }
        }

        private double _maxMana;

        /// <summary>
        /// The maximum mana of the Character.
        /// This is used by the Character to perform special actions and regenerates turn-to-turn.
        /// Is represented as a double but should be shown as an integer (using a ceiling).
        /// </summary>
        internal double maxMana
        {
            get { return _maxMana; }
            set
            {
                _maxMana += value;
                if (value > 0) { currentMana = value; }
                if (_maxMana < currentMana) { currentMana = (_maxMana - currentMana); }
                if (_maxMana < 0) { _maxMana = 0; }
            }
        }

        private double _currentMana;

        /// <summary>
        /// The current mana of the Character. 
        /// Attacks that use mana must have at least a current mana value of the cost to perform the attack.
        /// Is represented as a double but should be shown as an integer (using a floor).
        /// </summary>
        internal double currentMana
        {
            get { return _currentMana; }
            set
            {
                _currentMana += value;
                if (_currentMana < 0) { _currentMana = 0; }
                else if (maxMana < _currentMana) { _currentMana = maxMana; }
            }
        }

        private int _intiative;

        /// <summary>
        /// The base initiative of the Character.
        /// This base value has an additional D20 roll added to it to determine turn order.
        /// </summary>
        internal int initiative
        {
            get { return _intiative; }
            set { _intiative += value; }
        }

        private int _combatInitiative;

        /// <summary>
        /// The initiative of the Character for this combat iteration.
        /// Base initiative + D20.
        /// This value is rerolled for each combat.
        /// </summary>
        internal int combatInitiative
        {
            get { return _combatInitiative; }
            set { _combatInitiative += value; }
        }

        private int _partyPosition;

        /// <summary>
        /// The position this Character holds in the party.
        /// </summary>
        internal int partyPosition
        {
            get { return _partyPosition; }
            set { if (!(value < 1) && !(value > AbstractParty.MAX_PARTY_SIZE)) { _partyPosition = value; } }
        }


        /// <summary>
        /// Base constructor for an Abstract Character, player characters and enemies both inherit
        /// this constructor for their initialization.
        /// </summary>
        /// <param name="theName">The name of the Character.</param>
        /// <param name="theHitpoints">The maximum hitpoints of the Character.</param>
        /// <param name="theAttack">The attack of the Character.</param>
        /// <param name="theDefence">The defence of the Character.</param>
        /// <param name="theMana">The maximum mana of the Character.</param>
        /// <param name="theInitiative">The initiative of the Character.</param>
        internal AbstractCharacter(in string theName, in double theHitpoints, in double theAttack,
         in double theDefence, in double theMana, in int theInitiative)
        {
            Name = theName;
            maxHitpoints = theHitpoints;
            attack = theAttack;
            defence = theDefence;
            maxMana = theMana;
            initiative = theInitiative;
            combatInitiative = 0;
        }


        /// <summary>
        /// Whether this character is currently alive or not.
        /// </summary>
        /// <returns>True if alive, false if dead.</returns>
        internal bool IsAlive()
        {
            return currentHitpoints > 0;
        }

        /// <summary>
        /// A string representation of the current status of the Character.
        /// </summary>
        /// <returns>A string representation of the current status of the Character.</returns>
        public override string ToString()
        {
            return "Information about this Character:" + "\nPosition: " + partyPosition +
            "\nName: " + Name + "\nAttack: " + attack
            + "\nHitpoints: " + currentHitpoints + "/" + maxHitpoints + "\nDefence: " + defence
            + "\nMana: " + currentMana + "/" + maxMana + "\nInitiative: " + initiative + " + " +
            (combatInitiative - initiative) + "\nAlive?: " + IsAlive();
        }

    }

}
