using System;
using UnityEngine;

/// <summary>
/// Representation of a cell that can hold and display the data of an enemy.
/// </summary>
namespace DungeonAdventure
{
    sealed class enemyCell : MonoBehaviour
    {
        // [Header("Events")]
        // /// <summary>
        // /// The GameEvent called whenever a cell has been clicked on.
        // /// </summary>
        // [SerializeField]
        // GameEvent onButtonClick;

        // /// <summary>
        // /// A reference to the arrow sprite that displays when prompting the user
        // /// to move their hero.
        // /// </summary>
        // [SerializeField]
        // private GameObject arrowDisplay;

        /// <summary>
        /// A reference to the bar sprite that represents a character's health.
        /// </summary>
        [SerializeField]
        private GameObject myHealthBar;

        /// <summary>
        /// A reference to the AbstractCharacter object being represented in the instance of the cell.
        /// </summary>
        [SerializeField]
        private AbstractCharacter myCharacterRepresentative;

        /// <summary>
        /// A sprite array containing the different display states of a cell.
        /// </summary>
        [SerializeField]
        private Sprite[] mySpriteArray = new Sprite[2];

        /// <summary>
        /// A text mesh array containing the different text displays that show the enemy's stats.
        /// </summary>
        [SerializeField]
        private TextMesh[] myStats = new TextMesh[3];

        /// <summary>
        /// A reference to the game object displaying the enemy's stats.
        /// </summary>
        [SerializeField]
        private GameObject myStatDisplays;

        // /// <summary>
        // /// A vector representing the size of the arrow, when displayed.
        // /// </summary>
        // private Vector2 arrowSize;

        // /// <summary>
        // /// A string ID of the cell that was clicked.
        // /// </summary>
        // private string clickedCellName;

        // /// <summary>
        // /// A vector representing the location of the clicked cell.
        // /// </summary>
        // private Vector3 clickedVector;

        // /// <summary>
        // /// A vector representing the location of the arrow.
        // /// </summary>
        // private Vector3 arrowVector;

        private string behaviorString;

        /// <summary>
        /// Determines whether or not this cell holds a hero.
        /// </summary>
        private bool hasHero = false;

        // /// <summary>
        // /// Determines whether or not this cell is being evaluated for a hero translation or not.
        // /// </summary>
        // private bool selectMoveMode = false;

        // /// <summary>
        // /// Determines whether or not this cell is being held down by the mouse.
        // /// </summary>
        // private bool held = false;

        /// <summary>
        /// Determines whether or not this cell was selected as the hero to move to a new location.
        /// </summary>
        private bool clicked = false;

        private bool hovered = false;

        private bool isSelectableByAction = false;

        /// <summary>
        /// A reference to the SpriteRenderer component of this object.
        /// </summary>
        private SpriteRenderer rend;

        // Start is called before the first frame update

        void Start()
        {
            // arrowDisplay.SetActive(false);
            rend = gameObject.GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            hasHero = (myCharacterRepresentative != null);

            if (hasHero && name != "EnemyTemplate")
            {
                if (!myCharacterRepresentative.IsAlive())
                {
                    rend.color = new Color(0.5f, 0f, 0f, 1f);
                }
                else if (hovered)
                {
                    rend.color = Color.red;
                }
                // myStats[0].text = "" + myCharacterRepresentative.Attack;
                myStats[0].text = "" + Math.Round(myCharacterRepresentative.Attack, 0);
                myStats[1].text = "" + Math.Round(myCharacterRepresentative.Defence, 0);
                myStats[2].text = "" + myCharacterRepresentative.Name;
                myStats[2].GetComponent<TextMesh>().fontSize = 93 - (myStats[2].text.Length * 2);

                float healthPercentage = (float)(
                    myCharacterRepresentative.CurrentHitpoints
                    / myCharacterRepresentative.MaxHitpoints
                );

                myHealthBar.transform.localPosition = new Vector3(
                    0f,
                    (float)(healthPercentage - 1) / 2,
                    -0.51f
                );
                myHealthBar.transform.localScale = new Vector3(1f, healthPercentage, 1f);
            }
            else
            {
                rend.color = Color.white;
            }
            rend.sprite = mySpriteArray[System.Convert.ToInt32(hasHero)];
            myStatDisplays.SetActive(hasHero);
        }

        /// <summary>
        /// Checks to see if the active character is the character held in this cell.
        /// </summary>
        /// <param name="thePlayer"> The currently active player. </param>
        void CheckActivePlayer(AbstractCharacter thePlayer)
        {
            if (myCharacterRepresentative.IsAlive())
            {
                if (thePlayer == myCharacterRepresentative)
                {
                    rend.color = Color.yellow;
                }
                else
                {
                    rend.color = Color.white;
                }
            }
        }

