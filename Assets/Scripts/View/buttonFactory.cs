using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class buttonFactory : MonoBehaviour
{

    public GameObject templateSprite;
    public GameObject backgroundBasis;
    public float GridCenterOnBackgroundPercentage = 0.75f;
    private GameObject[] arrayOfObjects;

    public string[] buttonLabels;
    RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        int numObjects = buttonLabels.Length;
        Vector3 colliderSize = templateSprite.GetComponent<BoxCollider2D>().bounds.size;
        Vector3 scaleSize = new Vector3(
            3 * templateSprite.transform.localScale.x / (numObjects * 1.5f),
            3 * templateSprite.transform.localScale.y / (numObjects * 1.5f),
            2 * templateSprite.transform.localScale.z / (numObjects * 1.5f)
        );
        arrayOfObjects = new GameObject[numObjects];
        Vector3[] positionVectors = returnPositionValues(numObjects, colliderSize.x * scaleSize.x, colliderSize.y * scaleSize.y);
        // Vector3[] positionArray = returnPositionValues();
        for (int i = 0; i < numObjects; i++)
        {
            arrayOfObjects[i] = Instantiate(templateSprite, transform.position, transform.rotation);
            arrayOfObjects[i].transform.localScale = scaleSize;
            arrayOfObjects[i].name = buttonLabels[i];
            
            arrayOfObjects[i].transform.position = (positionVectors[i]);
            Debug.LogFormat("{0}: {1}, {2}", i, positionVectors[i].x, positionVectors[i].y);

            float xPos = colliderSize.x * scaleSize.x + colliderSize.x / 20;
            // if (numObjects > 3){
            //     Debug.Log(Mathf.Floor(i + 1 % 2));
            //     Debug.Log((Mathf.Floor((i - 1) % 2)) * colliderSize.y * scaleSize.y);
            //     arrayOfObjects[i].transform.Translate(
            //         (xPos * (i % Mathf.Floor(numObjects / 2))) - (xPos * (Mathf.Floor((numObjects - 1) / 2)) / 2),
            //         -(Mathf.Floor((i - 1) % 2)) * colliderSize.y * scaleSize.y,
            //         0.0f
            //     );
            // } else {
            //     arrayOfObjects[i].transform.Translate(
            //         (xPos * i) - (xPos * (numObjects - 1) / 2),
            //         0.0f,
            //         0.0f
            //     );
            // }
        }
    }

    // Update is called once per frame
    void Update() { }
    
    private Vector3[] returnPositionValues(int numObjects, float width, float length){

        float backgroundCenterPoint = backgroundBasis.GetComponent<SpriteRenderer>().bounds.size.y;

        Debug.LogFormat("{0} {1}", width, length);
        Vector3[] returnSet = new Vector3[numObjects];
        Vector2 gapBorder = new Vector2(width / 20, length / 20);
        int breakPoint = Mathf.CeilToInt(arrayOfObjects.Length / 2);
        Vector2 centeringPoint = new Vector2((gapBorder.x + width), (gapBorder.y + length));
        if (breakPoint % 2 == 0){
            centeringPoint.x = (gapBorder.y + width) * (numObjects - 1) / 2;
        }
        if (breakPoint < numObjects){
            centeringPoint.y = (gapBorder.y + length) / 2;
        }
        Debug.LogFormat("{0} * {1} = {2}", backgroundCenterPoint, GridCenterOnBackgroundPercentage, backgroundCenterPoint * GridCenterOnBackgroundPercentage);
        for (int i = 0; i < breakPoint; i++){
            returnSet[i] = new Vector3(
                ((gapBorder.x + width) * i) - centeringPoint.x,
                centeringPoint.y - (backgroundCenterPoint * GridCenterOnBackgroundPercentage),
                0f
            );
        }
        for (int i = breakPoint; i < numObjects; i++){
            returnSet[i] = new Vector3(
                ((gapBorder.x + width) * (i - breakPoint)) - centeringPoint.x,
                -centeringPoint.y - (backgroundCenterPoint * GridCenterOnBackgroundPercentage),
                0f
            );
        }
        return returnSet;
    }


}
