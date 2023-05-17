using System.IO;
using UnityEngine;
class loadButton : fileButton
{
    public override void PressButton()
    {   
        string path = Application.persistentDataPath + "/test.txt";
        StreamReader reader = new StreamReader(path);
        string data = reader.ReadToEnd();
        reader.Close();
        fileChangeRequest.Raise(this, new DataPacket(data, "LoadRequest", "Button Factory"));   
    }
}
