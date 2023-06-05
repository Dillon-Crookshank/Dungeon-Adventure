using System.Threading;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace DungeonAdventure
{
    /// <summary>
    /// The Controller to the Dungeon Adventure Game. **Must attach this script to a GameObject initialized in the Unity Editor**
    /// </summary>
    public class DungeonController : MonoBehaviour
    {
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

        private Queue<EnemyParty> myEnemyQueue;

        private PlayerParty myPlayerParty;

        private Combat myCombatModel;

        private bool isGameLoaded;

        /// <summary>
        /// A flag used to check if an animation is still in progress, so that the controller knows to wait.
        /// </summary>
        private bool myAnimationFlag;

        /// <summary>
        /// The Start method is run once, after the DungeonController GameObject is initialized.
        /// </summary>
        public void Start()
        {
            myMapCameraController = new CameraController(
                myMapCamera,
                myMapCamera.transform.localPosition,
                (4.5f, 18.0f),
                (32.0f, 18.0f)
            );
            myMapView = new MapView(myMapCamera.transform.localPosition);

            myMapCamera.SetActive(false);
            myCombatCamera.SetActive(false);
            myMenuCamera.SetActive(true);

            myAnimationFlag = false;
            GameObject.Find("Continue Button").SetActive(File.Exists("myMap.bin"));
        }

        /// <summary>
        /// The Update method is called once per frame while the DungeonController GameObject is active.
        /// </summary>
        public void Update()
        {
            
            if (isGameLoaded){
                (GameObject.Find("Button Factory")).SendMessage("setDisplayedParty", myPlayerParty);
                //Do extra check since peek throws an error when the queue is empty
                if (myEnemyQueue.Count != 0)
                {
                    (GameObject.Find("Enemy Button Factory")).SendMessage(
                        "setDisplayedParty",
                        myEnemyQueue.Peek()
                    );
                }

                //If either party is dead, we know that combat was just finished
                if (myEnemyQueue.Count != 0 && !(myEnemyQueue.Peek()).IsAllAlive() && !myAnimationFlag)
                {
                    //Show "enemies slain"
                    myAnimationFlag = true;
                    GameObject.Find("Enemies Slain Display").SendMessage("DoFadeAnimation");
                    myCombatModel.stopCombat();
                    //Response : EndCombatEncounter();
                }
                else if (!myPlayerParty.IsAllAlive() && !myAnimationFlag)
                {
                    //Show "you died"
                    myAnimationFlag = true;
                    GameObject.Find("You Died Display").SendMessage("DoFadeAnimation");
                    myCombatModel.stopCombat();
                    //Response : SwitchToMainMenu();
                }

                //left-ctr + w -> kill entire enemy party
                if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.LeftControl))
                {
                    foreach (
                        AbstractCharacter character in myEnemyQueue.Peek().GetPartyPositions().Values
                    )
                    {
                        character.CurrentHitpoints = -999;
                    }
                }

                //left-ctr + l -> kill entire user party
                if (Input.GetKeyDown(KeyCode.L) && Input.GetKey(KeyCode.LeftControl))
                {
                    foreach (AbstractCharacter character in myPlayerParty.GetPartyPositions().Values)
                    {
                        character.CurrentHitpoints = -999;
                    }
                }

                UpdateMapView();
                myMapCameraController.UpdateCamera();
            }
        }

        /// <summary>
        /// Called once per frame to keep the MapView up to date with the MapModel.
        /// </summary>
        private void UpdateMapView()
        {
            myMapView.GiveAllSprite(mySprites[2]);

            myMapView.GiveSprite(myMapModel.GetFocusedRoom(), mySprites[0]);

            //Look at each adjacent room and give them the secondary focus sprite
            for (int i = 0; myMapModel.GetNthAdjacentRoom(i) != null; i++)
            {
                myMapView.GiveSprite(myMapModel.GetNthAdjacentRoom(i), mySprites[1]);
            }

            for (int i = 0; i < myMapModel.GetRoomCount(); i++)
            {
                if (
                    myMapModel.GetNthRoom(i).GetEnemyFlag()
                    && myMapModel.GetNthRoom(i).GetSeenFlag()
                )
                {
                    myMapView.GiveIcon(myMapModel.GetNthRoom(i), mySprites[3]);
                }
                else
                {
                    myMapView.ClearIcon(myMapModel.GetNthRoom(i));
                }
            }
        }

        /// <summary>
        /// Saves the given DungeonMap to a file of the given name
        /// </summary>
        /// <param name="theFileName"> The name of the file that the map should be saved to. </param>
        /// <param name="theMap"> The DungeonMap that should be saved. </param>
        private void SerializeObject(in String theFileName, in System.Object theMap)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(
                theFileName,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None
            );
            formatter.Serialize(stream, theMap);
            stream.Close();
        }

        /// <summary>
        /// Returns the DungeonMap that was saved to the given file. Make sure you update
        /// </summary>
        /// <param name="theFileName"> The name of the file that the dungeon map is located in. </param>
        /// <returns> The deserialized dungeon map. </returns>
        private System.Object DeserializeObject(in String theFileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(
                theFileName,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read
            );

            //Save the map to a temp variable so we can close the file stream
            System.Object temp = formatter.Deserialize(stream);
            stream.Close();

            return temp;
        }

        /// <summary>
        /// Saves the game to a specific file. Will override any previous saves.
        /// </summary>
        public void SaveGame()
        {
            SerializeObject("myMap.bin", myMapModel);
            SerializeObject("EnemyQueue.bin", myEnemyQueue);
            SerializeObject("PlayerParty.bin", myPlayerParty);

            //Add stuff to save enemy party queue and user party here
        }

        /// <summary>
        /// Loads the game from existing files. If the files do not exist, a new game is created.
        /// </summary>
        public void LoadGame()
        {
            //Check if a game file exists; If not, load a new game

            myMapModel = (DungeonMap)DeserializeObject("myMap.bin");
            myEnemyQueue = (Queue<EnemyParty>)DeserializeObject("EnemyQueue.bin");
            myPlayerParty = (PlayerParty)DeserializeObject("PlayerParty.bin");

            myMapView.ClearMap();

            for (int i = 0; i < myMapModel.GetRoomCount(); i++)
            {
                if (myMapModel.GetNthRoom(i).GetSeenFlag())
                {
                    myMapView.GiveSprite(myMapModel.GetNthRoom(i), mySprites[1]);

                    if (myMapModel.GetNthRoom(i).GetEnemyFlag())
                    {
                        myMapView.GiveIcon(myMapModel.GetNthRoom(i), mySprites[3]);
                    }
                }
            }
            SetGameLoadedFlag();
        }

        /// <summary>
        /// Switches the active camera to the main menu camera.
        /// </summary>
        public void SwitchToMainMenu()
        {
            
            myMapCamera.SetActive(false);
            myCombatCamera.SetActive(false);
            myMenuCamera.SetActive(true);
            myAnimationFlag = false;
            isGameLoaded = false;
            if (File.Exists("myMap.bin")){
                File.Delete("myMap.bin");
            }   
            if (!isGameLoaded){
                try {
                    GameObject.Find("Continue Button").SetActive(false);
                } catch (Exception e){
                    Debug.Log(e.Message);
                }
            }
        }

        /// <summary>
        /// Switches the active camera to the combat camera.
        /// </summary>
        public void SwitchToCombat()
        {
            myMapCamera.SetActive(false);
            myCombatCamera.SetActive(true);
            myMenuCamera.SetActive(false);
        }

        /// <summary>
        /// Switches the active camera to the map camera.
        /// </summary>
        public void SwitchToMap()
        {
            myMapCamera.SetActive(true);
            myCombatCamera.SetActive(false);
            myMenuCamera.SetActive(false);
        }

        /// <summary>
        /// Exits the game application. This is ignored when called while in the editor.
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }

        /// <summary>
        /// Called by the MapView whenever a valid adjacent room was left-clicked.
        /// </summary>
        /// <param name="theID"> The ID of the DungeonRoom that the Focus of the map should be set to.</param>
        public void MapViewListener(int theID)
        {
            //We use the ID to look through the adjacent rooms until we find a matching ID
            for (int i = 0; myMapModel.GetNthAdjacentRoom(i) != null; i++)
            {
                if (theID == myMapModel.GetNthAdjacentRoom(i).GetID())
                {
                    myMapModel.FocusNthAdjacentRoom(i);

                    if (myMapModel.GetFocusedRoom().GetEnemyFlag())
                    {
                        //Pull from enemy party queue and initialize combat here

                        myCombatModel = new Combat(myPlayerParty, myEnemyQueue.Peek());

                        SwitchToCombat();
                    }
                }
            }
        }

        /// <summary>
        /// Called when the user clicks the 'continue' button in the main menu
        /// </summary>
        public void ContinueGame()
        {
            LoadGame();
            SwitchToMap();
        }

        /// <summary>
        /// Called by
        /// </summary>
        public void NewGame()
        {
            myMapModel = new DungeonMap();
            myEnemyQueue = EnemyPartyQueue.CreateEnemyQueue();
            int randomNumber = UnityEngine.Random.Range(0, 10);
            int numberOfPartyMembers = 0;
            if (randomNumber < 2){
                numberOfPartyMembers = 2;
            } else if (randomNumber < 6){
                numberOfPartyMembers = 3;
            } else if (randomNumber < 8){
                numberOfPartyMembers = 4;
            } else {
                numberOfPartyMembers = 5;
            }
            myPlayerParty = new PlayerParty(AccessDB.PlayerDatabaseConstructor("warrior"));

            string classNameAdder = "";
            for (int i = 0; i < numberOfPartyMembers - 1; i++){
                randomNumber = UnityEngine.Random.Range(0, 6);
                switch (randomNumber){
                    case 0:
                        classNameAdder = "warrior";
                        break;
                    case 1:
                        classNameAdder = "barbarian";
                        break;
                    case 2:
                        classNameAdder = "rogue";
                        break;
                    case 3:
                        classNameAdder = "wizard";
                        break;
                    case 4:
                        classNameAdder = "cleric";
                        break;
                    case 5:
                        classNameAdder = "archer";
                        break;
                }
                myPlayerParty.AddCharacter(AccessDB.PlayerDatabaseConstructor(classNameAdder));
            }
            // myPlayerParty.AddCharacter(AccessDB.PlayerDatabaseConstructor("barbarian"));
            // myPlayerParty.AddCharacter(AccessDB.PlayerDatabaseConstructor("rogue"));
            // myPlayerParty.AddCharacter(AccessDB.PlayerDatabaseConstructor("wizard"));
            // myPlayerParty.AddCharacter(AccessDB.PlayerDatabaseConstructor("cleric"));
            // myPlayerParty.AddCharacter(AccessDB.PlayerDatabaseConstructor("archer"));

            SaveGame();

            myMapView.ClearMap();

            for (int i = 0; i < myMapModel.GetRoomCount(); i++)
            {
                if (myMapModel.GetNthRoom(i).GetSeenFlag())
                {
                    myMapView.GiveSprite(myMapModel.GetNthRoom(i), mySprites[1]);

                    if (myMapModel.GetNthRoom(i).GetEnemyFlag())
                    {
                        myMapView.GiveIcon(myMapModel.GetNthRoom(i), mySprites[3]);
                    }
                }
            }
            SetGameLoadedFlag();
            SwitchToMap();
        }

        void EndCombatEncounter()
        {
            //Determine if that was the last battle of the game

            myMapModel.GetFocusedRoom().SetEnemyFlag(false);
            SwitchToMap();
            if (myEnemyQueue.Count != 1)
            {
                myEnemyQueue.Dequeue();
                myAnimationFlag = false;

                SaveGame();
            }
            else
            {
                //show "dungeon cleared"
                myAnimationFlag = true;
                GameObject.Find("Dungeon Cleared Display").SendMessage("DoFadeAnimation");
                //Response : SwitchToMainMenu();
            }
        }

        private void SetGameLoadedFlag(){
            GameObject.Find("Button Factory").SendMessage("SetButtonsGameLoadedFlag");
            isGameLoaded = true;
        }

        /// <summary>
        /// After the attack button is clicked, calls the attack on the target character and updates the combat log.
        /// </summary>
        /// <param name="theTarget">The AbstractCharacter being attacked.</param>
        async void DeliverBasicAttack(AbstractCharacter theTarget)
        {
            GameObject
                .Find("Combat Log")
                .SendMessage(
                    "UpdateCombatLog",
                    (myCombatModel.GetActiveCharacter().Name) + " attacks " + theTarget.Name + "..."
                );
            await Task.Delay(300);
            double damage = Math.Round(myCombatModel.GetActiveCharacter().BasicAttack(theTarget), 1);
            GameObject
                .Find("Combat Log")
                .SendMessage("UpdateCombatLog", ("The attack deals " + damage + " damage!"));
            if (!theTarget.IsAlive())
            {
                GameObject
                    .Find("Combat Log")
                    .SendMessage("UpdateCombatLog", (theTarget.Name + " dies!"));
            }
            myCombatModel.EndTurn();
        }

        /// <summary>
        /// After the Special Attack button is clicked, calls the attack on the target character and updates the combat log.
        /// </summary>
        /// <param name="theTarget">The AbstractCharacter being attacked.</param>
        async void DeliverSpecialAttack(AbstractCharacter theTarget)
        {
            GameObject
                .Find("Combat Log")
                .SendMessage(
                    "UpdateCombatLog",
                    (myCombatModel.GetActiveCharacter().Name)
                        + " uses "
                        + myCombatModel.GetActiveCharacter().MySpecialAttack.SpecialAttackName
                        + " on "
                        + theTarget.Name
                        + "..."
                );
            await Task.Delay(300);
            double damage = Math.Round(myCombatModel.GetActiveCharacter().SpecialAttack(theTarget), 1);
            GameObject
                .Find("Combat Log")
                .SendMessage("UpdateCombatLog", ("The attack deals " + damage + " damage!"));
            if (!theTarget.IsAlive())
            {
                GameObject
                    .Find("Combat Log")
                    .SendMessage("UpdateCombatLog", (theTarget.Name + " dies!"));
            }
            myCombatModel.EndTurn();
        }

        void Defend()
        {
            GameObject
                .Find("Combat Log")
                .SendMessage(
                    "UpdateCombatLog",
                    (myCombatModel.GetActiveCharacter().Name) + " is on guard this turn!"
                );
            myCombatModel.GetActiveCharacter().Defend();
            myCombatModel.EndTurn();
        }

        void Buff()
        {
            GameObject
                .Find("Combat Log")
                .SendMessage(
                    "UpdateCombatLog",
                    (myCombatModel.GetActiveCharacter().Name) + " uses " + myCombatModel.GetActiveCharacter().MyBuff.BuffName + "!"
                );
            myCombatModel.GetActiveCharacter().Buff();
            myCombatModel.EndTurn();
        }

        void EndTurn()
        {
            GameObject
                .Find("Combat Log")
                .SendMessage(
                    "UpdateCombatLog",
                    (myCombatModel.GetActiveCharacter().Name) + " skips their turn!"
                );
            myCombatModel.EndTurn();
        }
    }
}
