using System;
using DungeonAdventure;
using UnityEngine;

/// <summary>
/// Representation of a cell that can hold and display the data of a hero.
/// </summary>
sealed class playerCell : MonoBehaviour
{
    // [Header("Events")]
    // /// <summary>
    // /// The GameEvent called whenever a cell has been clicked on.
    // /// </summary>
    // [SerializeField]
    // GameEvent onButtonClick;

    /// <summary>
    /// A reference to the arrow sprite that displays when prompting the user
    /// to move their hero.
    /// </summary>
    // [SerializeField]
    // GameObject arrowDisplay;

    /// <summary>
    /// A reference to the bar sprite that represents a character's health.
    /// </summary>
    [SerializeField]
    private GameObject myHealthBar;

    /// <summary>
    /// A reference to the bar sprite that represents a character's mana.
    /// </summary>
    [SerializeField]
    private GameObject myManaBar;

    /// <summary>
    /// A reference to testHero object being represented in the instance of the cell.
    /// </summary>
    [SerializeField]
    private PlayerCharacter myCharacterRepresentative;

    /// <summary>
    /// A sprite array containing the different display states of a cell.
    /// </summary>
    [SerializeField]
    private Sprite[] mySpriteArray = new Sprite[2];

    /// <summary>
    /// A text mesh array containing the different text displays that show up as the hero's myStats.
    /// </summary>
    [SerializeField]
    TextMesh[] myStats = new TextMesh[3];

    /// <summary>
    /// A reference to the area labelling the hero's name.
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

    /// <summary>
    /// Determines whether or not this cell holds a hero.
    /// </summary>
    private bool myCellHasHero = false;

    // /// <summary>
    // /// Determines whether or not this cell is being evaluated for a hero translation or not.
    // /// </summary>
    // private bool selectMoveMode = false;

    // /// <summary>
    // /// Determines whether or not this cell is being held down by the mouse.
    // /// </summary>
    // private bool held = false;

    // /// <summary>
    // /// Determines whether or not this cell was selected as the hero to move to a new location.
    // /// </summary>
    // private bool clicked = false;

