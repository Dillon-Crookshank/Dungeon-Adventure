using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

/// <summary>
/// The behaviour script attached to a fading display.
/// </summary>
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
    
    /// <summary>
    /// The amount of time between phases of the animation. Since there are three phases, the animation will take (myFadeTime * 3) ms to complete.
    /// </summary>
    private int myFadeTime;

    /// <summary>
    /// The sprite renderer attached to the GameObject; This is fetched on Object Startup automatically.
    /// </summary>
    private SpriteRenderer myRenderer;

    /// <summary>
    /// Called when the Game Object is initialized.
    /// </summary>
    public void Start() {
        myRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// This method starts the coroutine that does the fading animation.
    /// </summary>
    public void DoFadeAnimation() {
        FadeAnimation();
    }

    /// <summary>
    /// The coroutine that makes the display fade in and out. This is done by editing the gama value of the color of the sprite renderer.
    /// </summary>
    private async void FadeAnimation() {
        this.transform.localPosition += new Vector3(0, 0, -25);
        // Debug.Log(myRenderer.color.a);
        //Fade in
        // Debug.Log("Fade In");
        GameObject.Find("ActionButtons").SendMessage("UnlockButtons", false);
        myRenderer.color = new Color(myRenderer.color.r, myRenderer.color.g, myRenderer.color.b, 0f);
        for (int i = 0; i < 100; i++) {
            await Task.Delay(myFadeTime / 100);
            myRenderer.color += new Color(0, 0, 0, 0.01f);
        }
    

        //Stay at full opacity
        await Task.Delay(myFadeTime);

        //Fade out
        for (int i = 0; i < 100; i++) {
            await Task.Delay(myFadeTime / 100);
            myRenderer.color += new Color(0, 0, 0, -0.01f);
        }

        //Send the response to the controller
        GameObject.Find(myController).SendMessage(myResponse);

        this.transform.localPosition += new Vector3(0, 0, 25);
    }
}