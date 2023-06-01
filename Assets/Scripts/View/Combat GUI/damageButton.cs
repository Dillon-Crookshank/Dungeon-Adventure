using System.IO;
using UnityEngine;

/// <summary>
/// An representation of a GUI button that handles file saving.
/// </summary>
namespace DefaultNamespace
{
    class damageButton : clickableButton
    {

        /// <summary>
        /// Requests a health change of -1 on all heroes of the party.
        /// </summary>
        public override void PressButton()
        {
            onButtonClick.Raise(this, new DataPacket("-1", "DamageAmount"));
        }
    }
}
