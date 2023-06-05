using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

public class FadingDisplay : MonoBehaviour {
    /// <summary>
    /// The name of the Controller game object that contains the response method
    /// </summary>
    [SerializeField]
    private string myController;

    /// <summary>
    /// The name of the method in the controller that should be called once the message has faded in.
    /// </summary>
    [SerializeField]
    private string myResponse;

    /// <summary>
    /// The amount of time a single stage of animation takes. The animation has 3 stages: Fade In, Stay at full opacity, Fade Out
    /// </summary>
    [SerializeField]
    private int myFadeTime;

    private SpriteRenderer myRenderer;

    public void Start() {
        myRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    public void DoFadeAnimation() {
        FadeAnimation();
    }


    private async void FadeAnimation() {
        this.transform.localPosition += new Vector3(0, 0, -25);
        Debug.Log(myRenderer.color.a);
        //Fade in
        Debug.Log("Fade In");
        GameObject.Find("ActionButtons").SendMessage("UnlockButtons", false);
        myRenderer.color = new Color(myRenderer.color.r, myRenderer.color.g, myRenderer.color.b, 0f);
        for (int i = 0; i < 100; i++) {
            await Task.Delay(myFadeTime / 100);
            myRenderer.color = new Color(myRenderer.color.r, myRenderer.color.g, myRenderer.color.b, myRenderer.color.a + 0.01f);
        }
    

        //Stay at full opacity
        Debug.Log("Hold");
        await Task.Delay(myFadeTime);

        //Fade out
        Debug.Log("Fade Out");
        for (int i = 0; i < 100; i++) {
            await Task.Delay(myFadeTime / 100);
            myRenderer.color = new Color(myRenderer.color.r, myRenderer.color.g, myRenderer.color.b, myRenderer.color.a - 0.01f);
        }

        //Send the response to the controller
        GameObject.Find(myController).SendMessage(myResponse);

        this.transform.localPosition += new Vector3(0, 0, 25);
    }
}