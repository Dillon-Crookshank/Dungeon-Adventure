using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// An representation of a GUI button that handles file loading.
/// </summary>
namespace DefaultNamespace
{
    class actionButton : clickableButton
    {    

        private bool isClickable;

        private Color lockedColor = new Color(0.2f, 0.2f, 0.2f, 1f);

        [SerializeField]
        string displayHeader;

        [SerializeField]
        string displayDescription;

        void Update(){
            if (!isClickable){
                rend.color = lockedColor;
            }
        }

        void OnMouseOver(){
            if (isClickable){
                GameObject.Find("ActionHeader").SendMessage("setText", displayHeader);
                GameObject.Find("ActionDescription").SendMessage("setText", displayDescription);
                GetComponent<SpriteRenderer>().color = highlightColor;
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log(name);
                    PressButton();
                }
            }
        }

        void OnMouseExit(){
            if (isClickable){
                GetComponent<SpriteRenderer>().color = Color.white;
                GameObject.Find("ActionHeader").SendMessage("setText", "");
                GameObject.Find("ActionDescription").SendMessage("setText", "");
            }   
        }

        /// <summary>
        /// Handles sending a request to the Button Factory to load the party.
        /// </summary>
        public override void PressButton()
        {
            if (isClickable){
                Debug.Log(name + " pressed");
            }   
        }

        public void SetDescription(in string theDescription) {
            displayDescription = theDescription;
        }

        void SetClickable(bool theClickability){
            isClickable = theClickability;
        }
    }
}
