using UnityEngine;

sealed class testBehavior : MonoBehaviour
{
    bool focused = false;

    void OnMouseDrag()
    {
        if (focused)
        {
            gameObject.transform.Translate(0.01f, 0.01f, 0.0f);
        }
    }

    void OnMouseOver()
    {
        focused = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    void OnMouseExit()
    {
        focused = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
