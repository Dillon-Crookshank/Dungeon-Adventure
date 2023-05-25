using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// An representation of a GUI button that handles file loading.
/// </summary>
namespace DefaultNamespace
{
    class loadButton : clickableButton
    {
        /// <summary>
        /// Handles sending a request to the Button Factory to load the party.
        /// </summary>
        public override void PressButton()
        {
            // string path = Application.persistentDataPath + "/test.txt";
            // StreamReader reader = new StreamReader(path);
            // string data = reader.ReadToEnd();
            // reader.Close();
            onButtonClick.Raise(this, new DataPacket(DeserializeParty("testParty.bin"), "LoadRequest", "Button Factory"));
        }

        private PlayerParty DeserializeParty(string theFileName) {

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(theFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            
            //Save the map to a temp variable so we can close the file stream
            PlayerParty tempParty = (PlayerParty) formatter.Deserialize(stream);
            stream.Close();

            Debug.Log("Map Deserialized!");

            return tempParty;
        }

        
    }
}