    /// <summary>
    /// Determines whether or not this cell can render its character yet.
    /// </summary>
    private bool isButtonGameLoaded = false;

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
        if (isButtonGameLoaded){
            myCellHasHero = (myCharacterRepresentative != null);

            if (myCellHasHero && name != "Template")
            {
                if (!myCharacterRepresentative.IsAlive()){
                    rend.color = new Color(0.5f, 0f, 0f, 1f);
                }
                // myStats[0].text = "" + myCharacterRepresentative.Attack;
                myStats[0].text = "" + myCharacterRepresentative.Attack;
                myStats[1].text = "" + myCharacterRepresentative.Defence;
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

                    float manaPercentage = (float)(
                        myCharacterRepresentative.CurrentMana
                        / myCharacterRepresentative.MaxMana
                    );
                    
                    myManaBar.transform.localPosition = new Vector3(
                        0f,
                        (float)(manaPercentage - 1) / 2,
                        -0.51f
                    );
                    myManaBar.transform.localScale = new Vector3(1f, manaPercentage, 1f);
                
            } else {
                rend.color = Color.white;
            }
        }
        rend.sprite = mySpriteArray[System.Convert.ToInt32(myCellHasHero)];
        myStatDisplays.SetActive(myCellHasHero);
    }

    void CheckActivePlayer(AbstractCharacter thePlayer){
        if (myCharacterRepresentative.IsAlive()){
            if (thePlayer == myCharacterRepresentative){
                rend.color = Color.yellow;
            } else {
                rend.color = Color.white;
            }
        }
    }

    public AbstractCharacter GetCharacterRepresentative()
    {
        return myCharacterRepresentative;
    }

    void SetNullCharacterRepresentative() {
        myCharacterRepresentative = null;
    }
    
    void SetCharacterRepresentative(PlayerCharacter theCharacter) {
        myCharacterRepresentative = theCharacter;
    }

    void SetGameLoadedFlag(){
        isButtonGameLoaded = true;
    }

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

    // void OnMouseOver()
    // {
    //     if (name != "Template")
    //     {
    //         // Click on cell
    //         if (Input.GetMouseButtonDown(0))
    //         {
    //             if (
    //                 myCharacterRepresentative != null && myCharacterRepresentative.IsAlive()
    //                 || myCharacterRepresentative == null
    //             )
    //             {
    //                 // Checking to see if clicking a cell should select it or cause a character to be moved
    //                 if (!selectMoveMode)
    //                 {
    //                     // (First click) checking cell for character
    //                     if (myCellHasHero)
    //                     {
    //                         onButtonClick.Raise(
    //                             this,
    //                             new DataPacket(gameObject.transform.position, "ArrowVector")
    //                         );
    //                         rend.sprite = mySpriteArray[System.Convert.ToInt32(myCellHasHero)];
    //                         held = true;
    //                         clicked = !clicked;
    //                     }
    //                 }
    //                 else
    //                 {
    //                     // (Second click) checking clicked cell for empty
    //                     if (!myCellHasHero)
    //                     {
    //                         onButtonClick.Raise(
    //                             this,
    //                             new DataPacket(clickedCellName, "SwapRequest", "Button Factory")
    //                         );
    //                         onButtonClick.Raise(
    //                             this,
    //                             new DataPacket(gameObject.transform.position, "ArrowVector")
    //                         );
    //                         clicked = false;
    //                         rend.color = Color.white;
    //                         arrowDisplay.SetActive(false);
    //                     }
    //                     else if (clicked)
    //                     {
    //                         onButtonClick.Raise(
    //                             this,
    //                             new DataPacket(gameObject.transform.position, "ArrowVector")
    //                         );
    //                         rend.sprite = mySpriteArray[System.Convert.ToInt32(myCellHasHero)];
    //                         held = true;
    //                         clicked = !clicked;
    //                     }
    //                 }
    //             }
    //         }
    //         else if (Input.GetMouseButtonUp(0))
    //         {
    //             held = false;
    //         }

    //         if (held)
    //         {
    //             rend.color = new Color(0.0f, 0.5f, 0.0f);
    //         }
    //         else if (myCellHasHero || selectMoveMode)
    //         {
    //             if (myCharacterRepresentative == null || myCharacterRepresentative.IsAlive())
    //             {
    //                 rend.color = Color.green;
    //             }
    //             else
    //             {
    //                 rend.color = Color.red;
    //             }

    //             if (selectMoveMode && !clicked)
    //             {
    //                 arrowDisplay.SetActive(true);
    //                 arrowDisplay.transform.position = arrowVector;
    //                 arrowDisplay.GetComponent<SpriteRenderer>().size = arrowSize;

    //                 // Obtains the new rotation values by calling LookAt to obtain new Z and W values.
    //                 arrowDisplay.transform.LookAt(gameObject.transform, new Vector3(0f, 0f, -1f));
    //                 float newZ = arrowDisplay.transform.rotation.z;
    //                 float newW = arrowDisplay.transform.rotation.w;
    //                 arrowDisplay.transform.rotation = new Quaternion(0f, 0f, newZ, newW);
    //                 if (myCellHasHero)
    //                 {
    //                     rend.color = Color.red;
    //                 }
    //             }
    //         }
    //     }
    // }

    // void OnMouseExit()
    // {
    //     held = false;
    //     if (selectMoveMode)
    //     {
    //         arrowDisplay.SetActive(false);
    //     }
    //     if (!clicked)
    //     {
    //         if (
    //             myCharacterRepresentative != null && myCharacterRepresentative.IsAlive()
    //             || myCharacterRepresentative == null
    //         )
    //         {
    //             rend.color = Color.white;
    //         }
    //         else
    //         {
    //             rend.color = new Color(0.5f, 0f, 0f, 1f);
    //         }
    //     }
    //     else
    //     {
    //         rend.color = Color.yellow;
    //     }
    // }

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
    //             myCharacterRepresentative = (PlayerCharacter)incomingData;
    //         }
    //     }
    // }

    
}
