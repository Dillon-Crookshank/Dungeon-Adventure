using System;

namespace DungeonAdventure
{

    [Serializable]
    internal class SpecialAttack
    {

        private string _statModifiedByBuff;

        internal string StatModifiedByBuff
        {
            get { return _statModifiedByBuff; }
            set { if (value == "attack" || value == "defence") { _statModifiedByBuff = value; } }
        }

        private int _debuffDuration;

        internal int DebuffDuration
        {
            get { return _debuffDuration; }
            set { if (value > 0) { _debuffDuration = value; } }
        }

        private double _debuffPercentage;

        internal double DebuffPercentage
        {
            get { return _debuffPercentage; }
            set { _debuffPercentage = value; }
        }

        private int _specialAttackManaCost;

        internal int SpecialAttackManaCost
        {
            get { return _specialAttackManaCost; }
            set { if (value > 0) { _specialAttackManaCost = value; } }
        }

        private string _specialAttackName;

        internal string SpecialAttackName
        {
            get { return _specialAttackName; }
            set { _specialAttackName = value; }
        }

        private double _baseDamage;

        internal double BaseDamage
        {
            get { return _baseDamage; }
            set { _baseDamage = value; }
        }

        internal SpecialAttack(in string theName, in int theDuration, in double thePercentage, in string theStatModified, in int theManaCost, in double theBaseDamage)
        {
            SpecialAttackName = theName;
            DebuffDuration = theDuration;
            DebuffPercentage = thePercentage;
            StatModifiedByBuff = theStatModified;
            SpecialAttackManaCost = theManaCost;
            BaseDamage = theBaseDamage;
        }

    }
}