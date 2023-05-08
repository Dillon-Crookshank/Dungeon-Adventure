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
                float priorX = arrowDisplay.transform.rotation.x;
                float priorY = arrowDisplay.transform.rotation.y;
                arrowDisplay.transform.LookAt(gameObject.transform, new Vector3(0f, 0f, -1f));
                float newZ = arrowDisplay.transform.rotation.z;
                float newW = arrowDisplay.transform.rotation.w;
                arrowDisplay.transform.rotation = new Quaternion(priorX, priorY, newZ, newW);
                
                // arrowDisplay.transform.localRotation = Quaternion.Euler(
                //     0f,
                //     0f,
                //     -180
                //         * DotProduct2D(
                //             new Vector3(0f, gameObject.transform.position.y, 0f),
                //             new Vector3(
                //                 (clickedVector.x - gameObject.transform.position.x),
                //                 (clickedVector.y - gameObject.transform.position.y),
                //                 0f
                //             )
                //         )
                // );
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

    private float Pythagorean(float v1, float v2)
    {
        return Mathf.Sqrt(Mathf.Pow(v1, 2) + Mathf.Pow(v2, 2));
    }

    private float DotProduct2D(Vector3 v1, Vector3 v2)
    {
        return v1.x * v2.x + v1.y * v2.y;
    }
}
