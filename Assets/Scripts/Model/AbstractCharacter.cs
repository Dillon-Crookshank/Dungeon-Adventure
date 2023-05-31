using System;

namespace DefaultNamespace
{

    /// An abstract representation of an Character in the Dungeon Adventure.
    /// All enemies and player characters inherit from this class, and this defines
    /// basic behaviours and fields that are universal.
    [Serializable]
    internal abstract class AbstractCharacter
    {

        private string _characterClass;

        internal string CharacterClass
        {
            get { return _characterClass; }
            set { _characterClass = value; }
        }

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
        internal double MaxHitpoints
        {
            get { return _maxHitpoints; }
            set
            {
                _maxHitpoints += value;
                if (value > 0) { CurrentHitpoints = value; }
                if (_maxHitpoints < CurrentHitpoints) { CurrentHitpoints = _maxHitpoints - CurrentHitpoints; }
            }
        }

        private double _currentHitpoints;

        /// <summary>
        /// The current hitpoints of the Character.
        /// This determines the health of the Character, if it reaches 0, the Character dies.
        /// Is represented as a double but should be shown as an integer (using a ceiling).
        /// </summary>
        internal double CurrentHitpoints
        {
            get { return _currentHitpoints; }
            set
            {
                _currentHitpoints += value;
                if (_currentHitpoints > MaxHitpoints) { _currentHitpoints = MaxHitpoints; }
                else if (_currentHitpoints < 0) { _currentHitpoints = 0; }
            }

        }

        /// <summary>
        /// The attack of the Character.
        /// This determines how much damage an Character deals with their attacks.
        /// Is represented as a double but should be shown as an integer (using a ceiling).
        /// </summary>
        private double _attack;

        internal double Attack
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
        internal double Defence
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
        internal double MaxMana
        {
            get { return _maxMana; }
            set
            {
                _maxMana += value;
                if (value > 0) { CurrentMana = value; }
                if (_maxMana < CurrentMana) { CurrentMana = (_maxMana - CurrentMana); }
                if (_maxMana < 0) { _maxMana = 0; }
            }
        }

        private double _currentMana;

        /// <summary>
        /// The current mana of the Character. 
        /// Attacks that use mana must have at least a current mana value of the cost to perform the attack.
        /// Is represented as a double but should be shown as an integer (using a floor).
        /// </summary>
        internal double CurrentMana
        {
            get { return _currentMana; }
            set
            {
                _currentMana += value;
                if (_currentMana < 0) { _currentMana = 0; }
                else if (MaxMana < _currentMana) { _currentMana = MaxMana; }
            }
        }

        private int _intiative;

        /// <summary>
        /// The base initiative of the Character.
        /// This base value has an additional D20 roll added to it to determine turn order.
        /// </summary>
        internal int Initiative
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
        internal int CombatInitiative
        {
            get { return _combatInitiative; }
            set { _combatInitiative += value; }
        }

        private int _partyPosition;

        /// <summary>
        /// The position this Character holds in the party.
        /// </summary>
        internal int PartyPosition
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
        internal AbstractCharacter(in string theClass, in double theHitpoints, in double theAttack,
         in double theDefence, in double theMana, in int theInitiative)
        {
            CharacterClass = theClass;
            Name = CharacterClass;
            MaxHitpoints = theHitpoints;
            Attack = theAttack;
            Defence = theDefence;
            MaxMana = theMana;
            Initiative = theInitiative;
            CombatInitiative = 0;
        }


        /// <summary>
        /// Whether this character is currently alive or not.
        /// </summary>
        /// <returns>True if alive, false if dead.</returns>
        internal bool IsAlive()
        {
            return CurrentHitpoints > 0;
        }

        /// <summary>
        /// A basic attack that simply takes deals the attack value - theTargets 
        /// defence value as damage, with a minimum of 1.0.
        /// </summary>
        /// <param name="theTarget">The <see cref"AbstractCharacter"/> being targeted with this attack.</param>
        internal bool BasicAttack(AbstractCharacter theTarget)
        {
            double theDamage = (Math.Max(1, Attack - theTarget.Defence));
            theTarget.CurrentHitpoints = (-1 * theDamage);
            return true;
        }

        internal int Defend()
        {
            Defence *= 2;
            return 1;
        }

        internal int Buff()
        {
            string statModified = accessDB.BuffDatabaseStatModified(CharacterClass);

            switch (statModified)
            {
                case "attack":
                    if (CurrentMana < accessDB.BuffDatabaseManaCost(CharacterClass))
                    {
                        return 0;
                    }
                    Attack = accessDB.BuffDatabasePercentage(CharacterClass) * Attack;
                    CurrentMana = (-accessDB.BuffDatabaseManaCost(CharacterClass));
                    return accessDB.BuffDatabaseDuration(CharacterClass);
                case "defence":
                    if (CurrentMana < accessDB.BuffDatabaseManaCost(CharacterClass))
                    {
                        return 0;
                    }
                    Defence = accessDB.BuffDatabasePercentage(CharacterClass) * Defence;
                    CurrentMana = (-accessDB.BuffDatabaseManaCost(CharacterClass));
                    return accessDB.BuffDatabaseDuration(CharacterClass);
            }


            return accessDB.BuffDatabaseDuration(CharacterClass);
        }

        /// <summary>
        /// A string representation of the current status of the Character.
        /// </summary>
        /// <returns>A string representation of the current status of the Character.</returns>
        public override string ToString()
        {
            return "Information about this Character:" + "\nPosition: " + PartyPosition +
            "\nName: " + Name + "\nClass: " + CharacterClass + "\nAttack: " + Attack
            + "\nHitpoints: " + CurrentHitpoints + "/" + MaxHitpoints + "\nDefence: " + Defence
            + "\nMana: " + CurrentMana + "/" + MaxMana + "\nInitiative: " + Initiative + " + " +
            (CombatInitiative - Initiative) + "\nAlive?: " + IsAlive();
        }

    }
}
