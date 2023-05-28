using UnityEngine;
using System;


/// <summary>
/// A Controller class that lets a user control a camera while keeping it within specified bounds.
/// </summary>
public class CameraController {

    /// <summary>
    /// A constant that represents how wide the viewport is.
    /// </summary>
    private const float ASPECT_X = 16.0f;

    /// <summary>
    /// A constant that represents how tall the viewport is.
    /// </summary>
    private const float ASPECT_Y = 9.0f;

    /// <summary>
    /// A step size constant used to displace values when the position or the size of the camera is changed.
    /// </summary>
    private const float STEP_SIZE = 0.05f;

    /// <summary>
    /// The Reference to the camera object.
    /// </summary>
    private GameObject myCamera;

    /// <summary>
    /// The Origin point that every action is centered about.
    /// </summary>
    private Vector3 myOrigin;

    /// <summary>
    /// The maximum half-height of the camera. See Camera.size in the Unity Docs for more info.
    /// </summary>
    private float myMaxSize;

    /// <summary>
    /// The minimum half-height of the camera. See Camera.size in the Unity Docs for more info.
    /// </summary>
    private float myMinSize;

    /// <summary>
    /// The absolute maximum x-coordinate of the camera, centered about the given Origin.
    /// </summary>
    private float myXBound;

    /// <summary>
    /// The absolute maximum y-coordinate of the camera, centered about the given Origin.
    /// </summary>
    private float myYBound;

    /// <summary>
    /// The previous mouse position. Sampled at the end of every frame.
    /// </summary>
    private Vector3 myPrevMousePosition;

    /// <summary>
    /// Creates a new camera controller. Does not create a Camera GameObject. You must create the camera in the Unity Editor first.
    /// </summary>
    /// <param name="theName"> The name of the camera. </param>
    /// <param name="theOrigin"> The point where the Axis Bounds are centered about. </param>
    /// <param name="theSizeBounds"> This is a (float, float) tuple, where the first entry is the minimum camera size, and the second entry is the maximum camera size, both in game units. </param>
    /// <param name="theAxisBounds"> This is a (float, float) tuple, where the first entry is the absolute maximum x-coordinate, and the second entry is the absolute maximum y-coordinate. </param>
    public CameraController(string theName, Vector3 theOrigin, (float, float) theSizeBounds, (float, float) theAxisBounds) 
        : this(GameObject.Find(theName), theOrigin, theSizeBounds, theAxisBounds) {}

    /// <summary>
    /// Creates a new camera controller. Does not create a Camera GameObject. You must create the camera in the Unity Editor first.
    /// </summary>
    /// <param name="theCamera"> The game object reference of the camera. </param>
    /// <param name="theOrigin"> The point where the Axis Bounds are centered about. </param>
    /// <param name="theSizeBounds"> This is a (float, float) tuple, where the first entry is the minimum camera size, and the second entry is the maximum camera size, both in game units. </param>
    /// <param name="theAxisBounds"> This is a (float, float) tuple, where the first entry is the absolute maximum x-coordinate, and the second entry is the absolute maximum y-coordinate. </param>
    public CameraController(GameObject theCamera, Vector3 theOrigin, (float, float) theSizeBounds, (float, float) theAxisBounds) {
        myCamera = theCamera;

        myOrigin = theOrigin;

        myMinSize = theSizeBounds.Item1;
        myMaxSize = theSizeBounds.Item2;

        myXBound = theAxisBounds.Item1;
        myYBound = theAxisBounds.Item2;

        ResetCamera();
    }