        void OnMouseOver()
        {
            if (isSelectableByAction && hasHero && myCharacterRepresentative.IsAlive())
            {
                hovered = true;
                // arrowDisplay.SetActive(true);
                // arrowDisplay.transform.position = arrowVector;
                // arrowDisplay.GetComponent<SpriteRenderer>().size = arrowSize;

                // Obtains the new rotation values by calling LookAt to obtain new Z and W values.
                // arrowDisplay.transform.LookAt(gameObject.transform, new Vector3(0f, 0f, -1f));
                // float newZ = arrowDisplay.transform.rotation.z;
                // float newW = arrowDisplay.transform.rotation.w;
                // arrowDisplay.transform.rotation = new Quaternion(0f, 0f, newZ, newW);
                if (hasHero && myCharacterRepresentative.IsAlive())
                {
                    rend.color = Color.red;
                    if (Input.GetMouseButtonDown(0))
                    {
                        GameObject
                            .Find("Dungeon Controller")
                            .SendMessage(behaviorString, myCharacterRepresentative);
                        // Debug.Log("We are attacking " + name);
                        for (int i = 1; i <= 6; i++)
                        {
                            GameObject.Find("E" + i).SendMessage("setBehaviorString", "");
                        }
                    }
                }
            }
        }

        void OnMouseExit()
        {
            hovered = false;
            // held = false;
            // if (selectMoveMode)
            // {
            //     arrowDisplay.SetActive(false);
            // }
            if (!clicked)
            {
                if (
                    myCharacterRepresentative != null && myCharacterRepresentative.IsAlive()
                    || myCharacterRepresentative == null
                )
                {
                    rend.color = Color.white;
                }
                else
                {
                    rend.color = new Color(0.5f, 0f, 0f, 1f);
                }
            }
            else
            {
                rend.color = Color.yellow;
            }
        }

        // /// <summary>
        // /// Toggles the 'clicked' variable.
        // /// </summary>
        // public void ToggleClicked()
        // {
        //     clicked = !clicked;
        // }

        // /// <summary>
        // /// Intakes a DataPacket and utilizes the data, depending on the label of the packet.
        // /// Can be expanded upon to include more behaviors.
        // /// </summary>
        // /// <param name="sender"> The component that sent the DataPacket. </param>
        // /// <param name="data"> The object (DataPacket) held. </param>
        // public void ReceiveDataPacket(Component sender, object data)
        // {
        //     DataPacket dPacket = (DataPacket)data;
        //     if ((dPacket.GetDestination() == null || dPacket.GetDestination().Equals(gameObject.name)))
        //     {
        //         object incomingData = dPacket.GetData();
        //         string dataLabel = dPacket.GetLabel();
        //         if (dataLabel.Equals("ArrowVector"))
        //         {
        //             selectMoveMode = !selectMoveMode;
        //             clickedCellName = sender.name;
        //             clickedVector = (Vector3)incomingData;
        //             arrowVector = new Vector3(
        //                 gameObject.transform.position.x
        //                     + (clickedVector.x - gameObject.transform.position.x) / 2,
        //                 gameObject.transform.position.y
        //                     + (clickedVector.y - gameObject.transform.position.y) / 2,
        //                 -0.4f
        //             );
        //             arrowSize = new Vector2(
        //                 10f,
        //                 20
        //                     * Mathf.Sqrt(
        //                         Mathf.Pow((clickedVector.x - gameObject.transform.position.x) / 2, 2)
        //                             + Mathf.Pow(
        //                                 (clickedVector.y - gameObject.transform.position.y) / 2,
        //                                 2
        //                             )
        //                     )
        //             );
        //         }
        //         else if (dataLabel.Equals("CharacterData"))
        //         {
        //             // // Debug.Log(incomingData);
        //             myCharacterRepresentative = (AbstractCharacter)incomingData;
        //         }
        //     }
        // }

        // public void HandleDamage(Component sender, object data)
        // {
        //     DataPacket dPacket = (DataPacket)data;
        //     int number = 0;
        //     if (
        //         dPacket.GetLabel() == "DamageAmount"
        //         && myCharacterRepresentative != null
        //         && Int32.TryParse((string)dPacket.GetData(), out number)
        //     )
        //     {
        //         myCharacterRepresentative.CurrentHitpoints = number;
        //         if (myCharacterRepresentative.IsAlive())
        //         {
        //             rend.color = Color.white;
        //         }
        //         else
        //         {
        //             rend.color = new Color(0.5f, 0f, 0f, 1f);
        //         }
        //     }
        // }

        // public AbstractCharacter GetCharacterRepresentative()
        // {
        //     return myCharacterRepresentative;
        // }

        /// <summary>
        /// Sets the behaviors of the enemy cells to be dependent on whether a given action has been selected.
        /// </summary>
        /// <param name="theCharacter"> The character to display. </param>
        public void setBehaviorString(string theBehavior)
        {
            behaviorString = theBehavior;
            isSelectableByAction = (theBehavior != "");
        }

        /// <summary>
        /// Sets the character held here to be empty.
        /// </summary>
        void SetNullCharacterRepresentative()
        {
            myCharacterRepresentative = null;
        }

        /// <summary>
        /// Sets the character held here to be a new character.
        /// </summary>
        /// <param name="theCharacter"> The character to display. </param>
        void SetCharacterRepresentative(EnemyCharacter theCharacter)
        {
            myCharacterRepresentative = theCharacter;
        }
    }
}
