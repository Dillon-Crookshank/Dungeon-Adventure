using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testButton : MonoBehaviour
{
    bool held = false;
    private SpriteRenderer rend;
    // Start is called before the first frame update
    
    
    
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseOver(){
        if (Input.GetMouseButtonDown(0)){
            Debug.Log(gameObject.name + " pressed");
            held = true;
        } else if (Input.GetMouseButtonUp(0)) {
            held = false;
        }

        if (held){
            rend.color = new Color(0.0f, 0.5f, 0.0f);
        } else {
            rend.color = Color.green;
        }

    }
    void OnMouseExit(){
        held = false;
        rend.color = Color.white;
    }
}
