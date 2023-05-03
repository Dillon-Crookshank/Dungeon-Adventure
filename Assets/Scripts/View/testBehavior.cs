using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBehavior : MonoBehaviour
{
    bool focused = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDrag(){
        if (focused){
            gameObject.transform.Translate(0.01f, 0.01f, 0.0f);
        }
    }
    void OnMouseOver(){
        focused = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.8f, 0.0f);
    }
    void OnMouseExit(){
        focused = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
    }

}
