using UnityEngine;
using UnityEditor;
using System.IO;

class saveButton : fileButton
{
    private AbstractParty party;
    public override void PressButton()
    {
        fileChangeRequest.Raise(this, new DataPacket(null, "SaveRequest", "Button Factory"));
    }

    public void HandleFile(Component sender, object data){
        string path = Application.persistentDataPath + "/test.txt";
        Debug.Log(path);
        DataPacket dPacket = (DataPacket) data;
        if (sender.name == "Button Factory" && dPacket.GetLabel() == "PartyData"){
            StreamWriter writer = new StreamWriter(path, false);
            writer.Write(dPacket.GetData());
            writer.Close();
            
        }
    }
}
