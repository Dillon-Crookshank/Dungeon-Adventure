using UnityEngine;

public class DungeonController : MonoBehaviour {

    [SerializeField] private Sprite myTestSprite;

    public void Start() {
        new UButton("Button0", myTestSprite, new Vector3(-0.5f, 0.5f, 0), new Vector3(1, 1, 1));
        new UButton("Button1", myTestSprite, new Vector3(0.5f, 0.5f, 0), new Vector3(1, 1, 1));
        new UButton("Button2", myTestSprite, new Vector3(-0.5f, -0.5f, 0), new Vector3(1, 1, 1));
        new UButton("Button3", myTestSprite, new Vector3(0.5f, -0.5f, 0), new Vector3(1, 1, 1));
    }

    public void Update() {
        //Debug.Log(Input.mousePosition);
    }

    public void EventListener((string, EventType) theInfo) {
        Debug.Log($"({theInfo.Item1}, {theInfo.Item2})");
    }
}

public enum EventType {
    LeftMousePressed,
    RightMousePressed,
    LeftMouseReleased,
    RightMouseReleased
}

