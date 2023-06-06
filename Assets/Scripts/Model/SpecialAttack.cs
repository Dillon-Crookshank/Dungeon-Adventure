using System;

namespace DungeonAdventure
{

    /// <summary>
    /// A class for storing Special Attack data.
    /// </summary>
    [Serializable]
    internal class SpecialAttack
    {


        private string _debuffedStat;

        /// <summary>
        /// The stat bebuffed by the Special Attack.
        /// </summary>
        internal string DebuffedStat
        {
            get { return _debuffedStat; }
            set { if (value == "attack" || value == "defence") { _debuffedStat = value; } }
        }

        private int _debuffDuration;

        /// <summary>
        /// The duration of the debuff inflicted by the Special Attack.
        /// </summary>
        internal int DebuffDuration
        {
            get { return _debuffDuration; }
            set { if (value > 0) { _debuffDuration = value; } }
        }

        private double _debuffPercentage;

        /// <summary>
        /// The percent modification of the debuff.
        /// </summary>
        internal double DebuffPercentage
        {
            get { return _debuffPercentage; }
            set { _debuffPercentage = value; }
        }

        private int _specialAttackManaCost;

        /// <summary>
        /// The mana cost of the Special Attack.
        /// </summary>
        internal int SpecialAttackManaCost
        {
            get { return _specialAttackManaCost; }
            set { if (value > 0) { _specialAttackManaCost = value; } }
        }

        private string _specialAttackName;

        /// <summary>
        /// The name of the Special Attack.
        /// </summary>
        internal string SpecialAttackName
        {
            get { return _specialAttackName; }
            set { _specialAttackName = value; }
        }

        private double _damageModifier;

        /// <summary>
        /// The percentage that the damage of the attack is modified by.
        /// </summary>
        internal double DamageModifier
        {
            get { return _damageModifier; }
            set { _damageModifier = value; }
        }

        /// <summary>
        /// Constructs a Special Attack object to be used by a Character.
        /// </summary>
        /// <param name="theName">The name of the attack.</param>
        /// <param name="theDuration">The duration of the debuff. 0 if no debuff.</param>
        /// <param name="thePercentage">The percentage modifier of the debuff.</param>
        /// <param name="theStatModified">The stat to be debuffed by the Special Attack.</param>
        /// <param name="theManaCost">The mana cost of the Special Attack.</param>
        /// <param name="theDamageModifier">The damage modifier of the Special Attack.</param>
        internal SpecialAttack(in string theName, in int theDuration, in double thePercentage, in string theStatModified, in int theManaCost, in double theDamageModifier)
        {
            SpecialAttackName = theName;
            DebuffDuration = theDuration;
            DebuffPercentage = thePercentage;
            DebuffedStat = theStatModified;
            SpecialAttackManaCost = theManaCost;
            DamageModifier = theDamageModifier;
        }

    }
}