using System;

namespace DungeonAdventure
{

    /// <summary>
    /// A class for storing data related to an AbstractCharacter Buff.
    /// </summary>
    [Serializable]
    internal class Buff
    {

        private string _statModifiedByBuff;

        /// <summary>
        /// The stat to be modified by the Buff.
        /// </summary>
        /// <value>A string value of the stat to be modified.</value>
        internal string StatModifiedByBuff
        {
            get { return _statModifiedByBuff; }
            set { if (value == "attack" || value == "defence") { _statModifiedByBuff = value; } }
        }

        private int _buffDuration;

        /// <summary>
        /// The duration the Buff is to last.
        /// </summary>
        /// <value>The duration of the Buff.</value>
        internal int BuffDuration
        {
            get { return _buffDuration; }
            set { if (value > 0) { _buffDuration = value; } }
        }

        private double _buffPercentage;

        /// <summary>
        /// The percentage the stat is modified by the Buff.
        /// </summary>
        /// <value>The percentage change by the Buff to the modified stat.</value>
        internal double BuffPercentage
        {
            get { return _buffPercentage; }
            set { _buffPercentage = value; }
        }

        private int _buffManaCost;

        /// <summary>
        /// The mana cost for the Buff to be used.
        /// </summary>
        /// <value>The mana cost of the Buff.</value>
        internal int BuffManaCost
        {
            get { return _buffManaCost; }
            set { if (value > 0) { _buffManaCost = value; } }
        }

        private string _buffName;

        /// <summary>
        /// The name of the Buff.
        /// </summary>
        /// <value>The name used to represent the Buff.</value>
        internal string BuffName
        {
            get { return _buffName; }
            set { _buffName = value; }
        }

        /// <summary>
        /// Constructor for the Buff object.
        /// </summary>
        /// <param name="theName">The Name of the Buff.</param>
        /// <param name="theDuration">The Duration of the Buff.</param>
        /// <param name="thePercentage">The Percentage change the Buff provides.</param>
        /// <param name="theStatModified">The Stat modified by the Buff.</param>
        /// <param name="theManaCost">The mana cost of the Buff.</param>
        internal Buff(in string theName, in int theDuration, in double thePercentage, in string theStatModified, in int theManaCost)
        {
            BuffName = theName;
            BuffDuration = theDuration;
            BuffPercentage = thePercentage;
            StatModifiedByBuff = theStatModified;
            BuffManaCost = theManaCost;
        }

    }
}