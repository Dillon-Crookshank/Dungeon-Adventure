using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonFactory : MonoBehaviour
{
    public int numObjects = 3;
    public GameObject templateSprite;
    private GameObject[] arrayOfObjects;

    RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 colliderSize;
        Vector3 scaleSize = new Vector3(
            (numObjects - 1) * templateSprite.transform.localScale.x / (numObjects * 1.5f),
            (numObjects - 1) * templateSprite.transform.localScale.y / (numObjects * 1.5f),
            (numObjects - 1) * templateSprite.transform.localScale.z / (numObjects * 1.5f)
        );
        arrayOfObjects = new GameObject[numObjects];
        for (int i = 0; i < numObjects; i++)
        {
            arrayOfObjects[i] = Instantiate(templateSprite, transform.position, transform.rotation);
            arrayOfObjects[i].transform.localScale = scaleSize;
            arrayOfObjects[i].name = "Button" + (i + 1);
            colliderSize = arrayOfObjects[i].GetComponent<BoxCollider2D>().bounds.size;
            float xPos = colliderSize.x * scaleSize.x + colliderSize.x / 20;
            arrayOfObjects[i].transform.Translate(
                (xPos * i) - (xPos * (numObjects - 1) / 2),
                0.0f,
                0.0f
            );
        }
    }

    // Update is called once per frame
    void Update() { }
}
