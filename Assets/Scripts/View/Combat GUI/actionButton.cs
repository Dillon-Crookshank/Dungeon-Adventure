using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// An representation of a GUI button that handles file loading.
/// </summary>
namespace DungeonAdventure
{
    class actionButton : clickableButton
    {    

        private bool myClickability;

        private Color myLockedColor = new Color(0.2f, 0.2f, 0.2f, 1f);

        [SerializeField]
        private string myDisplayHeader;

        [SerializeField]
        private string myDisplayDescription;

        private SpriteRenderer myActionRenderer;

        void Start(){
            myActionRenderer = GetComponent<SpriteRenderer>();
        }

        void Update(){
            if (!myClickability){
                myActionRenderer.color = myLockedColor;
            }
        }

        void OnMouseOver(){
            if (myClickability){
                GameObject.Find("ActionHeader").SendMessage("setText", myDisplayHeader);
                GameObject.Find("ActionDescription").SendMessage("setText", myDisplayDescription);
                myActionRenderer.color = myHighlightColor;
                if (Input.GetMouseButtonDown(0))
                {
                    PressButton();
                }
            }
        }

        void OnMouseExit(){
            if (myClickability){
                GetComponent<SpriteRenderer>().color = Color.white;
                GameObject.Find("ActionHeader").SendMessage("setText", "");
                GameObject.Find("ActionDescription").SendMessage("setText", "");
            }   
        }

        public override void PressButton(){}

        public void SetDescription(in string theDescription) {
            myDisplayDescription = theDescription;
        }

        void SetClickable(bool theClickability){
            myClickability = theClickability;
        }
    }
}
