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
    private bool myRightDown;
    private bool myLeftDown;

    public void Start() {
        myRightDown = false;
        myLeftDown = false;
    }
    
    public void OnMouseOver() {
        ChangeColor(Color.blue);

        if (Input.GetMouseButton(0)) {
            if (!myLeftDown) {
                FireListener((this.name, EventType.LeftMousePressed));
            }

            myLeftDown = true;
            ChangeColor(Color.green);
        }

        else if (myLeftDown) {
            myLeftDown = false;
            ChangeColor(Color.blue);
            FireListener((this.name, EventType.LeftMouseReleased));
        }

        if (Input.GetMouseButton(1)) {
            if (!myRightDown) {
                FireListener((this.name, EventType.RightMousePressed));
            }

            myRightDown = true;
            ChangeColor(Color.red);
        } 

        else if (myRightDown) {
            myRightDown = false;
            ChangeColor(Color.blue);
            FireListener((this.name, EventType.RightMouseReleased));
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
        
        myRightDown = false;
        myLeftDown = false;
    }
}