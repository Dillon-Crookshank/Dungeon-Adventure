using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

/// <summary>
/// The Controller to the Dungeon Adventure Game. **Must attach this script to a GameObject initialized in the Unity Editor**
/// </summary>
public class DungeonController : MonoBehaviour {
    /// <summary>
    /// The Sprites that are used by the Game. **Must initialize by using the Unity Editor**.
    /// </summary>
    [SerializeField]
    private Sprite[] mySprites;

    private CameraController myMapCamera;

    /// <summary>
    /// The View component that displays the Map.
    /// </summary>
    private MapView myMapView;

    /// <summary>
    /// The Model component that holds the Map.
    /// </summary>
    private DungeonMap myMapModel;

    /// <summary>
    /// The Start method is run once, after the DungeonController GameObject is initialized.
    /// </summary>
    public void Start() {
        myMapCamera = new CameraController("Main Camera", new Vector3(0, 0, -1), (4.5f, 18.0f), (32.0f, 18.0f));
        myMapView = new MapView(new Vector2(0, 0), mySprites);
        myMapModel = new DungeonMap();
    }


    /// <summary>
    /// The Update method is called once per frame while the DungeonController GameObject is active.
    /// </summary>
    public void Update() {
        UpdateMapView();
        //DebugMapView();
        myMapCamera.UpdateCamera();
    }

    /// <summary>
    /// Called once per frame to keep the MapView up to date with the MapModel.
    /// </summary>
    private void UpdateMapView() {
        //We call UnfocusAll to reset the mapView
        myMapView.UnfocusAll();
        myMapView.SetPrimaryFocus(myMapModel.GetFocusedRoom());

        for (int i = 0; myMapModel.GetNthAdjacentRoom(i) != null; i++) {
            myMapView.SetSecondaryFocus(myMapModel.GetNthAdjacentRoom(i));
        }


        if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
            SerializeMap("myMap.bin", myMapModel);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
            myMapModel = DeserializeMap("myMap.bin");
            myMapView.Clear();


            for (int i = 0; myMapModel.GetNthRoom(i) != null; i++) {
                if (myMapModel.GetNthRoom(i).GetSeenFlag()) {
                    myMapView.SetSecondaryFocus(myMapModel.GetNthRoom(i));
                }
            }
        }
    }

    /// <summary>
    /// Shows the entire map at once. Every time you press enter, it generates a new dungeon.
    /// </summary>
    private void DebugMapView() {
        myMapView.UnfocusAll();

        for (int i = 0; myMapModel.GetNthRoom(i) != null; i++) {
            myMapView.SetSecondaryFocus(myMapModel.GetNthRoom(i));
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            myMapModel = new DungeonMap();
            myMapView.Clear();
            myMapView = new MapView(new Vector2(0, 0), mySprites);
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
            SerializeMap("myMap.bin", myMapModel);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
            myMapModel = DeserializeMap("myMap.bin");
            myMapView.Clear();


            for (int i = 0; myMapModel.GetNthRoom(i) != null; i++) {
                if (myMapModel.GetNthRoom(i).GetSeenFlag()) {
                    myMapView.SetSecondaryFocus(myMapModel.GetNthRoom(i));
                }
            }
        }
    }

    /// <summary>
    /// Saves the given DungeonMap to a file of the given name
    /// </summary>
    /// <param name="theFileName"> The name of the file that the map should be saved to. </param>
    /// <param name="theMap"> The DungeonMap that should be saved. </param>
    private void SerializeMap(String theFileName, DungeonMap theMap) {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(theFileName, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, theMap);
        stream.Close();

        Debug.Log("Map Serialized!");
    }

    /// <summary>
    /// Returns the DungeonMap that was saved to the given file. Make sure you update
    /// </summary>
    /// <param name="theFileName"></param>
    /// <returns></returns>
    private DungeonMap DeserializeMap(String theFileName) {

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(theFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        
        //Save the map to a temp variable so we can close the file stream
        DungeonMap tempMap = (DungeonMap) formatter.Deserialize(stream);
        stream.Close();

        Debug.Log("Map Deserialized!");

        return tempMap;
    }

    /// <summary>
    /// Called by the MapView whenever a valid adjacent room was left-clicked.
    /// </summary>
    /// <param name="theID"> The ID of the DungeonRoom that the Primary Focus of the map should be set to.</param>
    public void MapViewListener(int theID) {
        //We use the ID to look through the adjacent rooms until we find a matching ID
        int i = 0;
        while ( myMapModel.GetNthAdjacentRoom(i) != null
            && theID != myMapModel.GetNthAdjacentRoom(i).GetID()) {
            i++;
        }

        //Use the index to update the model if the index is valid.
        if (myMapModel.GetNthAdjacentRoom(i) != null) {          
            myMapModel.FocusNthAdjacentRoom(i);
        }
    }
}


