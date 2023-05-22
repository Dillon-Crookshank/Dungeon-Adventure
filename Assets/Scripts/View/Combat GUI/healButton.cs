using System.IO;
using UnityEngine;

/// <summary>
/// An representation of a GUI button that handles file saving.
/// </summary>
namespace DefaultNamespace {
    class healButton : clickableButton
    {
        /// <summary>
        /// Requests a saved state of the party from the button factory.
        /// </summary>
        public override void PressButton()
        {
            onButtonClick.Raise(this, new DataPacket(null, "DamageAmount", "1"));
        }
    }
}
