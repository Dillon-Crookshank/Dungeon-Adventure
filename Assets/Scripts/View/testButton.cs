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

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(gameObject.transform.position);
            if (hasHero)
            {
                ToggleSelectMove(this, null);
                onToggleMove.Raise(this, gameObject.transform.position);
                onButtonClicked.Raise(this, selectMoveMode.ToString());
                rend.sprite = spriteArray[System.Convert.ToInt32(hasHero)];
                held = true;
                clicked = !clicked;
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
            if (selectMoveMode && gameObject.transform.position != clickedVector)
            {
                arrowDisplay.SetActive(true);
                arrowDisplay.transform.position = arrowVector;
                arrowDisplay.GetComponent<SpriteRenderer>().size = new Vector2(
                    20 * Mathf.Sqrt(
                        Mathf.Pow(
                            (clickedVector.x - gameObject.transform.position.x) / 2,
                            2
                        )
                            + Mathf.Pow(
                                (clickedVector.y - gameObject.transform.position.y) / 2,
                                2
                            )
                    ),
                    10f
                );
                arrowDisplay.transform.rotation = Quaternion.Euler(
                    0f,
                    0f,
                    180 + Mathf.Cos((clickedVector.x - gameObject.transform.position.x)*(clickedVector.y - gameObject.transform.position.y))
                );
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
        }
    }

    public void ToggleHasHero()
    {
        hasHero = !hasHero;
        //rend.sprite = spriteArray[System.Convert.ToInt32(hasHero)];
    }

    public bool GetSelectMoveMode()
    {
        return selectMoveMode;
    }
}
