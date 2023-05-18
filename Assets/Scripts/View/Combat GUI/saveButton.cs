using System.IO;
using UnityEngine;

/// <summary>
/// An representation of a GUI button that handles file saving.
/// </summary>
class saveButton : fileButton
{
    /// <summary>
    /// Requests a saved state of the party from the button factory.
    /// </summary>
    public override void PressButton()
    {
        fileChangeRequest.Raise(this, new DataPacket(null, "SaveRequest", "Button Factory"));
    }

    /// <summary>
    /// Handles writing the party to a file.
    /// </summary>
    /// <param name="sender"> The component that sent the DataPacket. </param>
    /// <param name="data"> The object (DataPacket) held. </param>
    public void HandleFile(Component sender, object data)
    {
        string path = Application.persistentDataPath + "/test.txt";
        Debug.Log(path);
        DataPacket dPacket = (DataPacket)data;
        if (sender.name == "Button Factory" && dPacket.GetLabel() == "PartyData")
        {
            StreamWriter writer = new StreamWriter(path, false);
            writer.Write(dPacket.GetData());
            writer.Close();
        }
    }
}
