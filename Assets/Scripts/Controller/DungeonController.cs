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

    /// <summary>
    /// A GameObject reference to the map camera. **Must initialize by using the Unity Editor**.
    /// </summary>
    [SerializeField]
    private GameObject myMapCamera;

    /// <summary>
    /// A GameObject reference to the main menu camera. **Must initialize by using the Unity Editor**.
    /// </summary>
    [SerializeField]
    private GameObject myMenuCamera;

    /// <summary>
    /// A GameObject reference to the combat camera. **Must initialize by using the Unity Editor**.
    /// </summary>
    [SerializeField]
    private GameObject myCombatCamera;

    /// <summary>
    /// The controller to the map camera. Allows for the camera to move and zoom while respecting bounds.
    /// </summary>
    private CameraController myMapCameraController;

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
        myMapCameraController = new CameraController(myMapCamera, myMapCamera.transform.localPosition, (4.5f, 18.0f), (32.0f, 18.0f));
        myMapView = new MapView(myMapCamera.transform.localPosition);
        myMapModel = new DungeonMap();

        myMapCamera.SetActive(false);
        myCombatCamera.SetActive(false);
        myMenuCamera.SetActive(true);
    }

    /// <summary>
    /// The Update method is called once per frame while the DungeonController GameObject is active.
    /// </summary>
    public void Update() {
        UpdateMapView();
        //DebugMapView();
        myMapCameraController.UpdateCamera();

        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            SwitchToMainMenu();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2)) {
            SwitchToCombat();
        }

        if (Input.GetKeyDown(KeyCode.Keypad3)) {
            SwitchToMap();
        }
    }

    /// <summary>
    /// Called once per frame to keep the MapView up to date with the MapModel.
    /// </summary>
    private void UpdateMapView() {
        myMapView.GiveAllSprite(mySprites[2]);

        myMapView.GiveSprite(myMapModel.GetFocusedRoom(), mySprites[0]);

        //Look at each adjacent room and give them the secondary focus sprite
        for (int i = 0; myMapModel.GetNthAdjacentRoom(i) != null; i++) {
            myMapView.GiveSprite(myMapModel.GetNthAdjacentRoom(i), mySprites[1]);

            if (myMapModel.GetNthAdjacentRoom(i).GetEnemyFlag()) {
                myMapView.GiveIcon(myMapModel.GetNthAdjacentRoom(i), mySprites[3]);
            }
        }
    }

    /// <summary>
    /// Saves the given DungeonMap to a file of the given name
    /// </summary>
    /// <param name="theFileName"> The name of the file that the map should be saved to. </param>
    /// <param name="theMap"> The DungeonMap that should be saved. </param>
    private void SerializeMap(in String theFileName, in DungeonMap theMap) {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(theFileName, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, theMap);
        stream.Close();
    }

    /// <summary>
    /// Returns the DungeonMap that was saved to the given file. Make sure you update
    /// </summary>
    /// <param name="theFileName"> The name of the file that the dungeon map is located in. </param>
    /// <returns> The deserialized dungeon map. </returns>
    private DungeonMap DeserializeMap(in String theFileName) {

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(theFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        
        //Save the map to a temp variable so we can close the file stream
        DungeonMap tempMap = (DungeonMap) formatter.Deserialize(stream);
        stream.Close();

        return tempMap;
    }

    /// <summary>
    /// Saves the game to a specific file. Will override any previous saves.
    /// </summary>
    public void SaveGame() {
        SerializeMap("myMap.bin", myMapModel);

        //Add stuff to save enemy party queue and user party here
    }

    /// <summary>
    /// Loads the game from existing files. If the files do not exist, a new game is created.
    /// </summary>
    public void LoadGame() {
        //Check if a game file exists; If not, load a new game

        myMapModel = DeserializeMap("myMap.bin");
        myMapView.ClearMap();

        for (int i = 0; i < myMapModel.GetRoomCount(); i++) {
            if (myMapModel.GetNthRoom(i).GetSeenFlag()) {
                myMapView.GiveSprite(myMapModel.GetNthRoom(i), mySprites[1]);

                if (myMapModel.GetNthRoom(i).GetEnemyFlag()) {
                    myMapView.GiveIcon(myMapModel.GetNthRoom(i), mySprites[3]);
                }
            }
        }

        //Add stuff to deserialize enemy party queue and user party here
    }

    /// <summary>
    /// Switches the active camera to the main menu camera.
    /// </summary>
    public void SwitchToMainMenu() {
        myMapCamera.SetActive(false);
        myCombatCamera.SetActive(false);
        myMenuCamera.SetActive(true);
    }

    /// <summary>
    /// Switches the active camera to the combat camera.
    /// </summary>
    public void SwitchToCombat() {
        myMapCamera.SetActive(false);
        myCombatCamera.SetActive(true);
        myMenuCamera.SetActive(false);
    }

    /// <summary>
    /// Switches the active camera to the map camera.
    /// </summary>
    public void SwitchToMap() {
        myMapCamera.SetActive(true);
        myCombatCamera.SetActive(false);
        myMenuCamera.SetActive(false);
    }

    /// <summary>
    /// Exits the game application. This is ignored when called while in the editor.
    /// </summary>
    public void QuitGame() {
        Application.Quit();
    }

    /// <summary>
    /// Called by the MapView whenever a valid adjacent room was left-clicked.
    /// </summary>
    /// <param name="theID"> The ID of the DungeonRoom that the Focus of the map should be set to.</param>
    public void MapViewListener(int theID) {
        //We use the ID to look through the adjacent rooms until we find a matching ID
        for (int i = 0; myMapModel.GetNthAdjacentRoom(i) != null; i++) {
            if (theID == myMapModel.GetNthAdjacentRoom(i).GetID()) {
                myMapModel.FocusNthAdjacentRoom(i);

                if (myMapModel.GetFocusedRoom().GetEnemyFlag()) {
                    //Pull from enemy party queue and initialize combat here

                    SwitchToCombat();
                }
            }
        }
    }

    /// <summary>
    /// Called when the user clicks the 'continue' button in the main menu
    /// </summary>
    public void ContinueGame() {
        LoadGame();
        SwitchToMap();
    }
}


