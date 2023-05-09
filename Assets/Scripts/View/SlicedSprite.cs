using UnityEngine;

/// <summary>
/// An instanceable unity button. By default, the button turns blue when hovering over, turns green when left-clicking, and turns red when right-clicking.
/// </summary>
public class SlicedSprite {
    /// <summary>
    /// The underlying GameObject of the button.
    /// </summary>
    private GameObject myObject;

    /// <summary>
    /// The Lone Constructor to the class.
    /// </summary>
    /// <param name="theName"> The Name of the GameObject. </param>
    /// <param name="theSprite"> The initial Sprite of the button. The Sprite must be 9-sliced. </param>
    /// <param name="thePosition"> The Position of the button, centered on the button. </param>
    /// <param name="theSize"> The size of the button. </param>
    public SlicedSprite(string theName, Sprite theSprite, Vector3 thePosition, Vector2 theSize) {
        myObject = new GameObject(
            theName,
            typeof(SpriteRenderer),
            typeof(BoxCollider2D)
        );

        (myObject.GetComponent<SpriteRenderer>()).drawMode = SpriteDrawMode.Sliced;
        myObject.transform.localScale = new Vector3(1, 1, 0);

        SetSprite(theSprite);
        SetSize(theSize);
        SetPosition(thePosition);
    }

    /// <summary>
    /// A mutator for the size of the button.
    /// </summary>
    /// <param name="theSize"> The new size of the button. </param>
    public void SetSize(Vector2 theSize) {
        (myObject.GetComponent<BoxCollider2D>()).size = theSize;
        (myObject.GetComponent<SpriteRenderer>()).size = theSize;
    }

    /// <summary>
    /// A mutator for the position of the button.
    /// </summary>
    /// <param name="thePosition"> The new Position of the button. </param>
    public void SetPosition(Vector3 thePosition) {
        myObject.transform.localPosition = thePosition;
    }

    /// <summary>
    /// An accessor for the position of the button.
    /// </summary>
    /// <returns> The position of the button. </returns>
    public Vector3 GetPosition() {
        return myObject.transform.localPosition;
    }

    /// <summary>
    /// A mutator for the Sprite of the button. 
    /// </summary>
    /// <param name="theSprite"> The new Sprite of the button. The Sprite must be 9-sliced. </param>
    public void SetSprite(Sprite theSprite) {
        (myObject.GetComponent<SpriteRenderer>()).sprite = theSprite;
    }

    /// <summary>
    /// A mutator for the color of the button.
    /// </summary>
    /// <param name="theColor"> The new color of the button. </param>
    public void SetColor(Color theColor) {
        (myObject.GetComponent<SpriteRenderer>()).color = theColor;
    }

    /// <summary>
    /// Lets you add a Monobehaviour script to the button for added functionality.
    /// </summary>
    /// <param name="theComponent"> The type of the MonoBehavior script. </param>
    public void AddComponent(System.Type theComponent) {
        myObject.AddComponent(theComponent);
    }
}
