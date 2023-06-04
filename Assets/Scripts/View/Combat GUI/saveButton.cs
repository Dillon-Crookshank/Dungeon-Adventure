using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// An representation of a GUI button that handles file saving.
/// </summary>
namespace DungeonAdventure
{
    class saveButton : clickableButton
    {
        /// <summary>
        /// Requests a saved state of the party from the button factory.
        /// </summary>
        public override void PressButton()
        {
            onButtonClick.Raise(this, new DataPacket(null, "SaveRequest", "Button Factory"));
        }

        /// <summary>
        /// Handles writing the party to a file.
        /// </summary>
        /// <param name="sender"> The component that sent the DataPacket. </param>
        /// <param name="data"> The object (DataPacket) held. </param>
        public void HandleFile(Component sender, object data)
        {
            DataPacket dPacket = (DataPacket)data;
            SerializeParty("testParty.bin", (PlayerParty)dPacket.GetData());
        }

        private void SerializeParty(string theFileName, PlayerParty theParty)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(theFileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, theParty);
            stream.Close();

            Debug.Log("Party Serialized!");
        }
    }
}
