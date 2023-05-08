using UnityEngine;
using System;

public class CameraController {

    /// <summary>
    /// 
    /// </summary>
    private const float ASPECT_X = 16.0f;

    /// <summary>
    /// 
    /// </summary>
    private const float ASPECT_Y = 9.0f;

    /// <summary>
    /// 
    /// </summary>
    private const float STEP_SIZE = 0.05f;

    /// <summary>
    /// 
    /// </summary>
    private string myCameraName;

    /// <summary>
    /// 
    /// </summary>
    private Vector3 myOrigin;

    /// <summary>
    /// 
    /// </summary>
    private float myMaxSize;

    /// <summary>
    /// 
    /// </summary>
    private float myMinSize;

    /// <summary>
    /// 
    /// </summary>
    private float myXBound;

    /// <summary>
    /// 
    /// </summary>
    private float myYBound;

    /// <summary>
    /// 
    /// </summary>
    private Vector3 myPrevMousePosition;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="theName"></param>
    /// <param name="theOrigin"> The point where the Axis Bounds are centered about. </param>
    /// <param name="theSizeBounds"> This is a (float, float) tuple, where the first entry is the minimum camera size, and the second entry is the maximum vcamera size, both in game units. </param>
    /// <param name="theAxisBounds"> This is a (float, float) tuple, where the first entry is the absolute maximum x-coordinate, and the second entry is the absolute maximum y-coordinate. </param>
    public CameraController(string theName, Vector3 theOrigin, (float, float) theSizeBounds, (float, float) theAxisBounds) {
        myCameraName = theName;

        myOrigin = theOrigin;

        myMinSize = theSizeBounds.Item1;
        myMaxSize = theSizeBounds.Item2;

        myXBound = theAxisBounds.Item1;
        myYBound = theAxisBounds.Item2;
    }

    /// <summary>
    /// 
    /// </summary>
    public void UpdateCamera() {
        
        DisplaceCameraSize(Input.mouseScrollDelta.y / 2);


        if (Input.GetMouseButton(0)) {
            Vector3 delta = (myPrevMousePosition - GetMouseInWorldSpace());
            DisplaceCameraPosition(new Vector2(delta.x, 0));
            DisplaceCameraPosition(new Vector2(0, delta.y));
        }

        if (Input.GetKeyDown(KeyCode.End) || Input.GetMouseButtonDown(2)) {
            ResetCamera();
        }

        if (Input.GetKey(KeyCode.PageUp)) {
            DisplaceCameraSize(STEP_SIZE);
        }

        if (Input.GetKey(KeyCode.PageDown)) {
            DisplaceCameraSize(-STEP_SIZE);
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            DisplaceCameraPosition(new Vector2(0, STEP_SIZE));
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            DisplaceCameraPosition(new Vector2(0, -STEP_SIZE));
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            DisplaceCameraPosition(new Vector2(-STEP_SIZE, 0));
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            DisplaceCameraPosition(new Vector2(STEP_SIZE, 0));
        }

        myPrevMousePosition = GetMouseInWorldSpace();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMouseInWorldSpace() {
        return (GetCameraObject().GetComponent<Camera>()).ScreenToWorldPoint(Input.mousePosition);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public GameObject GetCameraObject() {
        return GameObject.FindWithTag(myCameraName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCameraPosition() {
        return GetCameraObject().transform.localPosition;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="thePosition"></param>
    public void SetCameraPosition(Vector2 thePosition) {
        if (Math.Abs(thePosition.y - myOrigin.y) + GetCameraSize() > myYBound 
            || Math.Abs(thePosition.x - myOrigin.x) + (GetCameraSize() / ASPECT_Y * ASPECT_X) > myXBound) {
                return;
        }

        GetCameraObject().transform.localPosition = new Vector3(thePosition.x, thePosition.y, -1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="theDelta"></param>
    public void DisplaceCameraPosition(Vector2 theDelta) {
        SetCameraPosition(new Vector2(GetCameraPosition().x + theDelta.x, GetCameraPosition().y + theDelta.y));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float GetCameraSize() {
        return (GetCameraObject().GetComponent<Camera>()).orthographicSize;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="theSize"></param>
    public void SetCameraSize(float theSize) {
        if (theSize < myMinSize || theSize > myMaxSize) {
            return;
        } 

        if (Math.Abs(GetCameraPosition().y - myOrigin.y) + theSize > myYBound 
            || Math.Abs(GetCameraPosition().x - myOrigin.x) + (theSize / ASPECT_Y * ASPECT_X) > myXBound) {
                return;
        }

        (GetCameraObject().GetComponent<Camera>()).orthographicSize = theSize;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="theDelta"></param>
    public void DisplaceCameraSize(float theDelta) {
        SetCameraSize(GetCameraSize() + theDelta);
    }

    /// <summary>
    /// 
    /// </summary>
    public void ResetCamera() {
        SetCameraPosition(myOrigin);
        SetCameraSize(myMaxSize);
    }
}