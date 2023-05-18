using System.IO;
using UnityEngine;

/// <summary>
/// An representation of a GUI button that handles file loading.
/// </summary>
class loadButton : fileButton
{
    /// <summary>
    /// Handles sending a request to the Button Factory to load the party.
    /// </summary>
    public override void PressButton()
    {   
        string path = Application.persistentDataPath + "/test.txt";
        StreamReader reader = new StreamReader(path);
        string data = reader.ReadToEnd();
        reader.Close();
        fileChangeRequest.Raise(this, new DataPacket(data, "LoadRequest", "Button Factory"));   
    }
}
