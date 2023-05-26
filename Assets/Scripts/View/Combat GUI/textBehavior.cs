using UnityEngine;

sealed class textBehavior : MonoBehaviour
{
    public TextMesh textMesh;

    public void ReceiveDataPacket(Component sender, object data)
    {
        DataPacket dPacket = (DataPacket)data;
        if (dPacket.GetLabel() == "NewTextString")
        {
            textMesh.text = ((string)dPacket.GetData());
        }
    }
}
