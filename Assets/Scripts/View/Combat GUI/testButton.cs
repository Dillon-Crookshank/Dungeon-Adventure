using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class testButton : MonoBehaviour
{
    [Header("Events")]
    public GameEvent onButtonClicked;

    public GameEvent onToggleMove;

    public GameObject arrowDisplay;

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
        rend.sprite = spriteArray[System.Convert.ToInt32(hasHero)];
    }

    void Update(){
        
        string labelTexts = "";
        if (hasHero){
            labelTexts = "0";
        }

        foreach(TextMesh textLabel in stats){
            textLabel.text = labelTexts;
        }

        rend.sprite = spriteArray[System.Convert.ToInt32(hasHero)];
        labelArea.SetActive(hasHero);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!selectMoveMode){
                if (hasHero){
                    onToggleMove.Raise(this, gameObject.transform.position);
                    onButtonClicked.Raise(this, selectMoveMode.ToString());
                    rend.sprite = spriteArray[System.Convert.ToInt32(hasHero)];
                    held = true;
                    clicked = !clicked;
                }
            } else {
                if (!hasHero){
                    onToggleMove.Raise(this, "Move attempt!");
                    clicked = false;
                } else if (clicked) {
                    onToggleMove.Raise(this, gameObject.transform.position);
                    onButtonClicked.Raise(this, selectMoveMode.ToString());
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
                    arrowDisplay.GetComponent<SpriteRenderer>().size = new Vector2(
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

                    // Obtains the new rotation values by calling LookAt to obtain new Z and W values.
                    arrowDisplay.transform.LookAt(gameObject.transform, new Vector3(0f, 0f, -1f));
                    float newZ = arrowDisplay.transform.rotation.z;
                    float newW = arrowDisplay.transform.rotation.w;
                    arrowDisplay.transform.rotation = new Quaternion(0f, 0f, newZ, newW);
                if (hasHero){
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

    public void ToggleSelectMove(Component sender, object data)
    {
        if (data is Vector3)
        {
            clickedVector = (Vector3)data;
            arrowVector = new Vector3(
                gameObject.transform.position.x
                    + (clickedVector.x - gameObject.transform.position.x) / 2,
                gameObject.transform.position.y
                    + (clickedVector.y - gameObject.transform.position.y) / 2,
                -0.4f
            );
            selectMoveMode = !selectMoveMode;
            if (clicked)
            {
                rend.color = Color.yellow;
            }
        } else if (data is string){
            string readableData = data.ToString();
            if (readableData.Equals("Move attempt!")){
                arrowDisplay.SetActive(false);
                selectMoveMode = false;
                if (this == sender || clicked){
                    clicked = false;
                    ToggleHasHero();
                    rend.color = Color.white;
                }
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
