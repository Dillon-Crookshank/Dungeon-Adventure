using UnityEngine;

namespace DungeonAdventure
{
    /// <summary>
    /// A manager that mass-alters the available action buttons.
    /// </summary>
    public class actionButtonManager : MonoBehaviour
    {
        /// <summary>
        /// The array of action buttons to manage. Defined in the editor.
        /// </summary>
        [SerializeField]
        private GameObject[] myActionButtons;

        /// <summary>
        /// Changes whether or not all managed buttons can be clicked.
        /// </summary>
        /// <summary>
        /// <param name="theAvailability"> The current availability of the button. </param>
        void UnlockButtons(bool theAvailability)
        {
            // Debug.Log("Yep!");
            foreach (GameObject button in myActionButtons)
            {
                button.SendMessage("SetClickable", theAvailability);
                if (theAvailability)
                {
                    button.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }

        /// <summary>
        /// Changes whether or not all managed buttons can be seen.
        /// </summary>
        /// <summary>
        /// <param name="theAvailability"> The current availability of the button. </param>
        void EnableButtons(bool theAvailability)
        {
            foreach (GameObject button in myActionButtons)
            {
                button.SetActive(theAvailability);
            }
        }
    }
}
