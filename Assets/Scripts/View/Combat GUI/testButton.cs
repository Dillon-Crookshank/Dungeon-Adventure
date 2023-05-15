using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;

sealed class testButton : MonoBehaviour
{
    [Header("Events")]
    public GameEvent onButtonClicked;

    public GameEvent onToggleMove;

    public GameObject arrowDisplay;

    public testHero characterRepresentative;

    private Vector2 arrowSize;

    private string clickedCellName;

    private Vector3 clickedVector;

    private Vector3 arrowVector;

    public Sprite[] spriteArray = new Sprite[2];

    public TextMesh[] stats = new TextMesh[3];

    public GameObject labelArea;

    bool hasHero = false;
    bool selectMoveMode = false;
    bool held = false;
    bool clicked = false;

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

        if (hasHero){
            stats[0].text = "" + characterRepresentative.GetAttack();
            stats[1].text = "" + characterRepresentative.GetDefence();
            stats[2].text = "" + characterRepresentative.GetName();
        } else {
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
                    onToggleMove.Raise(this, new DataPacket(gameObject.transform.position, "ArrowVector"));
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

                    onToggleMove.Raise(this, new DataPacket(clickedCellName, "SwapRequest", "Button Factory"));
                    onToggleMove.Raise(this, new DataPacket(gameObject.transform.position, "ArrowVector"));
                    clicked = false;
                    rend.color = Color.white;
                    arrowDisplay.SetActive(false);
                }
                else if (clicked)
                {
                    onToggleMove.Raise(this, new DataPacket(gameObject.transform.position, "ArrowVector"));
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

    // public void ToggleSelectMove(Component sender, object data)
    // {
    //     if (data is Vector3)
    //     {
    //         clickedVector = (Vector3)data;
    //         arrowVector = new Vector3(
    //             gameObject.transform.position.x
    //                 + (clickedVector.x - gameObject.transform.position.x) / 2,
    //             gameObject.transform.position.y
    //                 + (clickedVector.y - gameObject.transform.position.y) / 2,
    //             -0.4f
    //         );
    //         selectMoveMode = !selectMoveMode;
    //         if (clicked)
    //         {
    //             rend.color = Color.yellow;
    //         }
    //     }
    //     else if (data is string)
    //     {
    //         string readableData = data.ToString();
    //         if (readableData.Equals("Move attempt!"))
    //         {
    //             arrowDisplay.SetActive(false);
    //             selectMoveMode = false;
    //             if (this == sender || clicked)
    //             {
    //                 clicked = false;
    //                 ToggleHasHero();
    //                 rend.color = Color.white;
    //             }
    //         }
    //     }
    // }

    public void ToggleClicked(){
        clicked = !clicked;
    }

    public void ReceiveDataPacket(Component sender, object data)
    {
        DataPacket dPacket = (DataPacket) data;
        if ((dPacket.GetDestination() == null || dPacket.GetDestination().Equals(gameObject.name)))
        {
            object incomingData = dPacket.GetData();
            string dataLabel = dPacket.GetLabel();
            if (dataLabel.Equals("ArrowVector"))
            {
                selectMoveMode = !selectMoveMode;
                clickedCellName = sender.name;
                clickedVector = (Vector3) incomingData;
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
            } else if (dataLabel.Equals("CharacterData")){
                characterRepresentative = (testHero) incomingData;
            }
        }
    }

    public void ToggleHasHero()
    {
        hasHero = !hasHero;
    }

    public bool GetSelectMoveMode()
    {
        return selectMoveMode;
    }
}
