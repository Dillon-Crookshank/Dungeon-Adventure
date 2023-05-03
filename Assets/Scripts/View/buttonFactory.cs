using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonFactory : MonoBehaviour
{
    public GameObject generateSprite;

    [SerializeField]
    private testButton scriptReference = null;
    [SerializeField]
    private Sprite spriteReference = null;
    private GameObject[] arrayOfObjects = new GameObject[10];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < arrayOfObjects.Length; i++)
        {
            arrayOfObjects[i] = new GameObject(
                ("Button" + (i + 1)),
                typeof(BoxCollider2D),
                typeof(SpriteRenderer),
                typeof(testButton),
                typeof(TextMesh)
            );
            arrayOfObjects[i].GetComponent<SpriteRenderer>().sprite = spriteReference;
            float xPos = i - (arrayOfObjects.Length / 2);
            arrayOfObjects[i].transform.Translate(xPos, 0.0f, 0.0f);
        }
    }

    // Update is called once per frame
    void Update() { }
}
