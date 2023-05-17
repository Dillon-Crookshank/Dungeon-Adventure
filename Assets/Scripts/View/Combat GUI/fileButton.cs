using UnityEngine;
using DefaultNamespace;
using System.Collections.Generic;
abstract class fileButton : MonoBehaviour
{
    Dictionary<int, AbstractActor> party;
    public TextAsset file;
    private SpriteRenderer rend;
    public GameEvent fileChangeRequest;

    void Start(){ 
        rend = gameObject.GetComponent<SpriteRenderer>();
        Debug.Log(rend != null);
    }

    void OnMouseOver(){
        rend.color = Color.green;
        if (Input.GetMouseButtonDown(0)){
            PressButton();
        }
    }

    void OnMouseExit(){
        rend.color = Color.white;
    }

    public void ReceiveDataPacket(Component sender, object data)
    {
        
    }

    public abstract void PressButton();

}
