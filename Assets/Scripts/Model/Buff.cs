using System.Collections.Generic;
using System;

namespace DungeonAdventure
{

    [Serializable]
    internal class Buff
    {

        private string _statModifiedByBuff;

        internal string StatModifiedByBuff
        {
            get { return _statModifiedByBuff; }
            set { if (value == "attack" || value == "defence") { _statModifiedByBuff = value; } }
        }

        private int _buffDuration;

        internal int BuffDuration
        {
            get { return _buffDuration; }
            set { if (value > 0) { _buffDuration = value; } }
        }

        private double _buffPercentage;

        internal double BuffPercentage
        {
            get { return _buffPercentage; }
            set { _buffPercentage = value; }
        }

        private int _buffManaCost;

        internal int BuffManaCost
        {
            get { return _buffManaCost; }
            set { if (value > 0) { _buffManaCost = value; } }
        }

        private string _buffName;

        internal string BuffName
        {
            get { return _buffName; }
            set { _buffName = value; }
        }

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