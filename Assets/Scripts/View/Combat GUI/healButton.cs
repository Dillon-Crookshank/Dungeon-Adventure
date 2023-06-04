using System.IO;
using UnityEngine;

/// <summary>
/// An representation of a GUI button that handles file saving.
/// </summary>
namespace DungeonAdventure
{
    class healButton : clickableButton
    {

        /// <summary>
        /// Requests a health change of +3 on all heroes of the party.
        /// </summary>
        public override void PressButton()
        {
            onButtonClick.Raise(this, new DataPacket("3", "DamageAmount"));
        }
    }
}