    /// <summary>
    /// Put inside GameController.Update(). Where input is checked and the camera is moved.
    /// </summary>
    public void UpdateCamera() {
        //Don't do anything if the camera isn't active
        if (!myCamera.activeSelf) {
            return;
        }
        
        DisplaceCameraSize(Input.mouseScrollDelta.y / -2);

        //If Left-Mouse is being held...
        if (Input.GetMouseButton(0)) {
            Vector3 delta = (myPrevMousePosition - GetMouseInWorldSpace());
            DisplaceCameraPosition(delta);
        }

        //If either the 'End' key was pressed, or the Middle-Mouse was pressed
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
            DisplaceCameraPosition(new Vector3(0, STEP_SIZE, 0));
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            DisplaceCameraPosition(new Vector3(0, -STEP_SIZE, 0));
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            DisplaceCameraPosition(new Vector3(-STEP_SIZE, 0, 0));
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            DisplaceCameraPosition(new Vector3(STEP_SIZE, 0, 0));
        }

        //Sample the mouse position
        myPrevMousePosition = GetMouseInWorldSpace();
    }

    /// <summary>
    /// Helper method that converts the mouses position in pixel-space to world-space for dragging functionality.
    /// </summary>
    /// <returns> The coordinates of the mouse in world space. </returns>
    private Vector3 GetMouseInWorldSpace() {
        return (myCamera.GetComponent<Camera>()).ScreenToWorldPoint(Input.mousePosition);

    }

    /// <summary>
    /// Returns the position of the camera.
    /// </summary>
    /// <returns> The position of the camera. </returns>
    private Vector3 GetCameraPosition() {
        return myCamera.transform.localPosition;
    }

    /// <summary>
    /// Changes the position of the camera. The given point is clamped to values such that the specified camera bounds are respected after the change in position.
    /// </summary>
    /// <param name="thePosition"> The new position of the camera. </param>
    private void SetCameraPosition(Vector3 thePosition) {
        //Clamp the values to keep the camera within the proper bounds
        float y = Math.Clamp(thePosition.y, -myYBound + GetCameraSize() + myOrigin.y, myYBound - GetCameraSize() + myOrigin.y);
        float x = Math.Clamp(thePosition.x, -myXBound + (GetCameraSize() / ASPECT_Y * ASPECT_X)  + myOrigin.x, myXBound - (GetCameraSize() / ASPECT_Y * ASPECT_X)  + myOrigin.x);

        myCamera.transform.localPosition = new Vector3(x, y, -1);
    }

    /// <summary>
    /// Applies the given displacement vector to the cameras position.
    /// </summary>
    /// <param name="theDelta"> The vector displacement that should be applied to the camera, </param>
    private void DisplaceCameraPosition(Vector3 theDelta) {
        SetCameraPosition(GetCameraPosition() + theDelta);
    }

    /// <summary>
    /// Returns the size of the camera. The size corresponds to the cameras half-height. See Camera.size in the Unity Docs for more info.
    /// </summary>
    /// <returns> The size of the camera. The size corresponds to the cameras half-height. See Camera.size in the Unity Docs for more info.</returns>
    private float GetCameraSize() {
        return (myCamera.GetComponent<Camera>()).orthographicSize;
    }

    /// <summary>
    /// Sets the size of the camera to the given value. The size corresponds to the cameras half-height. See Camera.size in the Unity Docs for more info.
    /// </summary>
    /// <param name="theSize"> The new size of the camera. </param>
    private void SetCameraSize(float theSize) {
        //Clamp the size to the specified bounds.
        float size = Math.Clamp(theSize, myMinSize, myMaxSize);

        (myCamera.GetComponent<Camera>()).orthographicSize = size;
        
        //Set the camera position to itself to automatically clamp the coordinates to the new size
        SetCameraPosition(GetCameraPosition());
    }

    /// <summary>
    /// Displaces the cameras size by the given delta value.
    /// </summary>
    /// <param name="theDelta"> The amount that the size of the camera should be changed. </param>
    private void DisplaceCameraSize(float theDelta) {
        SetCameraSize(GetCameraSize() + theDelta);
    }

    /// <summary>
    /// Sets the cameras position to the specified origin, and sets the cameras size to the specified maximum.
    /// </summary>
    private void ResetCamera() {
        SetCameraPosition(myOrigin);
        SetCameraSize(myMaxSize);
    }
}