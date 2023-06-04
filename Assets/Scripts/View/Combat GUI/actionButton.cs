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
        [SerializeField]
        GameEvent onButtonHover;

        [SerializeField]
        string displayHeader;

        [SerializeField]
        string displayDescription;

        void OnMouseOver()
        {
            onButtonHover.Raise(this, new DataPacket(displayHeader, "NewTextString", "ActionHeader"));
            onButtonHover.Raise(this, new DataPacket(displayDescription, "NewTextString", "ActionDescription"));
            GetComponent<SpriteRenderer>().color = highlightColor;
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(name);
                PressButton();
            }
        }

        void OnMouseExit()
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            onButtonHover.Raise(this, new DataPacket("", "NewTextString", "ActionHeader"));
            onButtonHover.Raise(this, new DataPacket("", "NewTextString", "ActionDescription"));
        }

        /// <summary>
        /// Handles sending a request to the Button Factory to load the party.
        /// </summary>
        public override void PressButton()
        {
            Debug.Log(name + " pressed");
        }

        public void SetDescription(in string theDescription)
        {
            displayDescription = theDescription;
        }
    }
}
