using DefaultNamespace;
using UnityEngine;

/// <summary>
/// Representation of a cell that can hold and display the data of a hero.
/// </summary>
sealed class testButton : MonoBehaviour
{
    [Header("Events")]
    /// <summary>
    /// The GameEvent called whenever a cell has been clicked on.
    /// </summary>
    public GameEvent onButtonClick;

    /// <summary>
    /// A reference to the arrow sprite that displays when prompting the user
    /// to move their hero.
    /// </summary>
    public GameObject arrowDisplay;

    /// <summary>
    /// A reference to testHero object being represented in the instance of the cell.
    /// </summary>
    public testHero characterRepresentative;

    /// <summary>
    /// A vector representing the size of the arrow, when displayed.
    /// </summary>
    private Vector2 arrowSize;

    /// <summary>
    /// A string ID of the cell that was clicked.
    /// </summary>
    private string clickedCellName;

    /// <summary>
    /// A vector representing the location of the clicked cell.
    /// </summary>
    private Vector3 clickedVector;

    /// <summary>
    /// A vector representing the location of the arrow.
    /// </summary>
    private Vector3 arrowVector;

    /// <summary>
    /// A sprite array containing the different display states of a cell.
    /// </summary>
    public Sprite[] spriteArray = new Sprite[2];

    /// <summary>
    /// A text mesh array containing the different text displays that show up as the hero's stats.
    /// </summary>
    public TextMesh[] stats = new TextMesh[3];

    /// <summary>
    /// A reference to the area labelling the hero's name.
    /// </summary>
    public GameObject labelArea;

    /// <summary>
    /// Determines whether or not this cell holds a hero.
    /// </summary>
    private bool hasHero = false;

    /// <summary>
    /// Determines whether or not this cell is being evaluated for a hero translation or not.
    /// </summary>
    private bool selectMoveMode = false;

    /// <summary>
    /// Determines whether or not this cell is being held down by the mouse.
    /// </summary>
    private bool held = false;

    /// <summary>
    /// Determines whether or not this cell was selected as the hero to move to a new location.
    /// </summary>
    private bool clicked = false;

    /// <summary>
    /// A reference to the SpriteRenderer component of this object.
    /// </summary>
    private SpriteRenderer rend;

    // Start is called before the first frame update

    void Start()
    {
        arrowDisplay.SetActive(false);
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        hasHero = (characterRepresentative != null);

        if (hasHero)
        {
            stats[0].text = "" + characterRepresentative.GetAttack();
            stats[1].text = "" + characterRepresentative.GetDefence();
            stats[2].text = "" + characterRepresentative.GetName();
        }
        else
        {
            stats[0].text = "";
            stats[1].text = "";
            stats[2].text = "";
        }

        rend.sprite = spriteArray[System.Convert.ToInt32(hasHero)];
        labelArea.SetActive(hasHero);
    }

    void OnMouseOver()
    {
        // Click on cell
        if (Input.GetMouseButtonDown(0))
        {
            // Checking to see if clicking a cell should select it or cause a character to be moved
            if (!selectMoveMode)
            {
                // (First click) checking cell for character
                if (hasHero)
                {
                    onButtonClick.Raise(
                        this,
                        new DataPacket(gameObject.transform.position, "ArrowVector")
                    );
                    rend.sprite = spriteArray[System.Convert.ToInt32(hasHero)];
                    held = true;
                    clicked = !clicked;
                }
            }
            else
            {
                // (Second click) checking clicked cell for empty
                if (!hasHero)
                {
                    onButtonClick.Raise(
                        this,
                        new DataPacket(clickedCellName, "SwapRequest", "Button Factory")
                    );
                    onButtonClick.Raise(
                        this,
                        new DataPacket(gameObject.transform.position, "ArrowVector")
                    );
                    clicked = false;
                    rend.color = Color.white;
                    arrowDisplay.SetActive(false);
                }
                else if (clicked)
                {
                    onButtonClick.Raise(
                        this,
                        new DataPacket(gameObject.transform.position, "ArrowVector")
                    );
                    rend.sprite = spriteArray[System.Convert.ToInt32(hasHero)];
                    held = true;
                    clicked = !clicked;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            held = false;
        }

        if (held)
        {
            rend.color = new Color(0.0f, 0.5f, 0.0f);
        }
        else if (hasHero || selectMoveMode)
        {
            rend.color = Color.green;
            if (selectMoveMode && !clicked)
            {
                arrowDisplay.SetActive(true);
                arrowDisplay.transform.position = arrowVector;
                arrowDisplay.GetComponent<SpriteRenderer>().size = arrowSize;

                // Obtains the new rotation values by calling LookAt to obtain new Z and W values.
                arrowDisplay.transform.LookAt(gameObject.transform, new Vector3(0f, 0f, -1f));
                float newZ = arrowDisplay.transform.rotation.z;
                float newW = arrowDisplay.transform.rotation.w;
                arrowDisplay.transform.rotation = new Quaternion(0f, 0f, newZ, newW);
                if (hasHero)
                {
                    rend.color = Color.red;
                }
            }
        }
    }

    void OnMouseExit()
    {
        held = false;
        if (selectMoveMode)
        {
            arrowDisplay.SetActive(false);
        }
        if (!clicked)
        {
            rend.color = Color.white;
        }
        else
        {
            rend.color = Color.yellow;
        }
    }
    /// <summary>
    /// Toggles the 'clicked' variable.
    /// </summary>
    public void ToggleClicked()
    {
        clicked = !clicked;
    }

    /// <summary>
    /// Intakes a DataPacket and utilizes the data, depending on the label of the packet.
    /// Can be expanded upon to include more behaviors.
    /// </summary>
    /// <param name="sender"> The component that sent the DataPacket. </param>
    /// <param name="data"> The object (DataPacket) held. </param>
    public void ReceiveDataPacket(Component sender, object data)
    {
        DataPacket dPacket = (DataPacket)data;
        if ((dPacket.GetDestination() == null || dPacket.GetDestination().Equals(gameObject.name)))
        {
            object incomingData = dPacket.GetData();
            string dataLabel = dPacket.GetLabel();
            if (dataLabel.Equals("ArrowVector"))
            {
                selectMoveMode = !selectMoveMode;
                clickedCellName = sender.name;
                clickedVector = (Vector3)incomingData;
                arrowVector = new Vector3(
                    gameObject.transform.position.x
                        + (clickedVector.x - gameObject.transform.position.x) / 2,
                    gameObject.transform.position.y
                        + (clickedVector.y - gameObject.transform.position.y) / 2,
                    -0.4f
                );
                arrowSize = new Vector2(
                    10f,
                    20
                        * Mathf.Sqrt(
                            Mathf.Pow((clickedVector.x - gameObject.transform.position.x) / 2, 2)
                                + Mathf.Pow(
                                    (clickedVector.y - gameObject.transform.position.y) / 2,
                                    2
                                )
                        )
                );
            }
            else if (dataLabel.Equals("CharacterData"))
            {
                characterRepresentative = (testHero)incomingData;
            }
        }
    }
}
