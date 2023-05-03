using UnityEngine;

public class UButton {
    private GameObject myButton;

    public UButton(string theName, Sprite theSprite, Vector3 thePosition, Vector3 theScale) {
        myButton = new GameObject(theName, typeof(SpriteRenderer), typeof(BoxCollider2D), typeof(ButtonMouseListener));

        (myButton.GetComponent<SpriteRenderer>()).sprite = theSprite;

        SetScale(theScale);
        SetPosition(thePosition);

        (myButton.GetComponent<BoxCollider2D>()).size = new Vector2(1, 1);
    }

    public void SetScale(Vector3 theScale) {
        myButton.transform.localScale = theScale;
    }

    public void SetPosition(Vector3 thePosition) {
        myButton.transform.localPosition = thePosition;
    }
}

class ButtonMouseListener : MonoBehaviour {
    public void Start() {}
    
    public void OnMouseOver() {
        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1)) {
            ChangeColor(Color.blue);
        }
        
        if (Input.GetMouseButtonDown(0)) {
            FireListener((this.name, EventType.LeftMousePressed));
            ChangeColor(Color.green);
        } 
        
        else if (Input.GetMouseButtonUp(0)) {
            FireListener((this.name, EventType.LeftMouseReleased));
            ChangeColor(Color.blue);
        }

        if (Input.GetMouseButtonDown(1)) {
            FireListener((this.name, EventType.RightMousePressed));
            ChangeColor(Color.red);
        } 
        
        else if (Input.GetMouseButtonUp(1)) {
            FireListener((this.name, EventType.RightMouseReleased));
            ChangeColor(Color.blue);
        }
    }

    private void ChangeColor(Color theColor) {
        (this.gameObject.GetComponent<SpriteRenderer>()).color = theColor;
    }

    private void FireListener((string, EventType) theEvent) {
        (GameObject.FindWithTag("GameController")).SendMessage("EventListener", theEvent);
    }

    public void OnMouseExit() {
        ChangeColor(Color.white);
    }
}