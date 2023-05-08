using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class buttonFactory : MonoBehaviour
{
    public GameObject templateSprite;
    public GameObject backgroundBasis;
    public float GridCenterOnBackgroundPercentage = 0.75f;
    private GameObject[] arrayOfObjects;
    private static Vector3[] positionVectors;
    public string[] buttonLabels;

    RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        int numObjects = buttonLabels.Length;

        Vector3 colliderSize = templateSprite.GetComponent<BoxCollider2D>().bounds.size;
        Vector3 scaleSize = new Vector3(
            3 * templateSprite.transform.localScale.x / (numObjects * 1.25f),
            3 * templateSprite.transform.localScale.y / (numObjects * 1.25f),
            2 * templateSprite.transform.localScale.z / (numObjects * 1.5f)
        );
        arrayOfObjects = new GameObject[numObjects];
        positionVectors = returnPositionValues(
            numObjects,
            colliderSize.x * scaleSize.x,
            colliderSize.y * scaleSize.y
        );

        for (int i = 0; i < numObjects; i++)
        {
            arrayOfObjects[i] = Instantiate(templateSprite, transform.position, transform.rotation);
            arrayOfObjects[i].transform.localScale = scaleSize;
            arrayOfObjects[i].name = buttonLabels[i];
            arrayOfObjects[i].transform.position = (positionVectors[i]);
            Debug.Log(i + ": " + positionVectors[i]);
            
        }
        int randomIndex = UnityEngine.Random.Range(0, arrayOfObjects.Length);
        arrayOfObjects[randomIndex].GetComponent<testButton>().ToggleHasHero();
        Debug.LogFormat("{0}: {1}", randomIndex, arrayOfObjects[randomIndex].GetComponent<testButton>().GetSelectMoveMode());
    }

    private Vector3[] returnPositionValues(int numObjects, float width, float length)
    {
        float backgroundCenterPoint = backgroundBasis.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector3[] returnSet = new Vector3[numObjects];
        Vector2 gapBorder = new Vector2(width / 20, length / 20);
        int breakPoint = Mathf.CeilToInt(arrayOfObjects.Length / 2);
        Vector2 centeringPoint = new Vector2((gapBorder.x + width), (gapBorder.y + length));
        if (breakPoint % 2 == 0)
        {
            centeringPoint.x = (gapBorder.y + width) * (numObjects - 1) / numObjects;
        }
        if (breakPoint < numObjects)
        {
            centeringPoint.y = (gapBorder.y + length) / 2;
        }
        Debug.LogFormat(
            "{0} * {1} = {2}",
            backgroundCenterPoint,
            GridCenterOnBackgroundPercentage,
            backgroundCenterPoint * GridCenterOnBackgroundPercentage
        );
        for (int i = 0; i < breakPoint; i++)
        {
            returnSet[i] = new Vector3(
                ((gapBorder.x + width) * i) - centeringPoint.x,
                centeringPoint.y - (backgroundCenterPoint * GridCenterOnBackgroundPercentage),
                0f
            );
        }
        for (int i = breakPoint; i < numObjects; i++)
        {
            returnSet[i] = new Vector3(
                ((gapBorder.x + width) * (i - breakPoint)) - centeringPoint.x,
                -centeringPoint.y - (backgroundCenterPoint * GridCenterOnBackgroundPercentage),
                0f
            );
        }
        return returnSet;
    }

    public Vector3 getPositionVector(int index)
    {
        if (index > arrayOfObjects.Length)
        {
            throw new UnityException("Illegal position!");
        }
        else
        {
            return positionVectors[index];
        }
    }
}
